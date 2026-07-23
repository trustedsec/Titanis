#!/bin/bash

_comp_T_param() {
	local paramSpec=$1
	local cmp=$2

	local paramName paramType rest
	IFS=":" read -r paramName paramType rest <<< "$paramSpec"

	if [[ "$paramType" = "" ]]; then
		# No completions available
		COMPREPLY=()
		return 1
	fi

	if [[ "$paramType" = "list" ]]; then
		IFS=";" read -r -a COMPREPLY <<< $rest
		_T_filterArray "$cmp" "${COMPREPLY[@]}"
		return 0
	elif [[ "$paramType" = "file" ]]; then
		local -a filePatterns
		IFS=";" read -r -a filePatterns <<< $rest
		COMPREPLY=()
		for i in "${filePatterns[@]}"; do
			COMPREPLY+=( $(compgen -G "$cmp$i") )
		done
		return 0
	else
		COMPREPLY=( $(compgen -A "$paramType" "$cmp") )
		return 0
	fi
}

_T_filterArray() {
	local pattern=$1
	if [[ -z $pattern ]]; then
		COMPREPLY=(${@:2})
		return 0
	fi

	local matching=()
	local i
	for i in ${@:2}; do
		if [[ -z $pattern || ${i,,} = ${pattern,,}* ]]; then
			matching+=($i)
		fi
	done

	COMPREPLY=(${matching[@]})

	return 0
}

_comp_Titanis() {
	local cmp=$1
	local argName
	local arg
	local args=( "${COMP_WORDS[@]:1}" )
	local argCount=${#args[@]}

	local -a paramSpecs
	IFS="|" read -r -a paramSpecs <<< "$2"

	declare -A paramSpecsByName
	declare -a paramsByPos
	local paramSpec
	for paramSpec in "${paramSpecs[@]}"; do
		local paramName paramType
		IFS=":" read -r paramName paramType <<< "$paramSpec"

		if [[ $paramName = -* ]]; then
			# This is a named argument
			paramSpecsByName+=(["${paramName,,}"]="${paramSpec}")
		elif [[ $paramName = @ ]]; then
			# This is a positional argument
			paramsByPos+=( "-$paramType" )
		fi
	done

	local targetArgIndex=$((COMP_CWORD - 1))
	if [[ "${COMP_LINE:(COMP_POINT-1):2}" == " " ]]; then
		# Cursor is at the end of the line
		#((targetArgIndex++))
		:
	fi

	local curArg="${args[$targetArgIndex]}"
	if [[ $curArg == -* ]]; then
		# Cursor is on a parameter name
		# Filter parameter names to exclude used parameters
		local -A usedParams
		for arg in ${args[@]}; do
			usedParams+=( ["${arg,,}"]=true )
		done

		local -a availParams
		local paramSpec
		for paramSpec in "${paramSpecs[@]}"; do
			local paramName paramType
			IFS=':' read -r paramName paramType <<< "$paramSpec"

			if [[ ! -v usedParams["${paramName,,}"] ]]; then
				availParams+=("$paramName")
			fi
		done

		_T_filterArray "$curArg" ${availParams[@]}
		return 0;
	fi

	local nextPos=0
	local i 
	local selArgName=""
	local isPos=0
	declare -A argValues
	for ((i=0; i<=$targetArgIndex; i++)); do
		arg=${args[i]}
		isPos=0

		if [[ $arg = -* ]]; then
			argName=$arg
		elif [[ $arg = *, ]]; then
			# Keep the same arg name
			:
		elif [[ -n "$argName" ]]; then
			# Consume this named argument
			selArgName=$argName
			argValues+=( ["$selArgName"]="$arg" )
			argName=""
		else
			while true; do
				selArgName="${paramsByPos[nextPos]}"
				((nextPos++))

				if [[ ! -v argValues["$selArgName"] ]]; then
					isPos=1
					break
				fi
			done
		fi
	done

	if [[ -n $selArgName ]]; then
		if [[ $isPos -gt 0 && -z $cmp ]]; then
			# Help the user by printing the name of the positional argument
			COMPREPLY=( "$selArgName" )
			return 0
		else
			local argSpec=${paramSpecsByName[${selArgName,,}]}
			_comp_T_param $argSpec $cmp
			return $?
		fi
	else
		# No completion available
		return 1
	fi
}

_comp_T_subcommands() {
	local base=${COMP_WORDS[0]}
	local subcmds=( ${@:3} )
	local cmp=$2

	if [ $COMP_CWORD -eq 1 ]; then
		_T_filterArray "$cmp" ${subcmds[@]}
	else
		local subcmd="${COMP_WORDS[1]}"
		((COMP_CWORD--))
		COMP_WORDS=( "${base}_${subcmd}" "${COMP_WORDS[@]:2}" )
		_comp_${base}_$subcmd $base $cmp
	fi
	return 0
}
