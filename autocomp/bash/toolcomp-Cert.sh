#!/bin/bash

# Cert
_comp_Cert () {
	_comp_T_subcommands "$1" "$2" selfcert
	return $?
}
complete -F _comp_Cert Cert

# Cert selfcert
_comp_Cert_selfcert () {
	declare -A params=(
		['-subject']=$'Subject::'
		['-keysizebits']=$'KeySizeBits::'
		['-hashalgorithm']=$'HashAlgorithm:list:Md5;Sha1;Sha256;Sha384;Sha512;Sha3_256;Sha3_384;Sha3_512'
		['-templatefile']=$'TemplateFile::'
		['-subjectaltname']=$'SubjectAltName::'
		['-pfxfilename']=$'PfxFileName::'
		['-certfilename']=$'CertFileName::'
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
		'Subject'
	)
	_comp_Titanis
}
complete -F _comp_Cert_selfcert Cert-selfcert
