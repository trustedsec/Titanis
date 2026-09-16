namespace Titanis.DceRpc.WireProtocol
{
	public enum RpcFaultCode : uint
	{
		// [MS-RPCE] § 3.2.3.5.1 - Failure Semantics
		AccessDenied = 5,



		nca_s_comm_failure = 0x1C010001,
		/* bad operation number in call: */
		OpnumRange = 0x1C010002,
		/* unknown interface: */
		nca_s_unk_if = 0x1C010003,
		/* client passed server wrong server boot time: */
		nca_s_wrong_boot_time = 0x1C010006,
		/* a restarted server called back a client: */
		nca_s_you_crashed = 0x1C010009,
		/* someone messed up the protocol: */
		nca_s_proto_error = 0x1C01000B,
		/* output args too big: */
		nca_s_out_args_too_big = 0x1C010013,
		/* server is too busy to handle call: */
		nca_s_server_too_busy = 0x1C010014,
		/* string argument longer than declared max len: */
		nca_s_fault_string_too_long = 0x1C010015,
		/* no implementation of generic operation for object: */
		nca_s_unsupported_type = 0x1C010017,

		nca_s_fault_int_div_by_zero = 0x1C000001,
		nca_s_fault_addr_error = 0x1C000002,
		nca_s_fault_fp_div_zero = 0x1C000003,
		nca_s_fault_fp_underflow = 0x1C000004,
		nca_s_fault_fp_overflow = 0x1C000005,
		nca_s_fault_invalid_tag = 0x1C000006,
		nca_s_fault_invalid_bound = 0x1C000007,
		nca_s_rpc_version_mismatch = 0x1C000008,
		/* call rejected, but no more detail: */
		nca_s_unspec_reject = 0x1C000009,
		nca_s_bad_actid = 0x1C00000A,
		nca_s_who_are_you_failed = 0x1C00000B,
		nca_s_manager_not_entered = 0x1C00000C,
		nca_s_fault_cancel = 0x1C00000D,
		nca_s_fault_ill_inst = 0x1C00000E,
		nca_s_fault_fp_error = 0x1C00000F,
		nca_s_fault_int_overflow = 0x1C000010,
		/* unused:                                    0x1C000011 */
		nca_s_fault_unspec = 0x1C000012,
		nca_s_fault_remote_comm_failure = 0x1C000013,
		nca_s_fault_pipe_empty = 0x1C000014,
		nca_s_fault_pipe_closed = 0x1C000015,
		nca_s_fault_pipe_order = 0x1C000016,
		nca_s_fault_pipe_discipline = 0x1C000017,
		nca_s_fault_pipe_comm_error = 0x1C000018,
		nca_s_fault_pipe_memory = 0x1C000019,
		nca_s_fault_context_mismatch = 0x1C00001A,
		nca_s_fault_remote_no_memory = 0x1C00001B,
		nca_s_invalid_pres_context_id = 0x1C00001C,
		nca_s_unsupported_authn_level = 0x1C00001D,
		nca_s_invalid_checksum = 0x1C00001F,
		nca_s_invalid_crc = 0x1C000020,
		nca_s_fault_user_defined = 0x1C000021,
		nca_s_fault_tx_open_failed = 0x1C000022,
		nca_s_fault_codeset_conv_error = 0x1C000023,
		nca_s_fault_object_not_found = 0x1C000024,
		nca_s_fault_no_client_stub = 0x1C000025,
	}
}
