using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Windows.Interop.ProjectedFileSystem
{

	using HRESULT = int;

	internal class NativeMethods
	{
		[DllImport("PROJECTEDFSLIB.dll", ExactSpelling = true), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[SupportedOSPlatform("windows10.0.17763")]
		internal static extern HRESULT PrjStartVirtualizing(
			[MarshalAs(UnmanagedType.LPWStr)] string virtualizationRootPath,
			ref readonly PRJ_CALLBACKS callbacks,
			[Optional] IntPtr instanceContext,
			[MarshalAs(UnmanagedType.LPArray)]
			[Optional] PRJ_STARTVIRTUALIZING_OPTIONS[] options,
			[MarshalAs(UnmanagedType.LPStruct)]
			out PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT namespaceVirtualizationContext
			);
	}

	[UnmanagedFunctionPointer(CallingConvention.Winapi)]
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal delegate HRESULT PRJ_START_DIRECTORY_ENUMERATION_CB(
		ref readonly PRJ_CALLBACK_DATA callbackData,
		ref readonly Guid enumerationId);

	[UnmanagedFunctionPointer(CallingConvention.Winapi)]
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal delegate HRESULT PRJ_END_DIRECTORY_ENUMERATION_CB(
		ref readonly PRJ_CALLBACK_DATA callbackData,
		ref readonly Guid enumerationId);
	[UnmanagedFunctionPointer(CallingConvention.Winapi)]
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal delegate HRESULT PRJ_GET_DIRECTORY_ENUMERATION_CB(
		ref readonly PRJ_CALLBACK_DATA callbackData,
		ref readonly Guid enumerationId,
		[MarshalAs(UnmanagedType.LPWStr)] string searchExpression,
		PRJ_DIR_ENTRY_BUFFER_HANDLE dirEntryBufferHandle);
	[UnmanagedFunctionPointer(CallingConvention.Winapi)]
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal delegate HRESULT PRJ_GET_PLACEHOLDER_INFO_CB(ref readonly PRJ_CALLBACK_DATA callbackData);

	[UnmanagedFunctionPointer(CallingConvention.Winapi)]
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal delegate HRESULT PRJ_GET_FILE_DATA_CB(
		ref readonly PRJ_CALLBACK_DATA callbackData,
		ulong byteOffset,
		uint length);

	[UnmanagedFunctionPointer(CallingConvention.Winapi)]
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal delegate HRESULT PRJ_QUERY_FILE_NAME_CB(ref readonly PRJ_CALLBACK_DATA callbackData);

	[UnmanagedFunctionPointer(CallingConvention.Winapi)]
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal delegate HRESULT PRJ_NOTIFICATION_CB(
		ref readonly PRJ_CALLBACK_DATA callbackData,
		[MarshalAs(UnmanagedType.Bool)] bool isDirectory,
		PRJ_NOTIFICATION notification,
		[MarshalAs(UnmanagedType.LPWStr)] string destinationFileName,
		ref PRJ_NOTIFICATION_PARAMETERS operationParameters);

	[UnmanagedFunctionPointer(CallingConvention.Winapi)]
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal delegate void PRJ_CANCEL_COMMAND_CB(ref readonly PRJ_CALLBACK_DATA callbackData);

	/// <summary>Extra parameters for notifications.</summary>
	/// <remarks>
	/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ns-projectedfslib-prj_notification_parameters">Learn more about this API from docs.microsoft.com</see>.</para>
	/// </remarks>
	[StructLayout(LayoutKind.Explicit)]
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal partial struct PRJ_NOTIFICATION_PARAMETERS
	{
		/// <summary></summary>
		[FieldOffset(0)]
		internal _PostCreate_e__Struct PostCreate;

		/// <summary></summary>
		[FieldOffset(0)]
		internal _FileRenamed_e__Struct FileRenamed;

		/// <summary></summary>
		[FieldOffset(0)]
		internal _FileDeletedOnHandleClose_e__Struct FileDeletedOnHandleClose;

		[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
		internal partial struct _PostCreate_e__Struct
		{
			internal PRJ_NOTIFY_TYPES NotificationMask;
		}

		[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
		internal partial struct _FileRenamed_e__Struct
		{
			internal PRJ_NOTIFY_TYPES NotificationMask;
		}

		[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
		internal partial struct _FileDeletedOnHandleClose_e__Struct
		{
			[MarshalAs(UnmanagedType.Bool)]
			internal bool IsFileModified;
		}
	}

	/// <summary>A notification value specified when sending the notification in a callback.</summary>
	/// <remarks>
	/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notification">Learn more about this API from docs.microsoft.com</see>.</para>
	/// </remarks>
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal enum PRJ_NOTIFICATION
	{
		/// <summary>
		/// <para>- Indicates that a handle has been created to an existing file or folder. - The provider can specify a new notification mask for this file or folder when responding to the notification.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notification#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFICATION_FILE_OPENED = 2,
		/// <summary>
		/// <para>- A new file or folder has been created. - The provider can specify a new notification mask for this file or folder when responding to the notification.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notification#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFICATION_NEW_FILE_CREATED = 4,
		/// <summary>
		/// <para>- An existing file has been overwritten or superceded. - The provider can specify a new notification mask for this file or folder when responding to the notification.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notification#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFICATION_FILE_OVERWRITTEN = 8,
		/// <summary>
		/// <para>- A file or folder is about to be deleted. - If the provider returns an error HRESULT code from the callback, the delete will not take effect.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notification#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFICATION_PRE_DELETE = 16,
		/// <summary>
		/// <para>- A file or folder is about to be renamed. - If the provider returns an error HRESULT code from the callback, the rename will not take effect. - If the callbackData-&gt;FilePathName parameter of <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nc-projectedfslib-prj_notification_cb">PRJ_NOTIFICATION_CB</a> is an empty string, this indicates that the rename is moving the file/directory from outside the virtualization instance. In that case, this notification will always be sent if the provider has registered a <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nc-projectedfslib-prj_notification_cb">PRJ_NOTIFICATION_CB</a> callback, even if the provider did not specify this bit when registering the subtree containing the destination path. However if the provider specified PRJ_NOTIFICATION_SUPPRESS_NOTIFICATIONS when registering the subtree containing the destination path, the notification will be suppressed. - If the destinationFileName parameter of <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nc-projectedfslib-prj_notification_cb">PRJ_NOTIFICATION_CB</a> is an empty string, this indicates that the rename is moving the file/folder out of the virtualization instance. - If both the callbackData-&gt;FilePathName and destinationFileName parameters of <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nc-projectedfslib-prj_notification_cb">PRJ_NOTIFICATION_CB</a> are non-empty strings, this indicates that the rename is within the virtualization instance. If the provider specified different notification masks for the source and destination paths in the NotificationMappings member of the options parameter of <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nf-projectedfslib-prjstartvirtualizing">PrjStartVirtualizing</a>, then this notification will be sent if the provider specified this bit when registering either the source or destination paths.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notification#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFICATION_PRE_RENAME = 32,
		/// <summary>
		/// <para>- A hard link is about to be created for the file. - If the provider returns an error HRESULT code from the callback, the hard link operation will not take effect. - If the callbackData-&gt;FilePathName parameter of <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nc-projectedfslib-prj_notification_cb">PRJ_NOTIFICATION_CB</a> is an empty string, this indicates that the hard link name will be created inside the virtualization instance, i.e. a new hard link is being created inside the virtualization instance to a file that exists outside the virtualization instance. In that case, this notification will always be sent if the provider has registered a <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nc-projectedfslib-prj_notification_cb">PRJ_NOTIFICATION_CB</a> callback, even if the provider did not specify this bit when registering the subtree where the new hard link name will be. However if the provider specified PRJ_NOTIFICATION_SUPPRESS_NOTIFICATIONS when registering the subtree containing the destination path, the notification will be suppressed. - If the destinationFileName parameter of <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nc-projectedfslib-prj_notification_cb">PRJ_NOTIFICATION_CB</a> is an empty string, this indicates that the hard link name will be created outside the virtualization instance, i.e. a new hard link is being created outside the virtualization instance for a file that exists inside the virtualization instance. - If both the callbackData-&gt;FilePathName and destinationFileName parameters of <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nc-projectedfslib-prj_notification_cb">PRJ_NOTIFICATION_CB</a> are non-empty strings, this indicates that the new hard link will be created within the virtualization instance for a file that exists within the virtualization instance. If the provider specified different notification masks for the source and destination paths in the NotificationMappings member of the options parameter of <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nf-projectedfslib-prjstartvirtualizing">PrjStartVirtualizing</a>, then this notification will be sent if the provider specified this bit when registering either the source or destination paths.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notification#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFICATION_PRE_SET_HARDLINK = 64,
		/// <summary>
		/// <para>- Indicates that a file/folder has been renamed. The file/folder may have been moved into the virtualization instance. - If the callbackData-&gt;FilePathName parameter of PRJ_NOTIFICATION_CB is an empty string, this indicates that the rename moved the file/directory from outside the virtualization instance. In that case ProjFS will always send this notification if the provider has registered a PRJ_NOTIFICATION_CB callback, even if the provider did not specify this bit when registering the subtree containing the destination path. - If the destinationFileName parameter of PRJ_NOTIFICATION_CB is an empty string, this indicates that the rename moved the file/directory out of the virtualization instance. - If both the callbackData-&gt;FilePathName and destinationFileName parameters of PRJ_NOTIFICATION_CB are non-empty strings, this indicates that the rename was within the virtualization instance. If the provider specified different notification masks for the source and destination paths in the NotificationMappings member of the options parameter of PrjStartVirtualizing, then ProjFS will send this notification if the provider specified this bit when registering either the source or destination paths. - The provider can specify a new notification mask for this file/directory when responding to the notification.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notification#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFICATION_FILE_RENAMED = 128,
		/// <summary>
		/// <para>- Indicates that a hard link has been created for the file. - If the callbackData-&gt;FilePathName parameter of PRJ_NOTIFICATION_CB is an empty string, this indicates that the hard link name was created inside the virtualization instance, i.e. a new hard link was created inside the virtualization instance to a file that exists outside the virtualization instance. In that case ProjFS will always send this notification if the provider has registered a PRJ_NOTIFICATION_CB callback, even if the provider did not specify this bit when registering the subtree where the new hard link name will be. - If the destinationFileName parameter of PRJ_NOTIFICATION_CB is an empty string, this indicates that the hard link name was created outside the virtualization instance, i.e. a new hard link was created outside the virtualization instance for a file that exists inside the virtualization instance. - If both the callbackData-&gt;FilePathName and destinationFileName parameters of PRJ_NOTIFICATION_CB are non-empty strings, this indicates that the a new hard link was created within the virtualization instance for a file that exists within the virtualization instance. If the provider specified different notification masks for the source and destination paths in the NotificationMappings member of the options parameter of PrjStartVirtualizing, then ProjFS will send this notification if the provider specified this bit when registering either the source or destination paths.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notification#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFICATION_HARDLINK_CREATED = 256,
		/// <summary>- A handle has been closed on the file/folder, and the file's content was not modified while the handle was open, and the file/folder was not deleted</summary>
		PRJ_NOTIFICATION_FILE_HANDLE_CLOSED_NO_MODIFICATION = 512,
		/// <summary>- A handle has been closed on the file, and that the file's content was modified while the handle was open.</summary>
		PRJ_NOTIFICATION_FILE_HANDLE_CLOSED_FILE_MODIFIED = 1024,
		/// <summary>
		/// <para>- A handle has been closed on the file/folder, and that it was deleted as part of closing the handle. - If the provider also registered to receive PRJ_NOTIFICATION_FILE_HANDLE_CLOSED_FILE_MODIFIED notifications, and the file was modified using the handle whose close resulted in deleting the file, then the operationParameters-&gt;FileDeletedOnHandleClose.IsFileModified parameter of <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nc-projectedfslib-prj_notification_cb">PRJ_NOTIFICATION_CB</a> will be TRUE. This applies only to files, not directories</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notification#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFICATION_FILE_HANDLE_CLOSED_FILE_DELETED = 2048,
		/// <summary>
		/// <para>- The file is about to be expanded from a placeholder to a full file, i.e. its contents are likely to be modified. - If the provider returns an error HRESULT code from the callback, the file will not be expanded to a full file, and the I/O that triggered the conversion will fail. - If there are multiple racing I/Os that would expand the same file, the provider will receive this notification value only once for the file.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notification#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFICATION_FILE_PRE_CONVERT_TO_FULL = 4096,
	}

	[DebuggerDisplay("{Value}")]
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal unsafe readonly partial struct PRJ_DIR_ENTRY_BUFFER_HANDLE
		: IEquatable<PRJ_DIR_ENTRY_BUFFER_HANDLE>
	{
		internal readonly void* Value;

		internal PRJ_DIR_ENTRY_BUFFER_HANDLE(void* value) => this.Value = value;

		internal PRJ_DIR_ENTRY_BUFFER_HANDLE(IntPtr value) : this((void*)value)
		{
		}

		internal static PRJ_DIR_ENTRY_BUFFER_HANDLE Null => default;

		internal bool IsNull => Value == default;

		public static implicit operator void*(PRJ_DIR_ENTRY_BUFFER_HANDLE value) => value.Value;

		public static explicit operator PRJ_DIR_ENTRY_BUFFER_HANDLE(void* value) => new PRJ_DIR_ENTRY_BUFFER_HANDLE(value);

		public static bool operator ==(PRJ_DIR_ENTRY_BUFFER_HANDLE left, PRJ_DIR_ENTRY_BUFFER_HANDLE right) => left.Value == right.Value;

		public static bool operator !=(PRJ_DIR_ENTRY_BUFFER_HANDLE left, PRJ_DIR_ENTRY_BUFFER_HANDLE right) => !(left == right);

		public bool Equals(PRJ_DIR_ENTRY_BUFFER_HANDLE other) => this.Value == other.Value;

		public override bool Equals(object obj) => obj is PRJ_DIR_ENTRY_BUFFER_HANDLE other && this.Equals(other);

		public override int GetHashCode() => unchecked((int)this.Value);

		public override string ToString() => $"0x{(nuint)this.Value:x}";

		public static implicit operator IntPtr(PRJ_DIR_ENTRY_BUFFER_HANDLE value) => new IntPtr(value.Value);

		public static explicit operator PRJ_DIR_ENTRY_BUFFER_HANDLE(IntPtr value) => new PRJ_DIR_ENTRY_BUFFER_HANDLE(value.ToPointer());

		public static explicit operator PRJ_DIR_ENTRY_BUFFER_HANDLE(UIntPtr value) => new PRJ_DIR_ENTRY_BUFFER_HANDLE(value.ToPointer());
	}

	/// <summary>A set of callback routines to where the provider stores its implementation of the callback.</summary>
	/// <remarks>
	/// <para>The provider must supply implementations for StartDirectoryEnumerationCallback, EndDirectoryEnumerationCallback, GetDirectoryEnumerationCallback, GetPlaceholderInformationCallback, and GetFileDataCallback.</para>
	/// <para>The QueryFileNameCallback, NotifyOperationCallback, and CancelCommandCallback callbacks are optional. </para>
	/// <para>This doc was truncated.</para>
	/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ns-projectedfslib-prj_callbacks#">Read more on docs.microsoft.com</see>.</para>
	/// </remarks>
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal partial struct PRJ_CALLBACKS
	{
		/// <summary>A pointer to the StartDirectoryEnumerationCallback.</summary>
		internal PRJ_START_DIRECTORY_ENUMERATION_CB StartDirectoryEnumerationCallback;

		/// <summary>A pointer to the EndDirectoryEnumerationCallback.</summary>
		internal PRJ_END_DIRECTORY_ENUMERATION_CB EndDirectoryEnumerationCallback;

		/// <summary>A pointer to the GetDirectoryEnumerationCallback.</summary>
		internal PRJ_GET_DIRECTORY_ENUMERATION_CB GetDirectoryEnumerationCallback;

		/// <summary>A pointer to the GetPlaceholderInformationCallback.</summary>
		internal PRJ_GET_PLACEHOLDER_INFO_CB GetPlaceholderInfoCallback;

		/// <summary>A pointer to the GetFileDataCallback.</summary>
		internal PRJ_GET_FILE_DATA_CB GetFileDataCallback;

		/// <summary>A pointer to the QueryFileNameCallback.</summary>
		internal PRJ_QUERY_FILE_NAME_CB QueryFileNameCallback;

		/// <summary>A pointer to the NotifyOperationCallback.</summary>
		internal PRJ_NOTIFICATION_CB NotificationCallback;

		/// <summary>A pointer to the CancelCommandCallback.</summary>
		internal PRJ_CANCEL_COMMAND_CB CancelCommandCallback;
	}

	/// <summary>Options to provide when starting a virtualization instance.</summary>
	/// <remarks>
	/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ns-projectedfslib-prj_startvirtualizing_options">Learn more about this API from docs.microsoft.com</see>.</para>
	/// </remarks>
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal partial struct PRJ_STARTVIRTUALIZING_OPTIONS
	{
		/// <summary>A flag for starting virtualization.</summary>
		internal PRJ_STARTVIRTUALIZING_FLAGS Flags;

		/// <summary>The number of threads the provider wants to create to service callbacks.</summary>
		internal uint PoolThreadCount;

		/// <summary>The maximum number of threads the provider wants to run concurrently to process callbacks.</summary>
		internal uint ConcurrentThreadCount;

		/// <summary>An array of zero or more notification mappings. See the Remarks section of PRJ_NOTIFICATION MAPPING for more details.</summary>
		[MarshalAs(UnmanagedType.LPArray)]
		internal unsafe PRJ_NOTIFICATION_MAPPING[] NotificationMappings;

		/// <summary>The number of notification mappings provided in NotificationMappings.</summary>
		internal uint NotificationMappingsCount;
	}

	/// <summary>Types of notifications describing a change to the file or folder.</summary>
	/// <remarks>
	/// <para>ProjFS can send notifications of file system activity to a provider. When the provider starts a virtualization instance it specifies which notifications it wishes to receive. It may also specify a new set of notifications for a file when it is created or renamed. The provider must register a PRJ_NOTIFICATION_CB notification callback routine in the callbacks parameter of PrjStartVirtualizing in order to receive notifications. ProjFS sends notifications for files and directories within an active virtualization root. That is, ProjFS will send notifications for the virtualization root and its descendants. Symbolic links and junctions within the virtualization root are not traversed when determining what constitutes a descendant of the virtualization root. ProjFS sends notifications only for the primary data stream of a file. ProjFS does not send notifications for operations on alternate data streams. ProjFS does not send notifications for an inactive virtualization instance. A virtualization instance is inactive if any one of the following is true: - The provider has not yet started it by calling <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nf-projectedfslib-prjstartvirtualizing">PrjStartVirtualizing</a>. - The provider has stopped the instance by calling <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nf-projectedfslib-prjstopvirtualizing">PrjStopVirtualizing</a>. - The provider process has exited The provider may specify which notifications it wishes to receive when starting a virtualization instance, or in response to a notification that allows a new notification mask to be set. The provider specifies a default set of notifications that it wants ProjFS to send for the virtualization instance when it starts the instance. This set of notifications is provided in the NotificationMappings member of the options parameter of <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nf-projectedfslib-prjstartvirtualizing">PrjStartVirtualizing</a>, which may specify different notification masks for different subtrees of the virtualization instance. The provider may choose to supply a different notification mask in response to a notification of file open, create, supersede/overwrite, or rename. ProjFS will continue to send these notifications for the given file until all handles to the file are closed. After that it will revert to the default set of notifications. Naturally if the default set of notifications does not specify that ProjFS should notify for open, create, etc., the provider will not get the opportunity to specify a different mask for those operations.</para>
	/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notify_types#">Read more on docs.microsoft.com</see>.</para>
	/// </remarks>
	[Flags]
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal enum PRJ_NOTIFY_TYPES : uint
	{
		/// <summary>No notification.</summary>
		PRJ_NOTIFY_NONE = 0x00000000,
		/// <summary>
		/// <para>If specified on virtualization instance start: - This indicates that notifications should not be sent for the virtualization instance, or a specified subtree of the instance. If specified in response to a notification: - This indicates that notifications should not be sent for the specified file or folder until all handles to it are closed. <div class="alert"><b>Note</b>  If this bit appears in a notification mask, it overrides all other bits in the mask. For example, a valid mask with this bit is treated as containing only PRJ_NOTIFY_SUPPRESS_NOTIFICATIONS.</div> <div> </div></para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notify_types#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFY_SUPPRESS_NOTIFICATIONS = 0x00000001,
		/// <summary>
		/// <para>If specified on virtualization instance start: - This indicates that the provider should be notified when a handle is created to an existing file or folder. If specified in response to a notification: - This indicates that the provider should be notified if any further handles are created to the file or folder.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notify_types#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFY_FILE_OPENED = 0x00000002,
		/// <summary>
		/// <para>If specified on virtualization instance start: - The provider should be notified when a new file or folder is created. If specified in response to a notification: - No effect.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notify_types#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFY_NEW_FILE_CREATED = 0x00000004,
		/// <summary>
		/// <para>If specified on virtualization instance start: - Indicates that the provider should be notified when an existing when an existing file is overwritten or superceded. If specified in response to a notification: - Indicates that the provider should be notified when the file or folder is overwritten or superceded.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notify_types#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFY_FILE_OVERWRITTEN = 0x00000008,
		/// <summary>
		/// <para>If specified on virtualization instance start: - Indicates that the provider should be notified when a file or folder is about to be deleted. If specified in response to a notification: - Indicates that the provider should be notified when a file or folder is about to be deleted.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notify_types#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFY_PRE_DELETE = 0x00000010,
		/// <summary>
		/// <para>If specified on virtualization instance start: - Indicates that the provider should be notified when a file or folder is about to be renamed. If specified in response to a notification: - Indicates that the provider should be notified when a file or folder is about to be renamed.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notify_types#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFY_PRE_RENAME = 0x00000020,
		/// <summary>
		/// <para>If specified on virtualization instance start: - Indicates that the provider should be notified when a hard link is about to be created for a file. If specified in response to a notification: - Indicates that the provider should be notified when a hard link is about to be created for a file.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notify_types#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFY_PRE_SET_HARDLINK = 0x00000040,
		/// <summary>
		/// <para>If specified on virtualization instance start: - Indicates that the provider should be notified that a file or folder has been renamed. If specified in response to a notification: - Indicates that the provider should be notified when a file or folder has been renamed.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notify_types#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFY_FILE_RENAMED = 0x00000080,
		/// <summary>
		/// <para>If specified on virtualization instance start: - Indicates that the provider should be notified that a hard link has been created for a file. If specified in response to a notification: - Indicates that the provider should be notified that a hard link has been created for the file.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notify_types#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFY_HARDLINK_CREATED = 0x00000100,
		/// <summary>
		/// <para>If specified on virtualization instance start: - The provider should be notified when a handle is closed on a file/folder and the closing handle neither modified nor deleted it. If specified in response to a notification: - The provider should be notified when handles are closed for the file/folder and there were no modifications or deletions associated with the closing handle.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notify_types#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFY_FILE_HANDLE_CLOSED_NO_MODIFICATION = 0x00000200,
		/// <summary>
		/// <para>If specified on virtualization instance start: - The provider should be notified when a handle is closed on a file/folder and the closing handle was used to modify it. If specified in response to a notification: - The provider should be notified when a handle is closed on the file/folder and the closing handle was used to modify it.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notify_types#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFY_FILE_HANDLE_CLOSED_FILE_MODIFIED = 0x00000400,
		/// <summary>
		/// <para>If specified on virtualization instance start: - The provider should be notified when a handle is closed on a file/folder and it is deleted as part of closing the handle. If specified in response to a notification: - The provider should be notified when a handle is closed on the file/folder and it is deleted as part of closing the handle.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notify_types#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFY_FILE_HANDLE_CLOSED_FILE_DELETED = 0x00000800,
		/// <summary>
		/// <para>If specified on virtualization instance start: - The provider should be notified when it is about to convert a placeholder to a full file. If specified in response to a notification: - The provider should be notified when it is about to convert the placeholder to a full file, assuming it is a placeholder and not already a full file.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notify_types#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFY_FILE_PRE_CONVERT_TO_FULL = 0x00001000,
		/// <summary>
		/// <para>If specified on virtualization instance start: - This value is not valid on virtualization instance start. If specified in response to a notification: -  Continue to use the existing set of notifications for this file/folder.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_notify_types#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_NOTIFY_USE_EXISTING_MASK = 0xFFFFFFFF,
	}

	/// <summary>Describes a notification mapping, which is a pairing between a directory (referred to as a &quot;notification root&quot;) and a set of notifications, expressed as a bit mask.</summary>
	/// <remarks>
	/// <para>PRJ_NOTIFICATION_MAPPING describes a "notification mapping", which is a pairing between a directory (referred to as a "notification root") and a set of notifications, expressed as a bit mask, which ProjFS should send for that directory and its descendants. A notification mapping can also be established for a single file.</para>
	/// <para>The provider puts an array of zero or more PRJ_NOTIFICATION_MAPPING structures in the NotificationMappings member of the options parameter of PrjStartVirtualizing to configure notifications for the virtualization root.</para>
	/// <para>If the provider does not specify any notification mappings, ProjFS will default to sending the notifications PRJ_NOTIFICATION_FILE_OPENED, PRJ_NOTIFICATION_NEW_FILE_CREATED, and PRJ_NOTIFICATION_FILE_OVERWRITTEN for all files and directories in the virtualization instance.</para>
	/// <para>The directory or file is specified relative to the virtualization root, with an empty string representing the virtualization root itself.</para>
	/// <para>If the provider specifies multiple notification mappings, and some are descendants of others, the mappings must be specified in descending depth. Notification mappings at deeper levels override higher-level ones for their descendants.</para>
	/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ns-projectedfslib-prj_notification_mapping#">Read more on docs.microsoft.com</see>.</para>
	/// </remarks>
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal partial struct PRJ_NOTIFICATION_MAPPING
	{
		/// <summary>A bit mask representing a set of notifications.</summary>
		internal PRJ_NOTIFY_TYPES NotificationBitMask;

		/// <summary>The directory that the notification mapping is paired to.</summary>
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string NotificationRoot;
	}

	/// <summary>Flags to provide when starting a virtualization instance.</summary>
	/// <remarks>
	/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_startvirtualizing_flags">Learn more about this API from docs.microsoft.com</see>.</para>
	/// </remarks>
	[Flags]
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal enum PRJ_STARTVIRTUALIZING_FLAGS
	{
		/// <summary>No flags.</summary>
		PRJ_FLAG_NONE = 0x00000000,
		/// <summary>
		/// <para>Specifies that ProjFS should maintain a "negative path cache" for the virtualization instance. If the negative path cache is active, then if the provider indicates that a file path does not exist by returning HRESULT_FROM_WIN32(ERROR_FILE_NOT_FOUND) from its <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nc-projectedfslib-prj_get_placeholder_info_cb">PRJ_GET_PLACEHOLDER_INFO_CB</a> callback, ProjFS will fail subsequent opens of that path without calling the <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nc-projectedfslib-prj_get_placeholder_info_cb">PRJ_GET_PLACEHOLDER_INFO_CB</a> callback again. To resume receiving the <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nc-projectedfslib-prj_get_placeholder_info_cb">PRJ_GET_PLACEHOLDER_INFO_CB</a> for paths the provider has indicated do not exist, the provider must call <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nf-projectedfslib-prjclearnegativepathcache">PrjClearNegativePathCache</a>.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_startvirtualizing_flags#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		PRJ_FLAG_USE_NEGATIVE_PATH_CACHE = 0x00000001,
	}

	[DebuggerDisplay("{Value}")]
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal unsafe readonly partial struct PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT
		: IEquatable<PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT>
	{
		internal readonly void* Value;

		internal PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT(void* value) => this.Value = value;

		internal PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT(IntPtr value) : this((void*)value)
		{
		}

		internal static PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT Null => default;

		internal bool IsNull => Value == default;

		public static implicit operator void*(PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT value) => value.Value;

		public static explicit operator PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT(void* value) => new PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT(value);

		public static bool operator ==(PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT left, PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT right) => left.Value == right.Value;

		public static bool operator !=(PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT left, PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT right) => !(left == right);

		public bool Equals(PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT other) => this.Value == other.Value;

		public override bool Equals(object obj) => obj is PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT other && this.Equals(other);

		public override int GetHashCode() => unchecked((int)this.Value);

		public override string ToString() => $"0x{(nuint)this.Value:x}";

		public static implicit operator IntPtr(PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT value) => new IntPtr(value.Value);

		public static explicit operator PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT(IntPtr value) => new PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT(value.ToPointer());

		public static explicit operator PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT(UIntPtr value) => new PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT(value.ToPointer());
	}

	/// <summary>Flags controlling what is returned in the enumeration.</summary>
	/// <remarks>
	/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ne-projectedfslib-prj_callback_data_flags">Learn more about this API from docs.microsoft.com</see>.</para>
	/// </remarks>
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal enum PRJ_CALLBACK_DATA_FLAGS
	{
		/// <summary>Start the scan at the first entry in the directory.</summary>
		PRJ_CB_DATA_FLAG_ENUM_RESTART_SCAN = 1,
		/// <summary>Return only one entry from the enumeration.</summary>
		PRJ_CB_DATA_FLAG_ENUM_RETURN_SINGLE_ENTRY = 2,
	}

	/// <summary>Information that uniquely identifies the contents of a placeholder file.</summary>
	/// <remarks>
	/// <para>A provider uses <b>PRJ_PLACEHOLDER_VERSION_INFO</b> to provide information that uniquely identifies the contents of a placeholder file. ProjFS stores the contents of this struct with the file and returns it when invoking callbacks.</para>
	/// <para><b>PRJ_PLACEHOLDER_VERSION_INFO</b>.ProviderID is a provider-specific identifier. The provider may use this value as its own unique identifier, for example as a version number for the format of the ContentID field.</para>
	/// <para><b>PRJ_PLACEHOLDER_VERSION_INFO</b>.ContentID is a content identifier, generated by the provider. This value is used to distinguish different versions of the same file, i.e. different file contents and/or metadata (e.g. timestamps) for the same file path.</para>
	/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ns-projectedfslib-prj_placeholder_version_info#">Read more on docs.microsoft.com</see>.</para>
	/// </remarks>
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal partial struct PRJ_PLACEHOLDER_VERSION_INFO
	{
		/// <summary>A provider specific identifier.</summary>
		internal Guid ProviderID;

		/// <summary>A content identifier, generated by the provider.</summary>
		internal Guid ContentID;
	}

	/// <summary>Defines the standard information passed to a provider for every operation callback.</summary>
	/// <remarks>
	/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ns-projectedfslib-prj_callback_data">Learn more about this API from docs.microsoft.com</see>.</para>
	/// </remarks>
	[System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.3.183+73e6125f79.RR")]
	internal partial struct PRJ_CALLBACK_DATA
	{
		/// <summary>Size in bytes of this structure. The provider must not attempt to access any field of this structure that is located beyond this value.</summary>
		internal uint Size;

		/// <summary>Callback-specific flags.</summary>
		internal PRJ_CALLBACK_DATA_FLAGS Flags;

		/// <summary>Opaque handle to the virtualization instance that is sending the callback.</summary>
		internal PRJ_NAMESPACE_VIRTUALIZATION_CONTEXT NamespaceVirtualizationContext;

		/// <summary>
		/// <para>A value that uniquely identifies a particular invocation of a callback. The provider uses this value:</para>
		/// <para></para>
		/// <para>This doc was truncated.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ns-projectedfslib-prj_callback_data#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		internal int CommandId;

		/// <summary>A value that uniquely identifies the file handle for the callback.</summary>
		internal Guid FileId;

		/// <summary>A value that uniquely identifies an open data stream for the callback.</summary>
		internal Guid DataStreamId;

		/// <summary>The path to the target file. This is a null-terminated string of Unicode characters. This path is always specified relative to the virtualization root.</summary>
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string FilePathName;

		/// <summary>Version information if the target of the callback is a placeholder or partial file.</summary>
		internal unsafe PRJ_PLACEHOLDER_VERSION_INFO* VersionInfo;

		/// <summary>
		/// <para>The process identifier for the process that triggered this callback. If this information is not available, this will be 0. Callbacks that supply this information include: <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nc-projectedfslib-prj_get_placeholder_info_cb">PRJ_GET_PLACEHOLDER_INFO_CB</a>, <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nc-projectedfslib-prj_get_file_data_cb">PRJ_GET_FILE_DATA_CB</a>, and <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nc-projectedfslib-prj_notification_cb">PRJ_NOTIFICATION_CB</a>.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ns-projectedfslib-prj_callback_data#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		internal uint TriggeringProcessId;

		/// <summary>A null-terminated Unicode string specifying the image file name corresponding to TriggeringProcessId. If TriggeringProcessId is 0 this will be NULL.</summary>
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string TriggeringProcessImageFileName;

		/// <summary>
		/// <para>A pointer to context information defined by the provider. The provider passes this context in the instanceContext parameter of <a href="https://docs.microsoft.com/windows/desktop/api/projectedfslib/nf-projectedfslib-prjstartvirtualizing">PrjStartVirtualizing</a>.</para>
		/// <para>If the provider did not specify such a context, this value will be NULL.</para>
		/// <para><see href="https://learn.microsoft.com/windows/win32/api/projectedfslib/ns-projectedfslib-prj_callback_data#members">Read more on docs.microsoft.com</see>.</para>
		/// </summary>
		internal unsafe void* InstanceContext;
	}
}
