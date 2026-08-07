using Microsoft.Data.Sqlite;
using System.Data.SqlTypes;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Titanis.Info
{
	internal abstract class Table
	{
		public abstract string Name { get; }
	}

	internal class TableInserter<TRow>
	{
		public TableInserter(SqliteCommand cmd, Func<TRow, object[]> vectorizer)
		{
			this._cmd = cmd;
			this._vectorizer = vectorizer;
		}

		private SqliteCommand _cmd;
		private Func<TRow, object[]> _vectorizer;

		public async Task<long> Insert(TRow obj, SqliteTransaction txact, CancellationToken cancellationToken)
		{
			var cmd = this._cmd;
			cmd.Connection = txact.Connection;
			cmd.Transaction = txact;
			var values = this._vectorizer(obj);
			for (int i = 0; i < values.Length; i++)
			{
				object? value = values[i];
				cmd.Parameters[i].Value = value ?? DBNull.Value;
			}
			return (long)await cmd.ExecuteScalarAsync(cancellationToken);
		}
	}

	struct SelectQueryInfo
	{
		internal Dictionary<string, object>? keys;
		internal ExtraFieldInfo[]? extraFields;
		internal string[]? sortFields;
	}

	internal class Table<TRow> : Table
	{
		internal Table()
		{
		}

		public override string Name => typeof(TRow).Name;

		public Task<IList<TRow>> Select(
			SqliteTransaction txact,
			Expression<Func<TRow, bool>>? predicate,
			in SelectQueryInfo queryInfo,
			CancellationToken cancellationToken
			) => this.Select(txact, predicate, BuildDefaultLambda(true), queryInfo, cancellationToken);
		public async Task<IList<TResult>> Select<TResult>(
			SqliteTransaction txact,
			Expression<Func<TRow, bool>>? predicate,
			Expression<Func<TRow, TResult>> projection,
			SelectQueryInfo queryInfo,
			CancellationToken cancellationToken
			)
		{
			if (txact is null) throw new ArgumentNullException(nameof(txact));
			if (projection is null) throw new ArgumentNullException(nameof(projection));

			var sel = this.BuildSelector(predicate, projection, queryInfo);
			List<TResult> results = new List<TResult>();
			await foreach (var result in sel(txact, cancellationToken).WithCancellation(cancellationToken))
			{
				results.Add(result);
			}
			return results;
		}

		private static DateTime? ParseNullableDate(string? dateText)
		{
			if (string.IsNullOrEmpty(dateText))
				return null;
			else
				return DateTime.Parse(dateText);
		}

		private static readonly ConstantExpression dbnullConstExpr = Expression.Constant(DBNull.Value);
		private static readonly ConstantExpression nullConstExpr = Expression.Constant(null);

		public Func<SqliteTransaction, CancellationToken, IAsyncEnumerable<TResult>> BuildSelector<TResult>(
			Expression<Func<TRow, bool>>? predicate,
			Expression<Func<TRow, TResult>> projection,
			SelectQueryInfo queryInfo
			)
		{
			if (projection is null) throw new ArgumentNullException(nameof(projection));

			StringBuilder sb = new StringBuilder();
			sb.Append("SELECT ");

			var cmd = new SqliteCommand();
			var b = new SqlSelectBuilder(sb, cmd);
			b.DefineTableParameter(projection.Parameters[0], "t");
			b.Visit(projection);

			if (queryInfo.extraFields != null)
			{
				foreach (var extraField in queryInfo.extraFields)
				{
					switch (extraField.Typecode)
					{
						case TypeCode.DateTime:
							sb.Append($", datetime([{extraField.Name}])");
							break;
						default:
							sb.Append($", [{extraField.Name}]");
							break;
					}
				}
			}

			sb.Append($" FROM [{this.Name}] t");
			if (predicate != null || queryInfo.keys != null)
			{
				sb.Append(" WHERE (");
				if (predicate != null)
				{
					b.DefineTableParameter(predicate.Parameters[0], "t");
					b.Visit(predicate);
				}

				if (queryInfo.keys != null)
					AppendKeysPredicate(queryInfo.keys, sb, b, (predicate != null));

				sb.Append(")");
			}

			if (!queryInfo.sortFields.IsNullOrEmpty())
			{
				sb.Append(" ORDER BY ");
				for (int i = 0; i < queryInfo.sortFields.Length; i++)
				{
					if (i > 0)
						sb.Append(",");

					string? field = queryInfo.sortFields[i];
					sb.Append($"[{field}]");
				}
			}

			string commandText = sb.ToString();
			cmd.CommandText = commandText;

			var selectedFields = b.selectedFields;
			var selectedMembers = b.selectedMembers;
			List<MemberBinding> bindings = new List<MemberBinding>(selectedFields.Count - b.ctorArgCount);
			var parmArray = Expression.Parameter(typeof(object[]));
			List<Expression> ctorArgs = new List<Expression>();
			for (int i = 0; i < selectedFields.Count; i++)
			{
				var fieldValue = selectedFields[i];
				var fieldMember = selectedMembers[i];
				Expression columnValue = Expression.ArrayIndex(parmArray, Expression.Constant(i));

				Type fieldClrType = fieldValue.Type;
				bool isNullable = false;
				var baseFieldType = Nullable.GetUnderlyingType(fieldClrType);
				isNullable = baseFieldType != null;
				baseFieldType ??= fieldClrType;
				if (baseFieldType == typeof(DateTime))
				{
					columnValue = Expression.Convert(columnValue, typeof(string));
					var parseMethod = isNullable ?
						((Func<string, DateTime?>)ParseNullableDate).Method
						: ((Func<string, DateTime>)DateTime.Parse).Method;
					columnValue = Expression.Call(parseMethod, columnValue);
					//columnValue = Expression.Call(Expression.Constant(null, typeof(DateTime)), parseMethod, columnValue);
				}
				else
				{
					var dbType = DataHelpers.SqliteTypeFromType(baseFieldType);
					Type colClrType = dbType switch
					{
						SqliteType.Integer => typeof(long),
						SqliteType.Real => typeof(double),
						SqliteType.Text => typeof(string),
						SqliteType.Blob => typeof(byte[]),
					};

					if (isNullable)
					{
						if (colClrType.IsValueType)
							colClrType = typeof(Nullable<>).MakeGenericType([colClrType]);
						columnValue = Expression.Convert(columnValue, colClrType);
					}
					else if (fieldClrType.IsClass)
					{
						columnValue = Expression.Convert(columnValue, colClrType);
					}
					else
					{
						Expression<Func<SqlNullValueException>> exceptFactory = () => new SqlNullValueException($"Field '{fieldMember.Name}' contained a null value");
						columnValue = Expression.Condition(Expression.NotEqual(columnValue, nullConstExpr), Expression.Convert(columnValue, colClrType), Expression.Throw(exceptFactory.Body, colClrType));
					}

					if (colClrType != fieldClrType)
						columnValue = Expression.Convert(columnValue, fieldClrType);
				}

				if (i < b.ctorArgCount)
					ctorArgs.Add(columnValue);
				else
					bindings.Add(Expression.Bind(fieldMember, columnValue));
			}


			Func<object[], TResult> factory;
			if (b.ctor is not null)
			{
				var factoryLambda = Expression.Lambda<Func<object[], TResult>>(Expression.MemberInit(Expression.New(b.ctor, ctorArgs), bindings), parmArray);

				factory = factoryLambda.Compile();
			}
			else
			{
				factory = arr => (TResult)arr[0];
			}

			return (txact, cx) => this.PerformSelect<TResult>(cmd, txact, factory, selectedFields.Count, queryInfo.extraFields, cx);
		}

		private static bool AppendKeysPredicate(Dictionary<string, object> keys, StringBuilder sb, SqlSelectBuilder b, bool subseq)
		{
			foreach (var key in keys)
			{
				if (subseq)
					sb.Append(")AND(");
				else
					subseq = true;

				b.AddCondition(key.Key, key.Value);
			}

			return subseq;
		}

		private async IAsyncEnumerable<TResult> PerformSelect<TResult>(
			SqliteCommand cmd_,
			SqliteTransaction txact, Func<object?[], TResult> factory,
			int staticFieldCount,
			ExtraFieldInfo[]? extraFields,
			CancellationToken cancellationToken
			)
		{
			var cmd = new SqliteCommand(cmd_.CommandText, txact.Connection, txact);
			foreach (SqliteParameter param in cmd_.Parameters)
			{
				var newParam = cmd.Parameters.Add(param.ParameterName, param.SqliteType);
				newParam.Value = param.Value;
			}

			var reader = await cmd.ExecuteReaderAsync(cancellationToken);
			object?[] values = new object[reader.FieldCount];
			var extraCount = reader.FieldCount - staticFieldCount;
			while (await reader.ReadAsync(cancellationToken))
			{
				reader.GetValues(values);
				for (int i = 0; i < values.Length; i++)
				{
					object? fieldValue = values[i];
					if (fieldValue == DBNull.Value)
						values[i] = null;
				}

				var row = factory(values);
				if (extraFields != null && (row is IWantExtraFields extra))
				{
					Dictionary<string, object?> extraValues = new Dictionary<string, object?>(extraCount);
					for (int i = 0; i < extraCount; i++)
					{
						ref var extraField = ref extraFields[i];
						var name = extraField.Name;
						var value = values[i + staticFieldCount];
						if (value is null or DBNull)
						{
							value = null;
						}
						else
						{
							value = extraField.Typecode switch
							{
								TypeCode.Boolean => (long)value != 0,
								TypeCode.Byte => (byte)(long)value,
								TypeCode.Char => ((string)value)[0],
								TypeCode.DateTime => DateTime.Parse((string)value),
								TypeCode.DBNull => null,
								TypeCode.Decimal => (decimal)(double)value,
								TypeCode.Double => (double)value,
								TypeCode.Empty => null,
								TypeCode.Int16 => (short)(long)value,
								TypeCode.Int32 => (int)(long)value,
								TypeCode.Int64 => (long)value,
								TypeCode.SByte => (sbyte)(long)value,
								TypeCode.Single => (float)(double)value,
								TypeCode.String => (string)value,
								TypeCode.UInt16 => (ushort)(long)value,
								TypeCode.UInt32 => (uint)(long)value,
								TypeCode.UInt64 => (ulong)(long)value,
								TypeCode.Object or _ => value,
							};
						}
						if (value != null)
							extraValues[name] = value;
					}
					extra.SetExtraFields(extraValues);
				}
				yield return row;
			}
		}

		public async Task<long> Insert(
			Expression<Func<TRow>> insertFunc,
			SqliteTransaction txact,
			CancellationToken cancellationToken,
			Dictionary<string, object>? extraFields = null
			)
		{
			if (insertFunc is null) throw new ArgumentNullException(nameof(insertFunc));
			if (txact is null) throw new ArgumentNullException(nameof(txact));

			SqliteCommand cmd = new SqliteCommand();

			StringBuilder sb = new StringBuilder();
			sb.Append($"INSERT INTO [{this.Name}](");

			SqlInsertBuilder updb = new SqlInsertBuilder(sb, cmd);
			updb.Visit(insertFunc);
			if (extraFields != null)
			{
				foreach (var item in extraFields)
				{
					sb.Append($",[{item.Key}]");
				}
			}
			sb.Append(")VALUES(");
			updb.EmitInsertedValues();
			if (extraFields != null)
			{
				int extIndex = 0;
				foreach (var item in extraFields)
				{
					sb.Append(',');
					updb.VisitConstant(item.Value, item.Value?.GetType());
				}
			}
			sb.Append(");SELECT last_insert_rowid()");

			cmd.CommandText = sb.ToString();
			cmd.Connection = txact.Connection;
			cmd.Transaction = txact;
			return (long)(await cmd.ExecuteScalarAsync(cancellationToken));
		}

		public TableInserter<TRow> BuildInserter()
		{
			Expression<Func<TRow, TRow>> lambda = BuildDefaultLambda(false);
			return this.BuildInserter(lambda);
		}

		private static Expression<Func<TRow, TRow>> BuildDefaultLambda(bool includeKey)
		{
			var props = typeof(TRow).GetProperties(BindingFlags.Instance | BindingFlags.Public);
			List<MemberBinding> bindings = new List<MemberBinding>(props.Length);
			ParameterExpression param = Expression.Parameter(typeof(TRow));
			foreach (var prop in props)
			{
				if (prop.CanWrite && (includeKey || !prop.IsDefined(typeof(PrimaryKeyAttribute))))
					bindings.Add(Expression.Bind(prop, Expression.Property(param, prop)));
			}

			var lambda = Expression.Lambda<Func<TRow, TRow>>(Expression.MemberInit(Expression.New(typeof(TRow)), bindings), param);
			return lambda;
		}

		public TableInserter<TRow> BuildInserter(Expression<Func<TRow, TRow>> insertFunc, Dictionary<string, object>? extraFields = null)
		{
			if (insertFunc is null) throw new ArgumentNullException(nameof(insertFunc));

			SqliteCommand cmd = new SqliteCommand();

			StringBuilder sb = new StringBuilder();
			sb.Append($"INSERT INTO [{this.Name}](");

			SqlInsertBuilder updb = new SqlInsertBuilder(sb, cmd);
			updb.Visit(insertFunc);
			if (extraFields != null)
			{
				foreach (var item in extraFields)
				{
					sb.Append($",[{item.Key}]");
				}
			}
			sb.Append(")VALUES(");

			var parms = updb._insertedValues;
			var paramValues = new Expression[parms.Count];
			for (int i = 0; i < parms.Count; i++)
			{
				if (i > 0)
					sb.Append(',');
				var paramSet = parms[i];
				var paramValue = paramSet;
				var name = $"@prm{i}";
				sb.Append(name);
				var param = cmd.Parameters.Add(name, DataHelpers.SqliteTypeFromType(paramSet.Type));

				if (!paramValue.Type.IsClass)
				{
					paramValue = Expression.Convert(paramValue, typeof(object));
				}
				paramValues[i] = paramValue;
			}
			if (extraFields != null)
			{
				int extIndex = 0;
				foreach (var item in extraFields)
				{
					var paramName = $"@ext{++extIndex}";
					sb.Append($",{paramName}");
					cmd.Parameters.AddWithValue(paramName, DataHelpers.ToDataValue(item.Value));
				}
			}
			sb.Append(");SELECT last_insert_rowid()");
			cmd.CommandText = sb.ToString();

			var vectorizer = Expression.Lambda<Func<TRow, object[]>>(
				Expression.NewArrayInit(typeof(object), paramValues),
				insertFunc.Parameters
				);
			vectorizer.Compile();
			return new TableInserter<TRow>(cmd, vectorizer.Compile());
		}

		public async Task Update(
			SqliteTransaction txact,
			Expression<Func<TRow, bool>> predicate,
			Expression<Func<TRow, TRow>> updateFunc,
			Dictionary<string, object?>? extraFields,
			CancellationToken cancellationToken
			)
		{
			if (txact is null) throw new ArgumentNullException(nameof(txact));
			if (updateFunc is null) throw new ArgumentNullException(nameof(updateFunc));

			SqliteCommand cmd = new SqliteCommand();

			StringBuilder sb = new StringBuilder();
			var target = $"[{this.Name}]";
			sb.Append($"UPDATE {target} SET ");

			SqlUpdateBuilder updb = new SqlUpdateBuilder(sb, cmd);
			updb.DefineTableParameter(updateFunc.Parameters[0], target);
			updb.Visit(updateFunc);

			if (extraFields != null)
			{
				foreach (var field in extraFields)
				{
					updb.SetField(field.Key, field.Value);
				}
			}

			if (predicate != null)
			{
				sb.Append(" WHERE ");

				SqlPredicateBuilder predb = new SqlPredicateBuilder(sb, cmd);
				predb.DefineTableParameter(predicate.Parameters[0], target);
				predb.Visit(predicate);
			}

			sb.Append(";SELECT changes()");

			cmd.CommandText = sb.ToString();
			cmd.Connection = txact.Connection;
			cmd.Transaction = txact;
			var changed = (long)await cmd.ExecuteScalarAsync(cancellationToken);

		}
	}

	class SqlBuilderBase : ExpressionVisitor
	{
		internal SqlBuilderBase(StringBuilder sb, SqliteCommand cmd)
		{
			this._sb = sb;
			this.cmd = cmd;
		}
		protected readonly StringBuilder _sb;
		private readonly SqliteCommand cmd;

		private static object _validValue = new object();
		protected static ConstantExpression Valid = Expression.Constant(_validValue);

		#region Parameters
		private Dictionary<ParameterExpression, string> _tableParams = new Dictionary<ParameterExpression, string>();
		public void DefineTableParameter(ParameterExpression param, string expr)
		{
			this._tableParams.Add(param, expr);
		}
		protected string AllocValueParam(ref object? value)
		{
			value = DataHelpers.ToDataValue(value);
			string name = $"@c_{this.cmd.Parameters.Count}";
			this.cmd.Parameters.AddWithValue(name, value);
			return name;
		}
		#endregion

		#region Generation
		protected string EmitFieldRef(MemberInfo member) => this.EmitFieldRef(member.Name);
		protected string EmitFieldRef(string fieldName)
		{
			this._sb.Append($"[{fieldName}]");
			return fieldName;
		}
		#endregion

		protected int Depth { get; private set; }

		private Expression? _current;
		protected Expression? Parent { get; private set; }
		private bool _supported;
		protected T Supported<T>(T expression)
		{
			this._supported = true;
			return expression;
		}

		protected override MemberBinding VisitMemberBinding(MemberBinding node)
		{
			this._supported = false;
			base.VisitMemberBinding(node);
			if (!this._supported)
				throw new NotSupportedException($"Not supported: {node.BindingType}");

			this._supported = false;
			return node;
		}

		public override Expression Visit(Expression node)
		{
			this.Depth++;

			var parent = this.Parent;
			try
			{
				this.Parent = this._current;
				this._current = node;

				var converted = base.Visit(node);
				var supp = this._supported;

				if (!supp)
					throw new NotSupportedException($"Unsupported expression {node.NodeType}");

				this._supported = false;
				return converted;
			}
			finally
			{
				this._current = this.Parent;
				this.Parent = parent;
				this.Depth--;
			}
		}
		protected override Expression VisitLambda<T>(Expression<T> node)
		{
			if (this.Parent != null)
				throw new ArgumentException($"A lambda may only appear as the root expression: {node}", nameof(node));

			this.Visit(node.Body);
			return Supported(node);
		}

		protected override Expression VisitUnary(UnaryExpression node)
		{
			if (node.NodeType is ExpressionType.Convert)
			{
				var nullType = Nullable.GetUnderlyingType(node.Type);
				if (nullType == node.Operand.Type)
					// Support nullable conversions
					return Supported(this.Visit(node.Operand));
				var tcOuter = Type.GetTypeCode(node.Type);
				var tcInner = Type.GetTypeCode(node.Operand.Type);
				if (tcOuter.IsIntegral() && tcInner.IsIntegral())
					return Supported(this.Visit(node.Operand));
			}
			return base.VisitUnary(node);
		}

		protected override Expression VisitBinary(BinaryExpression node)
		{
			var op = node.NodeType switch
			{
				ExpressionType.Equal => "=",
				ExpressionType.NotEqual => "!=",
				ExpressionType.LessThan => "<",
				ExpressionType.LessThanOrEqual => "<=",
				ExpressionType.GreaterThan => ">",
				ExpressionType.GreaterThanOrEqual => ">=",
				ExpressionType.AndAlso => " AND ",
				ExpressionType.OrElse => " OR ",
			};

			this._sb.Append('(');
			this.Visit(node.Left);
			this._sb.Append(op);
			this.Visit(node.Right);
			this._sb.Append(')');

			return Supported(node);
		}


		internal void AddCondition(string key, object? value)
		{
			this._sb.Append($"[{key}]=");
			this.VisitConstant(value, value?.GetType());
		}


		private bool TryResolveConstInstance(Expression? expression, out object? constInst)
		{
			if (expression is ConstantExpression constExpr)
			{
				constInst = constExpr.Value;
				return true;
			}
			else if (expression is MemberExpression memberExpr)
			{
				if (TryResolveConstInstance(memberExpr.Expression, out var innerInst) || (memberExpr.Expression is null))
				{
					constInst =
						(memberExpr.Member is FieldInfo field) ? field.GetValue(innerInst)
						: (memberExpr.Member is PropertyInfo property) ? property.GetValue(innerInst, null)
						: throw new NotSupportedException($"Cannot get member {memberExpr.Member}");
					return true;
				}
			}

			constInst = null;
			return false;
		}

		protected override Expression VisitMember(MemberExpression node)
		{
			if (TryResolveConstInstance(node, out var constInst))
			{
				this.VisitConstant(constInst, node.Type);
				return Supported(node);
			}
			else if (node.Expression is ParameterExpression param && this._tableParams.TryGetValue(param, out var table))
			{
				// This is a field reference

				var type = node.Type;
				bool isDate = (type == typeof(DateTime) || type == typeof(DateTime?));
				if (isDate)
					this._sb.Append("datetime(");

				this._sb.Append(table).Append('.');
				this.EmitFieldRef(node.Member);

				if (isDate)
					this._sb.Append(')');

				return Supported(node);
			}

			return node;
		}

		protected override Expression VisitConstant(ConstantExpression node)
		{
			this.VisitConstant(node.Value, node.Type);
			return Supported(node);
		}

		internal void VisitConstant(object? value, Type type)
		{
			var paramName = this.AllocValueParam(ref value);
			switch (Convert.GetTypeCode(value))
			{
				case TypeCode.DateTime:
					this._sb.Append($"julianday({paramName})");
					break;
				default:
					this._sb.Append(paramName);
					break;
			}
		}
	}

	class SqlPredicateBuilder : SqlBuilderBase
	{
		internal SqlPredicateBuilder(StringBuilder sb, SqliteCommand cmd) : base(sb, cmd)
		{
		}
	}

	abstract class SqlUpdateBuilderBase : SqlBuilderBase
	{
		internal SqlUpdateBuilderBase(StringBuilder sb, SqliteCommand cmd) : base(sb, cmd)
		{
		}

		protected override Expression VisitMemberInit(MemberInitExpression node)
		{
			if ((this.Parent?.NodeType ?? (ExpressionType)(-1)) != ExpressionType.Lambda)
				throw new ArgumentException($"A member-init expression may only appear directly under the root lamba expression: {node}", nameof(node));

			return this.Supported(base.VisitMemberInit(node));
		}

		protected abstract void OnSetField(MemberInfo? member, Expression value, bool isConstructorArgument);
		protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
		{
			this.OnSetField(node.Member, node.Expression, false);
			return this.Supported(node);
		}

		protected override Expression VisitNew(NewExpression node)
		{
			if ((this.Parent?.NodeType ?? (ExpressionType)(-1)) is not (ExpressionType.Lambda or ExpressionType.MemberInit))
				throw new ArgumentException($"A new expression may only appear directly under the root lamba expression or a member-init: {node}", nameof(node));

			if (node.Arguments.Count > 0)
			{
				for (int i = 0; i < node.Arguments.Count; i++)
				{
					Expression? arg = node.Arguments[i];
					var member = (i < node.Members.Count) ? node.Members[i] : null;
					this.OnSetField(member, arg, true);
				}
			}

			return this.Supported(node);
		}
	}

	class SqlUpdateBuilder : SqlUpdateBuilderBase
	{
		internal SqlUpdateBuilder(StringBuilder sb, SqliteCommand cmd) : base(sb, cmd)
		{
		}

		internal HashSet<string> _assigned = new HashSet<string>();
		protected override void OnSetField(MemberInfo? member, Expression value, bool isConstructorArgument)
		{
			if (this._assigned.Count > 0)
				this._sb.Append(',');

			this._assigned.Add(this.EmitFieldRef(member));
			this._sb.Append('=');
			this.Visit(value);
		}

		public void SetField(string fieldName, object? value)
		{
			if (this._assigned.Count > 0)
				this._sb.Append(',');

			this._assigned.Add(this.EmitFieldRef(fieldName));
			this._sb.Append('=');
			this.Visit(Expression.Constant(value));
		}
	}

	class SqlInsertBuilder : SqlUpdateBuilderBase
	{
		internal SqlInsertBuilder(StringBuilder sb, SqliteCommand cmd) : base(sb, cmd)
		{
		}

		internal List<Expression> _insertedValues = new List<Expression>();
		protected override void OnSetField(MemberInfo? member, Expression value, bool isConstructorArgument)
		{
			if (this._insertedValues.Count > 0)
				this._sb.Append(',');

			this.EmitFieldRef(member);
			this._insertedValues.Add(value);
		}

		internal void EmitInsertedValues()
		{
			for (int i = 0; i < _insertedValues.Count; i++)
			{
				if (i > 0)
					this._sb.Append(',');

				var member = this._insertedValues[i];
				this.Visit(member);
			}
		}
	}


	class FieldCollector
	{

	}

	class SqlSelectBuilder : SqlUpdateBuilderBase
	{
		public SqlSelectBuilder(StringBuilder sb, SqliteCommand cmd) : base(sb, cmd)
		{
		}

		private FieldCollector? _collector;

		internal readonly List<Expression> selectedFields = new List<Expression>();
		internal readonly List<MemberInfo?> selectedMembers = new List<MemberInfo?>();
		internal int ctorArgCount;
		internal ConstructorInfo? ctor;

		protected override void OnSetField(MemberInfo? member, Expression value, bool isConstructorArgument)
		{
			if (this.selectedFields.Count > 0)
				this._sb.Append(',');

			if (isConstructorArgument)
				this.ctorArgCount++;

			this.selectedFields.Add(value);
			this.selectedMembers.Add(member);
			this.Visit(value);
		}
		protected override Expression VisitNew(NewExpression node)
		{
			this.ctor = node.Constructor;
			return base.VisitNew(node);
		}
	}
}
