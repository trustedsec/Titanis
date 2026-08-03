#!/bin/bash

# Dcom
_comp_Dcom () {
	_comp_T_subcommands "$1" "$2" activate invoke
	return $?
}
complete -F _comp_Dcom Dcom

# Dcom activate
_comp_Dcom_activate () {
	declare -A params=(
		['-servername']=$'ServerName::'
		['-consoleoutputstyle']=$'ConsoleOutputStyle:list:Freeform;Raw;Table;List;Csv;Tsv;Json;TreeTable'
		['-outputheaders']=$'OutputHeaders::'
		['-loglevel']=$'LogLevel:list:Debug;Diagnostic;Verbose;Info;Warning;Error;Critical'
		['-consolelogformat']=$'ConsoleLogFormat:list:Text;TextWithTimestamp;Json'
		['-verbose']=$'Verbose::'
		['-diagnostic']=$'Diagnostic::'
		['-debuglog']=$'DebugLog::'
		['-humanreadable']=$'HumanReadable::'
		['-rpcconnecttimeout']=$'RpcConnectTimeout::'
		['-rpccalltimeout']=$'RpcCallTimeout::'
		['-spnego']=$'Spnego::'
		['-authepm']=$'AuthEpm::'
		['-encryptepm']=$'EncryptEpm::'
		['-encryptrpc']=$'EncryptRpc::'
		['-prefersmb']=$'PreferSmb::'
		['-clsid']=$'Clsid::'
		['-filename']=$'FileName::'
		['-anonymous']=$'Anonymous::'
		['-username']=$'UserName::'
		['-userdomain']=$'UserDomain::'
		['-password']=$'Password::'
		['-ntlmhash']=$'NtlmHash::'
		['-aeskey']=$'AesKey::'
		['-deskey']=$'DesKey::'
		['-workstation']=$'Workstation::'
		['-tgt']=$'Tgt::'
		['-armorticket']=$'ArmorTicket::'
		['-tickets']=$'Tickets::'
		['-ticketcache']=$'TicketCache::'
		['-delegate']=$'Delegate::'
		['-delegateticket']=$'DelegateTicket::'
		['-ntlmversion']=$'NtlmVersion::'
		['-kdc']=$'Kdc::'
		['-keytab']=$'Keytab::'
		['-s4username']=$'S4UserName::'
		['-u2username']=$'U2UserName::'
		['-s4usercert']=$'S4UserCert::'
		['-s4proxyservice']=$'S4ProxyService::'
		['-spnoverride']=$'SpnOverride::'
		['-authproxy']=$'AuthProxy::'
		['-hostaddress']=$'HostAddress::'
		['-usetcp6only']=$'UseTcp6Only::'
		['-usetcp4only']=$'UseTcp4Only::'
		['-socks5']=$'Socks5::'
		['-dialects']=$'Dialects:list:Smb2_0_2;Smb2_1;Smb3_0;Smb3_0_2;Smb3_1_1'
		['-requiresigning']=$'RequireSigning::'
		['-requiresecurenegotiate']=$'RequireSecureNegotiate::'
		['-encryptsmb']=$'EncryptSmb::'
		['-followdfs']=$'FollowDfs::'
		['-dfsreferralbuffersize']=$'DfsReferralBufferSize::'
		['-usercert']=$'UserCert::'
		['-userkey']=$'UserKey::'
		['-userkeypassword']=$'UserKeyPassword::'
	)
	declare -a paramsByPos=(
		'ServerName'
		'Clsid'
	)
	_comp_Titanis
}
complete -F _comp_Dcom_activate Dcom-activate

# Dcom invoke
_comp_Dcom_invoke () {
	declare -A params=(
		['-servername']=$'ServerName::'
		['-methodname']=$'MethodName::'
		['-arguments']=$'Arguments::'
		['-consoleoutputstyle']=$'ConsoleOutputStyle:list:Freeform;Raw;Table;List;Csv;Tsv;Json;TreeTable'
		['-outputheaders']=$'OutputHeaders::'
		['-loglevel']=$'LogLevel:list:Debug;Diagnostic;Verbose;Info;Warning;Error;Critical'
		['-consolelogformat']=$'ConsoleLogFormat:list:Text;TextWithTimestamp;Json'
		['-verbose']=$'Verbose::'
		['-diagnostic']=$'Diagnostic::'
		['-debuglog']=$'DebugLog::'
		['-humanreadable']=$'HumanReadable::'
		['-rpcconnecttimeout']=$'RpcConnectTimeout::'
		['-rpccalltimeout']=$'RpcCallTimeout::'
		['-spnego']=$'Spnego::'
		['-authepm']=$'AuthEpm::'
		['-encryptepm']=$'EncryptEpm::'
		['-encryptrpc']=$'EncryptRpc::'
		['-prefersmb']=$'PreferSmb::'
		['-clsid']=$'Clsid::'
		['-filename']=$'FileName::'
		['-anonymous']=$'Anonymous::'
		['-username']=$'UserName::'
		['-userdomain']=$'UserDomain::'
		['-password']=$'Password::'
		['-ntlmhash']=$'NtlmHash::'
		['-aeskey']=$'AesKey::'
		['-deskey']=$'DesKey::'
		['-workstation']=$'Workstation::'
		['-tgt']=$'Tgt::'
		['-armorticket']=$'ArmorTicket::'
		['-tickets']=$'Tickets::'
		['-ticketcache']=$'TicketCache::'
		['-delegate']=$'Delegate::'
		['-delegateticket']=$'DelegateTicket::'
		['-ntlmversion']=$'NtlmVersion::'
		['-kdc']=$'Kdc::'
		['-keytab']=$'Keytab::'
		['-s4username']=$'S4UserName::'
		['-u2username']=$'U2UserName::'
		['-s4usercert']=$'S4UserCert::'
		['-s4proxyservice']=$'S4ProxyService::'
		['-spnoverride']=$'SpnOverride::'
		['-authproxy']=$'AuthProxy::'
		['-hostaddress']=$'HostAddress::'
		['-usetcp6only']=$'UseTcp6Only::'
		['-usetcp4only']=$'UseTcp4Only::'
		['-socks5']=$'Socks5::'
		['-dialects']=$'Dialects:list:Smb2_0_2;Smb2_1;Smb3_0;Smb3_0_2;Smb3_1_1'
		['-requiresigning']=$'RequireSigning::'
		['-requiresecurenegotiate']=$'RequireSecureNegotiate::'
		['-encryptsmb']=$'EncryptSmb::'
		['-followdfs']=$'FollowDfs::'
		['-dfsreferralbuffersize']=$'DfsReferralBufferSize::'
		['-usercert']=$'UserCert::'
		['-userkey']=$'UserKey::'
		['-userkeypassword']=$'UserKeyPassword::'
	)
	declare -a paramsByPos=(
		'ServerName'
		'Clsid'
		'MethodName'
		'Arguments'
	)
	_comp_Titanis
}
complete -F _comp_Dcom_invoke Dcom-invoke
