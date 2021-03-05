using ms_wmi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Mswmi
{
	/// <summary>
	/// Thrown when a WMI operation has failed.
	/// </summary>
	[Serializable]
	public class WmiException : Exception
	{
		/// <summary>
		/// Initializes a new <see cref="WmiException"/>.
		/// </summary>
		/// <param name="status">WMI status code</param>
		public WmiException(WBEMSTATUS status) : this(status, GetMessage(status)) { }

		/// <summary>
		/// Initializes a new <see cref="WmiException"/>.
		/// </summary>
		/// <param name="status">WMI status code</param>
		/// <param name="message">Message describing the error</param>
		public WmiException(WBEMSTATUS status, string message) : base(message)
		{
			this.HResult = (int)status;
			this.WmiStatusCode = status;
		}
		/// <summary>
		/// Initializes a new <see cref="WmiException"/> with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information</param>
		protected WmiException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
			this.WmiStatusCode = (WBEMSTATUS)info.GetInt32(nameof(WmiStatusCode));
		}

		/// <inheritdoc/>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(nameof(WmiStatusCode), (int)this.WmiStatusCode);
			base.GetObjectData(info, context);
		}

		/// <summary>
		/// Gets the WMI status code.
		/// </summary>
		public WBEMSTATUS WmiStatusCode { get; }

		private static string GetMessage(WBEMSTATUS status)
		{
			if (_messages.TryGetValue(status, out var message))
				return message;
			else
				return $"A WMI error has occurred.  Status: {status} (0x{((uint)status):X8})";
		}

		// [MS-WMI] § 2.2.11 WBEMSTATUS Enumeration
		private static Dictionary<WBEMSTATUS, string> _messages = new Dictionary<WBEMSTATUS, string>()
		{
			{ WBEMSTATUS.WBEM_E_FAILED, "The server has encountered an unknown error while processing the client's request." },
			{ WBEMSTATUS.WBEM_E_NOT_FOUND, "The object specified in the path does not exist." },
			{ WBEMSTATUS.WBEM_E_ACCESS_DENIED, "The permission required to perform the operation is not helped by the security principal performing the operation." },
			{ WBEMSTATUS.WBEM_E_PROVIDER_FAILURE, "The server has encountered an unknown error while processing the client's request." },
			{ WBEMSTATUS.WBEM_E_TYPE_MISMATCH, "The server has found an incorrect data type associated with property or input parameter in client's request." },
			{ WBEMSTATUS.WBEM_E_OUT_OF_MEMORY, "The server ran out of memory before completing the operation." },
			{ WBEMSTATUS.WBEM_E_INVALID_CONTEXT, "The IWbemContext object sent as part of client's request does not contain the required properties." },
			{ WBEMSTATUS.WBEM_E_INVALID_PARAMETER, "One or more of the parameters passed to the method is not valid. Methods return this error in any of the following circumstances: (1) a parameter is NULL where a non-NULL value is required, (2) the flags specified in the lFlags parameter are not allowed in this method." },
			{ WBEMSTATUS.WBEM_E_NOT_AVAILABLE, "The resource is unavailable." },
			{ WBEMSTATUS.WBEM_E_CRITICAL_ERROR, "The server has encountered a catastrophic failure and cannot process any client's request." },
			{ WBEMSTATUS.WBEM_E_NOT_SUPPORTED, "The attempted operation is not supported." },
			{ WBEMSTATUS.WBEM_E_PROVIDER_NOT_FOUND, "The server has encountered an implementation-specific error." },
			{ WBEMSTATUS.WBEM_E_INVALID_PROVIDER_REGISTRATION, "The server has encountered an implementation-specific error." },
			{ WBEMSTATUS.WBEM_E_PROVIDER_LOAD_FAILURE, "The server has encountered an implementation-specific error." },
			{ WBEMSTATUS.WBEM_E_INITIALIZATION_FAILURE, "The server has encountered failure during its initialization." },
			{ WBEMSTATUS.WBEM_E_TRANSPORT_FAILURE, "There is a network problem detected in reaching the server." },
			{ WBEMSTATUS.WBEM_E_INVALID_OPERATION, "The operation performed is not valid." },
			{ WBEMSTATUS.WBEM_E_ALREADY_EXISTS, "When a Put method is called for a CIM object with the flag WBEM_FLAG_CREATE_ONLY and the object already exists, WBEM_E_ALREADY_EXISTS is returned." },
			{ WBEMSTATUS.WBEM_E_UNEXPECTED, "An unspecified error has occurred." },
			{ WBEMSTATUS.WBEM_E_INCOMPLETE_CLASS, "The object passed doesn't correspond to any of classes registered with WMI." },
			{ WBEMSTATUS.WBEM_E_SHUTTING_DOWN, "The server cannot process the requested operation as it is shutting down." },
			{ WBEMSTATUS.E_NOTIMPL, "The attempted operation is not implemented. The value of this element is as specified in [MS-ERREF] section 2.1." },
			{ WBEMSTATUS.WBEM_E_INVALID_SUPERCLASS, "When putting a class, the server did not find the parent class specified for the new class to be added." },
			{ WBEMSTATUS.WBEM_E_INVALID_NAMESPACE, "When connecting to WMI, the namespace specified is not found." },
			{ WBEMSTATUS.WBEM_E_INVALID_OBJECT, "The CIM instance passed to the server doesn't have required information." },
			{ WBEMSTATUS.WBEM_E_INVALID_CLASS, "The class name is invalid." },
			{ WBEMSTATUS.WBEM_E_INVALID_QUERY, "The query sent to the server doesn't semantically conform to the rules specified in section 2.2.1." },
			{ WBEMSTATUS.WBEM_E_INVALID_QUERY_TYPE, "The query language specified is invalid." },
			{ WBEMSTATUS.WBEM_E_PROVIDER_NOT_CAPABLE, "The server does not support the requested operation on the given CIM class." },
			{ WBEMSTATUS.WBEM_E_CLASS_HAS_CHILDREN, "The class cannot be updated because it has derived classes." },
			{ WBEMSTATUS.WBEM_E_CLASS_HAS_INSTANCES, "The class cannot be updated because it has instances." },
			{ WBEMSTATUS.WBEM_E_ILLEGAL_NULL, "The server identifies that one of the non-nullable NULL properties was set to NULL in the Put operation." },
			{ WBEMSTATUS.WBEM_E_INVALID_CIM_TYPE, "The CIM type specified is not valid." },
			{ WBEMSTATUS.WBEM_E_INVALID_METHOD, "The CIM object does not implement the specified method." },
			{ WBEMSTATUS.WBEM_E_INVALID_METHOD_PARAMETERS, "One or more of the parameters passed to the CIM method are not valid." },
			{ WBEMSTATUS.WBEM_E_INVALID_PROPERTY, "The property for which the operation is made is no longer present in the CIM database." },
			{ WBEMSTATUS.WBEM_E_CALL_CANCELLED, "The server canceled the execution of the request due to resource constraints. The client can try the call again." },
			{ WBEMSTATUS.WBEM_E_INVALID_OBJECT_PATH, "The object path is not syntactically valid." },
			{ WBEMSTATUS.WBEM_E_OUT_OF_DISK_SPACE, "Insufficient resources on the server to satisfy the client's request." },
			{ WBEMSTATUS.WBEM_E_UNSUPPORTED_PUT_EXTENSION, "The server has encountered an implementation-specific error." },
			{ WBEMSTATUS.WBEM_E_QUOTA_VIOLATION, "Quota violation." },
			{ WBEMSTATUS.WBEM_E_SERVER_TOO_BUSY, "The server cannot complete the operation at this point." },
			{ WBEMSTATUS.WBEM_E_METHOD_NOT_IMPLEMENTED, "An attempt was made to execute a method not marked with \"implemented\" in this class or any of its derived classes." },
			{ WBEMSTATUS.WBEM_E_METHOD_DISABLED, "An attempt was made to execute a method marked with \"disabled\" qualifier in MOF." },
			{ WBEMSTATUS.WBEM_E_UNPARSABLE_QUERY, "The query sent to the server doesn't syntactically conform to the rules specified in section 2.2.1." },
			{ WBEMSTATUS.WBEM_E_NOT_EVENT_CLASS, "The FROM clause of WQL Event Query (section 2.2.1.2) represents a class that is not derived from Event." },
			{ WBEMSTATUS.WBEM_E_MISSING_GROUP_WITHIN, "The GROUP BY clause of WQL query does not have WITHIN specified." },
			{ WBEMSTATUS.WBEM_E_MISSING_AGGREGATION_LIST, "The GROUP BY clause was used with aggregation, which is not supported." },
			{ WBEMSTATUS.WBEM_E_PROPERTY_NOT_AN_OBJECT, "The GROUP BY clause references an object that is an embedded object without using Dot notation." },
			{ WBEMSTATUS.WBEM_E_AGGREGATING_BY_OBJECT, "The GROUP BY clause references an object that is an embedded object without using Dot notation." },
			{ WBEMSTATUS.WBEM_E_BACKUP_RESTORE_WINMGMT_RUNNING, "A request for backing up or restoring the CIM database was sent while the server was using it." },
			{ WBEMSTATUS.WBEM_E_QUEUE_OVERFLOW, "The EventQueue on the server has more events than can be consumed by the client." },
			{ WBEMSTATUS.WBEM_E_PRIVILEGE_NOT_HELD, "The server could not find the required privilege for performing operations on CIM classes or CIM instances." },
			{ WBEMSTATUS.WBEM_E_INVALID_OPERATOR, "An operator in the WQL query is invalid for this property type." },
			{ WBEMSTATUS.WBEM_E_CANNOT_BE_ABSTRACT, "The CIM class on the server had the abstract qualifier set to true, while its parent class does not have the abstract qualifier set to false." },
			{ WBEMSTATUS.WBEM_E_AMENDED_OBJECT, "A CIM instance with amended qualifier set to true is being updated without WBEM_FLAG_USE_AMENDED_QUALIFIERS flag." },
			{ WBEMSTATUS.WBEM_E_VETO_PUT, "The server cannot perform a PUT operation because it is not supported for the given CIM class." },
			{ WBEMSTATUS.WBEM_E_PROVIDER_SUSPENDED, "The server has encountered an implementation-specific error." },
			{ WBEMSTATUS.WBEM_E_ENCRYPTED_CONNECTION_REQUIRED, "The server has encountered an implementation-specific error." },
			{ WBEMSTATUS.WBEM_E_PROVIDER_TIMED_OUT, "WBEM_E_NO_KEY:  The IWbemServices::PuInstance or IWbemServices::PutInstanceAsync operation was attempted with no value set for the key properties." },
			{ WBEMSTATUS.WBEM_E_PROVIDER_DISABLED, "The server has encountered an implementation-specific error." },
			{ WBEMSTATUS.WBEM_E_REGISTRATION_TOO_BROAD, "The server has encountered an implementation-specific error." },
			{ WBEMSTATUS.WBEM_E_REGISTRATION_TOO_PRECISE, "The WQL query for intrinsic events for a class issued without a WITHIN clause." },
		};
	}
}
