using ms_oaut;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc.Client;
using Titanis.Reflection;
using Titanis.Winterop;

namespace Titanis.Msrpc.Msdcom
{
	/// <summary>
	/// Represents an object implementing IDispatch.
	/// </summary>
	/// <remarks>
	/// This object supports dynamic invocation using the <see langword="dynamic"/> keyword.
	/// </remarks>
	public class OleAutomationObject : IDynamicMetaObjectProvider
	{
		internal OleAutomationObject(IDispatch dispatch, DcomClient dcom)
		{
			this._dispatch = dispatch;
			this._dcom = dcom;
		}

		private const int Lcid = 0x0409;
		private const uint FACILITY_CONTROL = 0x800A_0000U;

		// [MS-OAUT] § 2.2.32 - DISPID
		enum DISPID
		{
			Value = 0,
			Unknown = -1,
			PropPut = -3,
			NewEnum = -4
		}

		private readonly IDispatch _dispatch;
		private readonly DcomClient _dcom;

		private Dictionary<string, int>? _membersByName;

		DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
		{
			return new OleAutomationDynamicMeta(parameter, this);
		}

		private async Task<object?> VariantToObject(wireVARIANTStr variant, CancellationToken cancellationToken)
		{
			switch ((VARENUM)variant.vt)
			{
				case VARENUM.VT_DISPATCH:
					return new OleAutomationObject(await variant._varUnion.pdispVal.Unwrap(this._dcom, cancellationToken).ConfigureAwait(false), this._dcom);
				case VARENUM.VT_EMPTY:
					return null;
				case VARENUM.VT_NULL:
					return DBNull.Value;
				case VARENUM.VT_I2:
					return variant._varUnion.iVal;
				case VARENUM.VT_I4:
					return variant._varUnion.lVal;
				case VARENUM.VT_R4:
					return variant._varUnion.fltVal;
				case VARENUM.VT_R8:
					return variant._varUnion.dblVal;
				case VARENUM.VT_BSTR:
					return variant._varUnion.bstrVal.DecodeString();
				case VARENUM.VT_BOOL:
					return variant._varUnion.boolVal != 0;
				case VARENUM.VT_I1:
					return (sbyte)variant._varUnion.bVal;
				case VARENUM.VT_UI1:
					return variant._varUnion.bVal;
				case VARENUM.VT_UI2:
					return variant._varUnion.uiVal;
				case VARENUM.VT_UI4:
					return variant._varUnion.ulVal;
				case VARENUM.VT_I8:
					return variant._varUnion.llVal;
				case VARENUM.VT_UI8:
					return variant._varUnion.ullVal;
				case VARENUM.VT_INT:
					return variant._varUnion.ullVal;
				case VARENUM.VT_DATE:
					return DateTime.FromOADate(variant._varUnion.date);
				case VARENUM.VT_UINT:
				case VARENUM.VT_CY:
				case VARENUM.VT_ERROR:
				case VARENUM.VT_VARIANT:
				case VARENUM.VT_UNKNOWN:
				case VARENUM.VT_DECIMAL:
				case VARENUM.VT_VOID:
				case VARENUM.VT_HRESULT:
				case VARENUM.VT_PTR:
				case VARENUM.VT_SAFEARRAY:
				case VARENUM.VT_CARRAY:
				case VARENUM.VT_USERDEFINED:
				case VARENUM.VT_LPSTR:
				case VARENUM.VT_LPWSTR:
				case VARENUM.VT_RECORD:
				case VARENUM.VT_INT_PTR:
				case VARENUM.VT_UINT_PTR:
				case VARENUM.VT_ARRAY:
				case VARENUM.VT_BYREF:
				default:
					throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Gets the value of a property.
		/// </summary>
		/// <param name="name">Property name</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>The value of the property</returns>
		public async Task<object?> GetProperty(string name, CancellationToken cancellationToken)
		{
			return await this.Invoke(
				name,
				INVOKEKIND.INVOKE_PROPERTYGET,
				null,
				cancellationToken
				).ConfigureAwait(false);
		}

		/// <summary>
		/// Gets the value of a property.
		/// </summary>
		/// <param name="name">Property name</param>
		/// <param name="indexes">Index values</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>The value of the property</returns>
		public async Task<object?> GetIndexedProperty(string name, object[] indexes, CancellationToken cancellationToken)
		{
			return await this.Invoke(
				name,
				INVOKEKIND.INVOKE_PROPERTYGET,
				indexes,
				cancellationToken
				).ConfigureAwait(false);
		}

		/// <summary>
		/// Sets the value of a property.
		/// </summary>
		/// <param name="name">Property name</param>
		/// <param name="value">Value to set</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		public async Task SetProperty(string name, object? value, CancellationToken cancellationToken)
		{
			await this.Invoke(
				name,
				INVOKEKIND.INVOKE_PROPERTYPUT,
				new object[] { value },
				cancellationToken
				).ConfigureAwait(false);
		}

		private async Task<object?> Invoke(
			string memberName,
			INVOKEKIND invokeKind,
			object?[]? args,
			CancellationToken cancellationToken)
		{
			int memid = await this.GetMemberId(memberName, cancellationToken).ConfigureAwait(false);

			DceRpc.RpcPointer<DceRpc.RpcPointer<wireVARIANTStr>> pVarResult = new();
			DISPPARAMS dispParams = new DISPPARAMS();
			if (args != null)
			{
				dispParams.cArgs = (uint)args.Length;

				var argvars = new DceRpc.RpcPointer<wireVARIANTStr>[args.Length];
				for (int i = 0; i < args.Length; i++)
				{
					// Reverse the order because [MS-OAUT]
					argvars[args.Length - i - 1] = new DceRpc.RpcPointer<wireVARIANTStr>(ToVariant(args[i]));
				}
				dispParams.rgvarg = new DceRpc.RpcPointer<DceRpc.RpcPointer<wireVARIANTStr>[]>(argvars);
			}

			DceRpc.RpcPointer<uint> pArgErr = new();

			DceRpc.RpcPointer<DceRpc.RpcPointer<wireVARIANTStr>[]> rgVarRef = new(Array.Empty<DceRpc.RpcPointer<wireVARIANTStr>>());
			uint[] rgVarRefIdx = Array.Empty<uint>();
			uint CVarRef = 0;
			if (invokeKind == INVOKEKIND.INVOKE_PROPERTYPUT)
			{
				dispParams.cNamedArgs = 1;
				dispParams.rgdispidNamedArgs = new DceRpc.RpcPointer<int[]>(new int[] { (int)DISPID.PropPut });
			}

			DceRpc.RpcPointer<EXCEPINFO> pExcepInfo = new();
			var hr = (Hresult)await this._dispatch.Invoke(
				memid,
				Guid.Empty,
				Lcid,
				(uint)invokeKind,
				dispParams,
				pVarResult,
				pExcepInfo,
				pArgErr,
				CVarRef,
				rgVarRefIdx,
				rgVarRef,
				cancellationToken
				).ConfigureAwait(false);
			switch (hr)
			{
				case Hresult.DISP_E_EXCEPTION:
					throw new AutomationException(
						(Hresult)pExcepInfo.value.scode,
						pExcepInfo.value.wCode,
						pExcepInfo.value.bstrSource.DecodeString(),
						pExcepInfo.value.bstrDescription.DecodeString(),
						pExcepInfo.value.bstrHelpFile.DecodeString(),
						pExcepInfo.value.dwHelpContext
						);
				case Hresult.DISP_E_PARAMNOTFOUND:
					throw new DispatchArgumentException((int)pArgErr.value, "The DISPID of the parameter does not correspond to a parameter on the method.");
				case Hresult.DISP_E_TYPEMISMATCH:
					throw new DispatchArgumentException((int)pArgErr.value, "The argument could not be coerced into the type of the corresponding formal parameter.");
			}

			if ((int)hr < 0)
			{
				Hresult scode = hr;
				int appErrorCode = 0;
				if (((uint)hr & FACILITY_CONTROL) == FACILITY_CONTROL)
				{
					scode = 0;
					appErrorCode = (ushort)hr;
				}

				var errInfo = DcomClient.GetLastError() as DcomErrorInfo;
				if (errInfo != null)
				{
					throw new AutomationException(
						scode,
						appErrorCode,
						errInfo.Source,
						errInfo.Description,
						errInfo.HelpFile,
						errInfo.HelpContext
						);
				}
				else
				{
					throw new AutomationException(
						scode,
						appErrorCode,
						null,
						null,
						null,
						0
						);
				}
			}
			else
			{
				var obj = await VariantToObject(pVarResult.value.value, cancellationToken).ConfigureAwait(false);
				return obj;
			}
		}

		private wireVARIANTStr ToVariant(object? value)
		{
			// If tc != Empty, the value is non-null, but the compiler doesn't know that
			if (value is null)
				// Make the compiler happy
				return default;

			var tc = Convert.GetTypeCode(value);
			wireVARIANTStr v = new wireVARIANTStr
			{
				clSize = 6,
				_varUnion = tc switch
				{
					TypeCode.Empty => new Unnamed_1 { vt = (int)VARENUM.VT_EMPTY },
					TypeCode.DBNull => new Unnamed_1 { vt = (int)VARENUM.VT_NULL },
					TypeCode.Boolean => new Unnamed_1 { vt = (int)VARENUM.VT_BOOL, boolVal = ((bool)value) ? unchecked((short)0xFFFF) : (short)0 },
					TypeCode.SByte => new Unnamed_1 { vt = (int)VARENUM.VT_I1, bVal = (byte)(sbyte)value },
					TypeCode.Byte => new Unnamed_1 { vt = (int)VARENUM.VT_UI1, bVal = (byte)value },
					TypeCode.Int16 => new Unnamed_1 { vt = (int)VARENUM.VT_I2, iVal = (short)value },
					TypeCode.UInt16 => new Unnamed_1 { vt = (int)VARENUM.VT_UI2, uiVal = (ushort)value },
					TypeCode.Int32 => new Unnamed_1 { vt = (int)VARENUM.VT_I4, lVal = (int)value },
					TypeCode.UInt32 => new Unnamed_1 { vt = (int)VARENUM.VT_UI4, ulVal = (uint)value },
					TypeCode.Int64 => new Unnamed_1 { vt = (int)VARENUM.VT_I8, llVal = (long)value },
					TypeCode.UInt64 => new Unnamed_1 { vt = (int)VARENUM.VT_UI8, ullVal = (ulong)value },
					TypeCode.Single => new Unnamed_1 { vt = (int)VARENUM.VT_R4, fltVal = (float)value },
					TypeCode.Double => new Unnamed_1 { vt = (int)VARENUM.VT_R8, dblVal = (double)value },
					TypeCode.String => new Unnamed_1 { vt = (int)VARENUM.VT_BSTR, bstrVal = DcomClient.MakeString((string)value) },
					TypeCode.DateTime => new Unnamed_1 { vt = (int)VARENUM.VT_DATE, date = ((DateTime)value).ToOADate() },
					TypeCode.Object
					or TypeCode.Char
					or TypeCode.Decimal
						=> throw new NotImplementedException()
				}
			};
			v.vt = (ushort)v._varUnion.vt;
			return v;
		}

		private async Task<int> GetMemberId(string name, CancellationToken cancellationToken)
		{
			DceRpc.RpcPointer<int[]> rgDispId = new();
			var hr = (Hresult)await this._dispatch.GetIDsOfNames(Guid.Empty,
				new DceRpc.RpcPointer<string>[]{
					new DceRpc.RpcPointer<string>(name)
				},
				1,
				Lcid,
				rgDispId,
				cancellationToken).ConfigureAwait(false);
			if (hr is 0)
				return rgDispId.value[0];
			else if (hr is Hresult.DISP_E_UNKNOWNNAME)
				throw new UnknownMemberException(name);
			else
				throw DcomClient.GetExceptionFor(hr);
		}

		/// <summary>
		/// Invokes a method.
		/// </summary>
		/// <param name="methodName">Name of method</param>
		/// <param name="args">Method arguments</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>The value returned by the method</returns>
		public Task<object> InvokeMethod(string methodName, object[] args, CancellationToken cancellationToken)
		{
			// HACK: VBA passes both flags here to cover the syntax ambiguity of a method call vs. an indexed property access.
			return this.Invoke(methodName, INVOKEKIND.INVOKE_FUNC | INVOKEKIND.INVOKE_PROPERTYGET, args, cancellationToken);
		}
	}

	class OleAutomationDynamicMeta : DynamicMetaObject
	{
		private readonly OleAutomationObject _obj;

		internal OleAutomationDynamicMeta(Expression param, OleAutomationObject obj)
			: base(param, BindingRestrictions.Empty, obj)
		{
			this._obj = obj;
		}

		private static object? GetProperty(OleAutomationObject obj, string propertyName)
		{
			var value = obj.GetProperty(propertyName, CancellationToken.None).Result;
			return value;
		}
		private static object? GetIndexedProperty(OleAutomationObject obj, object[] indexes)
		{
			var value = obj.GetIndexedProperty("Item", indexes, CancellationToken.None).Result;
			return value;
		}
		private static object? SetProperty(OleAutomationObject obj, string propertyName, object? value)
		{
			obj.SetProperty(propertyName, value, CancellationToken.None).Wait();
			return value;
		}
		private static object? InvokeMethod(OleAutomationObject obj, string methodName, object[] args)
		{
			var value = obj.InvokeMethod(methodName, args, CancellationToken.None).Result;
			return value;
		}
		private static readonly MethodInfo getProperty = ReflectionHelper.MethodOf<OleAutomationObject, string, object?>((r, s) => GetProperty(r, s));
		private static readonly MethodInfo getIndex = ReflectionHelper.MethodOf<OleAutomationObject, object?>(r => GetIndexedProperty(r, null));
		private static readonly MethodInfo setProperty = ReflectionHelper.MethodOf<OleAutomationObject, string>((r, s) => SetProperty(r, s, null));
		private static readonly MethodInfo invokeMethod = ReflectionHelper.MethodOf<OleAutomationObject, string, object?>((r, s) => InvokeMethod(r, s, null));
		public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
		{
			return new DynamicMetaObject(Expression.Call(
				getProperty,
				new Expression[]
				{
					Expression.Convert(this.Expression,typeof(OleAutomationObject)),
					Expression.Constant(binder.Name)
				}),
				BindingRestrictions.GetTypeRestriction(this.Expression, typeof(OleAutomationObject))
				);
		}
		public override DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes)
		{
			Expression[] args = new Expression[indexes.Length + 1];
			args[0] = Expression.Convert(this.Expression, typeof(OleAutomationObject));
			indexes.CopyTo(args, 1);
			return new DynamicMetaObject(Expression.Call(
				getIndex,
				args),
				BindingRestrictions.GetTypeRestriction(this.Expression, typeof(OleAutomationObject))
				);
		}
		public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
		{
			return new DynamicMetaObject(
				Expression.Call(
				setProperty,
				new Expression[]
				{
					Expression.Convert(this.Expression,typeof(OleAutomationObject)),
					Expression.Constant(binder.Name),
					Expression.Convert(value.Expression, typeof(object))
				}),
				BindingRestrictions.GetTypeRestriction(this.Expression, typeof(OleAutomationObject)
				));
		}
		public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
		{
			return new DynamicMetaObject(Expression.Call(
				invokeMethod,
				new Expression[]
				{
					Expression.Convert(this.Expression,typeof(OleAutomationObject)),
					Expression.Constant(binder.Name),
					Expression.NewArrayInit(typeof(object), Array.ConvertAll(args, r => Expression.Convert(r.Expression, typeof(object))))
				}),
				BindingRestrictions.GetTypeRestriction(this.Expression, typeof(OleAutomationObject))
				);
		}
	}
}
