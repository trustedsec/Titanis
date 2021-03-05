# Tool Index

This index lists the tools by command name as well as by task.

* [List of tools by name](#tools-by-name)
* [List of tools by task](#tools-by-task)

# Tools by Name

|Command|Description|
|-|-|
|[CredCoerce](CredCoerce.md#credcoerce)|Sends RPC calls to coerce a system to authenticate to a remote system|
|[Epm](Epm.md#epm)|Commands for interacting with the RPC endpoint mapper|
|[Epm lsep](Epm.md#epm-lsep)|Lists the dynamic RPC endpoints registered with the endpoint mapper|
|[Kerb](Kerb.md#kerb)|Commands for working with Kerberos authentication|
|[Kerb asreq](Kerb.md#kerb-asreq)|Requests a TGT from the KDC.|
|[Kerb getasinfo](Kerb.md#kerb-getasinfo)|Gets server time and encryption types (with salts) for a user account.|
|[Kerb select](Kerb.md#kerb-select)|Selects and displays tickets from a file.|
|[Kerb tgsreq](Kerb.md#kerb-tgsreq)|Requests a ticket from the KDC.|
|[Lsa](Lsa.md#lsa)|Commands for interacting with the LSA|
|[Lsa addpriv](Lsa.md#lsa-addpriv)|Adds one or more privileges to an account|
|[Lsa createaccount](Lsa.md#lsa-createaccount)|Creates an account|
|[Lsa enumaccounts](Lsa.md#lsa-enumaccounts)|Enumerates accounts|
|[Lsa enumprivaccounts](Lsa.md#lsa-enumprivaccounts)|Enumerates accounts that have a specific privilege or user right|
|[Lsa getprivs](Lsa.md#lsa-getprivs)|Gets the privileges assigned to an account.|
|[Lsa getrights](Lsa.md#lsa-getrights)|Gets the user rights and privileges granted to an account|
|[Lsa getsysaccess](Lsa.md#lsa-getsysaccess)|Gets the system access rights granted to an account|
|[Lsa lookupname](Lsa.md#lsa-lookupname)|Gets the SID for one or more account names|
|[Lsa lookupsid](Lsa.md#lsa-lookupsid)|Translates one or more SIDs to their account names|
|[Lsa rmpriv](Lsa.md#lsa-rmpriv)|Removes one or more privileges from an account|
|[Lsa setsysaccess](Lsa.md#lsa-setsysaccess)|Sets the system access rights granted to an account|
|[Lsa whoami](Lsa.md#lsa-whoami)|Gets the name and domain of the connected user|
|[Sam](Sam.md#sam)|Commands for interacting with a remote Security Accounts Manager|
|[Sam enumusers](Sam.md#sam-enumusers)|Enumerates the users|
|[Scm](Scm.md#scm)|Provides functionality for interacting with the service control manager on a remote Windows system|
|[Scm create](Scm.md#scm-create)|Creates and optionally starts a new service|
|[Scm delete](Scm.md#scm-delete)|Deletes a service|
|[Scm qtriggers](Scm.md#scm-qtriggers)|Queries the status of a service|
|[Scm query](Scm.md#scm-query)|Queries the status of a service|
|[Scm start](Scm.md#scm-start)|Starts a service|
|[Scm stop](Scm.md#scm-stop)|Stops a service|
|[Smb2Client](Smb2Client.md#smb2client)|Performs operations on an SMB2 server.|
|[Smb2Client enumnics](Smb2Client.md#smb2client-enumnics)|Queries the server for a list of network interfaces.|
|[Smb2Client enumopenfiles](Smb2Client.md#smb2client-enumopenfiles)|Lists files open on the server.|
|[Smb2Client enumsessions](Smb2Client.md#smb2client-enumsessions)|Lists active sessions on the server.|
|[Smb2Client enumshares](Smb2Client.md#smb2client-enumshares)|Lists shares on the server|
|[Smb2Client enumsnapshots](Smb2Client.md#smb2client-enumsnapshots)|Lists the available snapshots for a file or directory.|
|[Smb2Client enumstreams](Smb2Client.md#smb2client-enumstreams)|Lists the data streams of a file or directory.|
|[Smb2Client get](Smb2Client.md#smb2client-get)|Gets the contents of a file.|
|[Smb2Client ls](Smb2Client.md#smb2client-ls)|Lists the contents of a directory (including named pipes).|
|[Smb2Client mkdir](Smb2Client.md#smb2client-mkdir)|Creates a directory.|
|[Smb2Client mklink](Smb2Client.md#smb2client-mklink)|Creates a symbolic link.|
|[Smb2Client mount](Smb2Client.md#smb2client-mount)|Creates a mount point or junction.|
|[Smb2Client put](Smb2Client.md#smb2client-put)|Sends a file to the server.|
|[Smb2Client rm](Smb2Client.md#smb2client-rm)|Deletes a file.|
|[Smb2Client rmdir](Smb2Client.md#smb2client-rmdir)|Deletes a directory.|
|[Smb2Client umount](Smb2Client.md#smb2client-umount)|Unmounts a mount point.|
|[Smb2Client watch](Smb2Client.md#smb2client-watch)|Watches for modifications to a directory or subtree.|
|[Wmi](Wmi.md#wmi)|Commands for interacting with the Windows Management Instrumentation service|
|[Wmi backup](Wmi.md#wmi-backup)|Backs up the WMI repository|
|[Wmi exec](Wmi.md#wmi-exec)|Executes a command on a remote system via WMI|
|[Wmi get](Wmi.md#wmi-get)|Gets an object with a WMI path|
|[Wmi invoke](Wmi.md#wmi-invoke)|Invokes a method on a WMI class or object|
|[Wmi lsclass](Wmi.md#wmi-lsclass)|Lists the classes within a namespace.|
|[Wmi lsmethod](Wmi.md#wmi-lsmethod)|Lists the methods of a class or object.|
|[Wmi lsns](Wmi.md#wmi-lsns)|Lists the available namespaces within a namespace.|
|[Wmi lsprop](Wmi.md#wmi-lsprop)|Lists the properties of a class or object.|
|[Wmi query](Wmi.md#wmi-query)|Executes a WMI query|
|[Wmi restore](Wmi.md#wmi-restore)|Restores the WMI repository|


# Tools by Task

|Task|Command|
|-|-|
|****|
|Coerce a system to authenticate to a remote target|[CredCoerce](CredCoerce.md#credcoerce)|
|**Enumeration**|
|Check whether a user name is valid|[Kerb getasinfo](Kerb.md#kerb-getasinfo)|
|Check whether a user name is valid|[Kerb asreq](Kerb.md#kerb-asreq)|
|Enumerate policy accounts|[Lsa enumaccounts](Lsa.md#lsa-enumaccounts)|
|Enumerate the accounts in the Security Accounts Manager database|[Sam enumusers](Sam.md#sam-enumusers)|
|Enumerate the data streams of a file on an SMB server|[Smb2Client enumstreams](Smb2Client.md#smb2client-enumstreams)|
|Enumerate the network interfaces and network addresses of an SMB server|[Smb2Client enumnics](Smb2Client.md#smb2client-enumnics)|
|Enumerate the open files on an SMB server|[Smb2Client enumopenfiles](Smb2Client.md#smb2client-enumopenfiles)|
|Enumerate the privileges granted to an account|[Lsa getprivs](Lsa.md#lsa-getprivs)|
|Enumerate the rights and privileges granted to an account|[Lsa getrights](Lsa.md#lsa-getrights)|
|Enumerate the sessions of users connected to an SMB server|[Smb2Client enumsessions](Smb2Client.md#smb2client-enumsessions)|
|Enumerate the shares of an SMB server|[Smb2Client enumshares](Smb2Client.md#smb2client-enumshares)|
|Enumerate the system access rights granted to an account|[Lsa getsysaccess](Lsa.md#lsa-getsysaccess)|
|Enumerate the volume snapshots on an SMB server|[Smb2Client enumsnapshots](Smb2Client.md#smb2client-enumsnapshots)|
|Executes a WMI query|[Wmi query](Wmi.md#wmi-query)|
|Get a WMI object|[Wmi get](Wmi.md#wmi-get)|
|Invoke a method on a WMI class or object|[Wmi invoke](Wmi.md#wmi-invoke)|
|List the classes within a WMI namespace|[Wmi lsclass](Wmi.md#wmi-lsclass)|
|List the methods of a WMI class or object|[Wmi lsmethod](Wmi.md#wmi-lsmethod)|
|List the namespaces within a WMI namespace|[Wmi lsns](Wmi.md#wmi-lsns)|
|List the properties of a WMI class or object|[Wmi lsprop](Wmi.md#wmi-lsprop)|
|Query the status of a service|[Scm query](Scm.md#scm-query)|
|Query the triggers configured to start or stop a service|[Scm qtriggers](Scm.md#scm-qtriggers)|
|Translate an a SID to its account name and domain|[Lsa lookupsid](Lsa.md#lsa-lookupsid)|
|Translate an account name to its SID and domain name|[Lsa lookupname](Lsa.md#lsa-lookupname)|
|**Expanding Access**|
|Create an LSA policy account|[Lsa createaccount](Lsa.md#lsa-createaccount)|
|Get ticket hash for hash cracking|[Kerb tgsreq](Kerb.md#kerb-tgsreq)|
|Grant a privilege to an account|[Lsa addpriv](Lsa.md#lsa-addpriv)|
|Request a ticket for a service|[Kerb tgsreq](Kerb.md#kerb-tgsreq)|
|Request a ticket-granting-ticket|[Kerb asreq](Kerb.md#kerb-asreq)|
|Set the system access rights for an account|[Lsa setsysaccess](Lsa.md#lsa-setsysaccess)|
|**Kerberos**|
|Check the encryption types supported for a user account|[Kerb getasinfo](Kerb.md#kerb-getasinfo)|
|Check the encryption types supported for a user account|[Kerb asreq](Kerb.md#kerb-asreq)|
|Check whether a user account requires pre-authentication|[Kerb getasinfo](Kerb.md#kerb-getasinfo)|
|Check whether a user account requires pre-authentication|[Kerb asreq](Kerb.md#kerb-asreq)|
|Convert between a .ccache file and a .kirbi file|[Kerb select](Kerb.md#kerb-select)|
|Describe a Kerberos ticket|[Kerb select](Kerb.md#kerb-select)|
|Get ticket hash for hash cracking|[Kerb tgsreq](Kerb.md#kerb-tgsreq)|
|Print the contents of a .ccache file|[Kerb select](Kerb.md#kerb-select)|
|Print the contents of a .kirbi file|[Kerb select](Kerb.md#kerb-select)|
|Query tickets within a .ccache file or .kirbi file|[Kerb select](Kerb.md#kerb-select)|
|Request a ticket for a service|[Kerb tgsreq](Kerb.md#kerb-tgsreq)|
|Request a ticket-granting-ticket|[Kerb asreq](Kerb.md#kerb-asreq)|
|**Lateral Movement**|
|Create a service|[Scm create](Scm.md#scm-create)|
|Execute a command line on a remote system|[Wmi exec](Wmi.md#wmi-exec)|
|Invoke a method on a WMI class or object|[Wmi invoke](Wmi.md#wmi-invoke)|
|Start a service|[Scm start](Scm.md#scm-start)|
|**LSA**|
|Create an LSA policy account|[Lsa createaccount](Lsa.md#lsa-createaccount)|
|Enumerate policy accounts|[Lsa enumaccounts](Lsa.md#lsa-enumaccounts)|
|Enumerate the privileges granted to an account|[Lsa getprivs](Lsa.md#lsa-getprivs)|
|Enumerate the rights and privileges granted to an account|[Lsa getrights](Lsa.md#lsa-getrights)|
|Enumerate the system access rights granted to an account|[Lsa getsysaccess](Lsa.md#lsa-getsysaccess)|
|Grant a privilege to an account|[Lsa addpriv](Lsa.md#lsa-addpriv)|
|Revoke a privilege from an account|[Lsa rmpriv](Lsa.md#lsa-rmpriv)|
|Set the system access rights for an account|[Lsa setsysaccess](Lsa.md#lsa-setsysaccess)|
|Translate an a SID to its account name and domain|[Lsa lookupsid](Lsa.md#lsa-lookupsid)|
|Translate an account name to its SID and domain name|[Lsa lookupname](Lsa.md#lsa-lookupname)|
|**RPC**|
|Enumerate dynamic RPC endpoints|[Epm lsep](Epm.md#epm-lsep)|
|**SAM**|
|Enumerate the accounts in the Security Accounts Manager database|[Sam enumusers](Sam.md#sam-enumusers)|
|**SCM**|
|Create a service|[Scm create](Scm.md#scm-create)|
|Delete a service|[Scm delete](Scm.md#scm-delete)|
|Query the status of a service|[Scm query](Scm.md#scm-query)|
|Query the triggers configured to start or stop a service|[Scm qtriggers](Scm.md#scm-qtriggers)|
|Start a service|[Scm start](Scm.md#scm-start)|
|Stop a service|[Scm stop](Scm.md#scm-stop)|
|**SMB**|
|Create a directory junction or mount point on an SMB share|[Smb2Client mount](Smb2Client.md#smb2client-mount)|
|Create a directory on an SMB share|[Smb2Client mkdir](Smb2Client.md#smb2client-mkdir)|
|Create a filesystem link on an SMB share|[Smb2Client mklink](Smb2Client.md#smb2client-mklink)|
|Delete a directory on an SMB share|[Smb2Client rmdir](Smb2Client.md#smb2client-rmdir)|
|Delete a file in SMB share|[Smb2Client rm](Smb2Client.md#smb2client-rm)|
|Get a file from an SMB server|[Smb2Client get](Smb2Client.md#smb2client-get)|
|List the contents of directory on an SMB share|[Smb2Client ls](Smb2Client.md#smb2client-ls)|
|Remove a directory junction or mount point within an SMB share|[Smb2Client umount](Smb2Client.md#smb2client-umount)|
|Upload a file to an SMB share|[Smb2Client put](Smb2Client.md#smb2client-put)|
|Watch a file or directory on an SMB server for changes|[Smb2Client watch](Smb2Client.md#smb2client-watch)|
|**WMI**|
|Back up the WMI MOF repository|[Wmi backup](Wmi.md#wmi-backup)|
|Execute a command line on a remote system|[Wmi exec](Wmi.md#wmi-exec)|
|Executes a WMI query|[Wmi query](Wmi.md#wmi-query)|
|Get a WMI object|[Wmi get](Wmi.md#wmi-get)|
|Invoke a method on a WMI class or object|[Wmi invoke](Wmi.md#wmi-invoke)|
|List the classes within a WMI namespace|[Wmi lsclass](Wmi.md#wmi-lsclass)|
|List the methods of a WMI class or object|[Wmi lsmethod](Wmi.md#wmi-lsmethod)|
|List the namespaces within a WMI namespace|[Wmi lsns](Wmi.md#wmi-lsns)|
|List the properties of a WMI class or object|[Wmi lsprop](Wmi.md#wmi-lsprop)|
|Restore the WMI MOF repository|[Wmi restore](Wmi.md#wmi-restore)|

