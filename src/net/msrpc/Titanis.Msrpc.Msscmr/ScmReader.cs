using ms_scmr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using Titanis.IO;

namespace Titanis.Msrpc.Msscmr
{
	struct EnumServiceStatusStruct
	{
		public static unsafe int StructSize => sizeof(EnumServiceStatusStruct);

		internal uint offServiceName;
		internal uint offDisplayName;
		internal SERVICE_STATUS status;
	}

	static class ScmReader
	{
		internal static void ReadServiceStatusInfoArray(this IO.ByteMemoryReader reader, int count, IList<EnumServiceStatusInfo> list)
		{
			for (int i = 0; i < count; i++)
			{
				EnumServiceStatusInfo statusInfo = reader.ReadServiceStatusInfo();
				list.Add(statusInfo);
			}
		}

		public static EnumServiceStatusInfo ReadServiceStatusInfo(this IO.ByteMemoryReader reader)
		{
			int pos = reader.Position;
			EnumServiceStatusStruct enumInfo = reader.ReadServiceStatusInfoStruct();
			var printerInfo = new EnumServiceStatusInfo(
				reader.ReadZString(0, enumInfo.offServiceName),
				reader.ReadZString(0, enumInfo.offDisplayName),
				new ServiceStatus(enumInfo.status)
			);
			return printerInfo;
		}

		internal unsafe static EnumServiceStatusStruct ReadServiceStatusInfoStruct(this IO.ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(EnumServiceStatusStruct.StructSize))
			{
				return *(EnumServiceStatusStruct*)pBuf;
			}
		}


		private static string ReadZString(
			this IO.ByteMemoryReader reader,
			int baseOffset,
			uint offset)
		{
			string str = (offset == 0)
				? null
				: reader.ExtractZStringUni(baseOffset + (int)offset)
				;
			return str;
		}

		internal static unsafe int ServiceStatusProcessStructSize => sizeof(SERVICE_STATUS_PROCESS);

		internal static unsafe SERVICE_STATUS_PROCESS ReadStatusProcessStructFrom(byte[] bytes)
		{
			if (bytes == null)
				throw new ArgumentNullException(nameof(bytes));
			if (bytes.Length < ServiceStatusProcessStructSize)
				// TOOD: More informative error message
				throw new InvalidDataException();

			fixed (byte* pBuf = bytes)
			{
				return *(SERVICE_STATUS_PROCESS*)pBuf;
			}
		}

		internal unsafe static SC_ACTION ReadSC_ACTION(this IO.ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(sizeof(SC_ACTION)))
			{
				return *(SC_ACTION*)pBuf;
			}
		}

		internal unsafe static SERVICE_FAILURE_ACTIONS_WOW64 ReadSERVICE_FAILURE_ACTIONS_WOW64(this IO.ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(sizeof(SERVICE_FAILURE_ACTIONS_WOW64)))
			{
				return *(SERVICE_FAILURE_ACTIONS_WOW64*)pBuf;
			}
		}

		private static ServiceFailureAction[] ReadServiceFailureActions(
			this IO.ByteMemoryReader reader,
			int offset,
			int count)
		{
			ServiceFailureAction[] actions = new ServiceFailureAction[count];
			reader.Position = offset;
			for (int i = 0; i < count; i++)
			{
				var action = actions[i] = reader.ReadServiceFailureAction();
			}
			return actions;
		}

		private static ServiceFailureAction ReadServiceFailureAction(
			this IO.ByteMemoryReader reader
			)
		{
			var scAction = reader.ReadSC_ACTION();
			return new ServiceFailureAction
			{
				ActionType = (ServiceFailureActionType)scAction.Type,
				Delay = TimeSpan.FromMilliseconds(scAction.Delay)
			};
		}

		internal static ServiceFailureActions ReadServiceFailureActions(this IO.ByteMemoryReader reader)
		{
			int offStart = reader.Position;

			SERVICE_FAILURE_ACTIONS_WOW64 struc = reader.ReadSERVICE_FAILURE_ACTIONS_WOW64();
			return new ServiceFailureActions
			{
				ResetPeriod = TimeSpan.FromSeconds(struc.dwResetPeriod),
				RebootMessage = (struc.dwRebootMsgOffset > 0) ? reader.ExtractZStringUni(offStart + (int)struc.dwRebootMsgOffset) : null,
				Actions = reader.ReadServiceFailureActions(offStart + (int)struc.dwsaActionsOffset, (int)struc.cActions)
			};

		}
	}
}
