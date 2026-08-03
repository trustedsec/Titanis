#!/bin/bash

# Sddl
_comp_Sddl () {
	_comp_T_subcommands "$1" "$2" describe lookupguid lookupwks
	return $?
}
complete -F _comp_Sddl Sddl

# Sddl describe
_comp_Sddl_describe () {
	declare -A params=(
		['-sddlorhex']=$'SddlOrHex::'
		['-objecttype']=$'ObjectType:list:File;Directory;RegistryKey;SamServer;SamDomain;SamGroup;SamAlias;SamUserAccount;DirectoryObject;Scm;Service'
		['-printhex']=$'PrintHex::'
		['-printsddl']=$'PrintSddl::'
		['-consoleoutputstyle']=$'ConsoleOutputStyle:list:Freeform;Raw;Table;List;Csv;Tsv;Json;TreeTable'
		['-outputheaders']=$'OutputHeaders::'
		['-loglevel']=$'LogLevel:list:Debug;Diagnostic;Verbose;Info;Warning;Error;Critical'
		['-consolelogformat']=$'ConsoleLogFormat:list:Text;TextWithTimestamp;Json'
		['-verbose']=$'Verbose::'
		['-diagnostic']=$'Diagnostic::'
		['-debuglog']=$'DebugLog::'
		['-humanreadable']=$'HumanReadable::'
	)
	declare -a paramsByPos=(
		'SddlOrHex'
	)
	_comp_Titanis
}
complete -F _comp_Sddl_describe Sddl-describe

# Sddl lookupguid
_comp_Sddl_lookupguid () {
	declare -A params=(
		['-guid']=$'Guid::'
		['-consoleoutputstyle']=$'ConsoleOutputStyle:list:Freeform;Raw;Table;List;Csv;Tsv;Json;TreeTable'
		['-outputfields']=$'OutputFields:list:Guid;Kind;Name'
		['-outputheaders']=$'OutputHeaders::'
		['-loglevel']=$'LogLevel:list:Debug;Diagnostic;Verbose;Info;Warning;Error;Critical'
		['-consolelogformat']=$'ConsoleLogFormat:list:Text;TextWithTimestamp;Json'
		['-verbose']=$'Verbose::'
		['-diagnostic']=$'Diagnostic::'
		['-debuglog']=$'DebugLog::'
		['-humanreadable']=$'HumanReadable::'
	)
	declare -a paramsByPos=(
		'Guid'
	)
	_comp_Titanis
}
complete -F _comp_Sddl_lookupguid Sddl-lookupguid

# Sddl lookupwks
_comp_Sddl_lookupwks () {
	declare -A params=(
		['-sidorwks']=$'SidOrWks::'
		['-consoleoutputstyle']=$'ConsoleOutputStyle:list:Freeform;Raw;Table;List;Csv;Tsv;Json;TreeTable'
		['-outputfields']=$'OutputFields:list:Wks;Sid'
		['-outputheaders']=$'OutputHeaders::'
		['-loglevel']=$'LogLevel:list:Debug;Diagnostic;Verbose;Info;Warning;Error;Critical'
		['-consolelogformat']=$'ConsoleLogFormat:list:Text;TextWithTimestamp;Json'
		['-verbose']=$'Verbose::'
		['-diagnostic']=$'Diagnostic::'
		['-debuglog']=$'DebugLog::'
		['-humanreadable']=$'HumanReadable::'
	)
	declare -a paramsByPos=(
		'SidOrWks'
	)
	_comp_Titanis
}
complete -F _comp_Sddl_lookupwks Sddl-lookupwks
