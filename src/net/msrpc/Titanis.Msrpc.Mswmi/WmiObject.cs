using ms_dcom;
using ms_wmi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.Msrpc.Msdcom;

namespace Titanis.Msrpc.Mswmi
{
	/// <summary>
	/// Represents a WMI object.
	/// </summary>
	/// <remarks>
	/// A WMI object may be an instance of <see cref="WmiClassObject"/> or <see cref="WmiInstanceObject"/>,
	/// as indicated by <see cref="IsClass"/> or <see cref="IsInstance"/>.
	/// </remarks>
	public abstract class WmiObject : IWbemClassObject, ICustomDcomMarshal
	{
		/// <summary>
		/// Gets the <see cref="WmiScope"/> containing the object.
		/// </summary>
		[Browsable(false)]
		public WmiScope? Scope { get; internal set; }

		private protected WmiScope VerifyAttached()
		{
			if (this.Scope is null)
				throw new InvalidOperationException("The object is not attached to a WMI scope.");

			return this.Scope;
		}

		/// <summary>
		/// Gets the MOF representation of the object.
		/// </summary>
		/// <returns></returns>
		public abstract string ToMof();
		internal abstract void EncodeObjectBlockTo(ByteWriter writer);

		/// <summary>
		/// Gets the path of the object relative to the scope.
		/// </summary>
		public abstract string RelativePath { get; }

		internal abstract WmiObjectFlags ObjectFlags { get; }
		/// <summary>
		/// Gets the type of WMI object.
		/// </summary>
		public WmiObjectFlags ObjectType => (this.ObjectFlags & WmiObjectFlags.ObjectTypeMask);
		/// <summary>
		/// Gets a value indicating whether this object is a class.
		/// </summary>
		/// <remarks>
		/// A class object may be cast to <see cref="WmiClassObject"/>.
		/// </remarks>
		[Browsable(false)]
		public bool IsInstance => 0 != (this.ObjectFlags & WmiObjectFlags.CimInstance);
		/// <summary>
		/// Gets a value indicating whether this object is an instance.
		/// </summary>
		/// <remarks>
		/// An instance object may be cast to <see cref="WmiInstanceObject"/>.
		/// </remarks>
		[Browsable(false)]
		public bool IsClass => 0 != (this.ObjectFlags & WmiObjectFlags.CimClass);

		[Browsable(false)]
		public abstract WmiQualifier[] Qualifiers { get; }



		public abstract WmiMethod? GetMethod(string methodName);
		public async Task<WmiInstanceObject> InvokeMethodAsync(
			string methodName,
			Dictionary<string, object?>? args,
			CancellationToken cancellationToken
			)
		{
			if (string.IsNullOrEmpty(methodName)) throw new ArgumentException($"'{nameof(methodName)}' cannot be null or empty.", nameof(methodName));

			var method = this.GetMethod(methodName);
			if (method == null)
				throw new ArgumentException($"Class '{this.RelativePath}' does not have a method named '{methodName}'.", nameof(methodName));

			this.VerifyAttached();

			WmiInstanceObject? inputObj = (args != null) ? (method.InputSignature?.Instantiate(args)) : null;
			var result = await this.VerifyAttached().ExecuteMethodAsync(this.RelativePath, methodName, inputObj, cancellationToken).ConfigureAwait(false);
			return result;
		}





		Objref ICustomDcomMarshal.CreateObjref() => new WmiObjref(this);

		Task<int> IUnknown.Opnum0NotUsedOnWire(CancellationToken cancellationToken) => throw new NotImplementedException();
		Task<int> IUnknown.Opnum1NotUsedOnWire(CancellationToken cancellationToken) => throw new NotImplementedException();
		Task<int> IUnknown.Opnum2NotUsedOnWire(CancellationToken cancellationToken) => throw new NotImplementedException();
	}
}
