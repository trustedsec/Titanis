using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;

namespace Titanis.Cli
{
	/// <summary>
	/// Describes a parameter group.
	/// </summary>
	/// <seealso cref="ParameterGroupAttribute"/>
	public class ParameterGroupInfo
	{
		internal ParameterGroupInfo(ParameterGroupOptions options)
		{
			this.Options = options;
		}

		internal ParameterGroupInfo(
			ParameterGroupInfo? nestingGroup,
			PropertyDescriptor? groupProperty,
			ConstructorInfo? constructor,
			string? groupCategory,
			ParameterGroupOptions options)
			: this(options)
		{
			this.NestingGroup = nestingGroup;
			this.GroupProperty = groupProperty;
			this.Constructor = constructor;
			this.GroupCategory = groupCategory;
		}

		internal object GetGroupObject(object instance, object owner)
			=> this.GetGroupObject(instance, owner, true)!;
		internal object? GetGroupObject(object instance, object owner, bool create)
		{
			if (this.NestingGroup != null)
				owner = this.NestingGroup.GetGroupObject(instance, owner);

			if (this.GroupProperty == null)
			{
				return instance;
			}
			else
			{
				var propValue = this.GroupProperty.GetValue(owner);
				if (propValue == null && create)
				{
					propValue = this.Constructor.Invoke(null);
					if (propValue is IParameterGroup parmGroup)
						parmGroup.Initialize((instance as IServiceProvider)?.GetService<IServiceContainer>(), instance);
					this.GroupProperty.SetValue(owner, propValue);
				}
				return propValue;
			}
		}

		/// <summary>
		/// <see cref="ParameterGroupInfo"/> of the group containing this group.
		/// </summary>
		public ParameterGroupInfo? NestingGroup { get; }
		/// <summary>
		/// Gets the <see cref="PropertyInfo"/> referencing this group.
		/// </summary>
		public PropertyDescriptor? GroupProperty { get; }
		/// <summary>
		/// Gets the constructor to create a new instance of this group.
		/// </summary>
		public ConstructorInfo? Constructor { get; }
		/// <summary>
		/// Gets the name of the category for parameters in this group, if any.
		/// </summary>
		public string? GroupCategory { get; }
		/// <summary>
		/// Gets a <see cref="ParameterGroupOptions"/> specifying options for this group.
		/// </summary>
		public ParameterGroupOptions Options { get; }

		public bool IsRoot => (this.GroupProperty == null);
	}
}
