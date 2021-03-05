using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Msscmr
{

	public record struct TriggerTypeInfo
	{
		public Guid Subtype { get; }
		public string Name { get; }

		public TriggerTypeInfo(Guid subtype, string name)
		{
			this.Subtype = subtype;
			this.Name = name;
		}
	}

	public static class ServiceTriggerSubtypes
	{
		public static readonly Guid DomainJoin = new Guid("1ce20aba-9851-4421-9430-1ddeb766e809");
		public static readonly Guid DomainLeave = new Guid("ddaf516e-58c2-4866-9574-c3b615d42ea1");
		public static readonly Guid FirewallPortOpen = new Guid("b7569e07-8421-4ee0-ad10-86915afdad09");
		public static readonly Guid FirewallPortClose = new Guid("a144ed38-8e12-4de4-9d96-e64740b1a524");
		public static readonly Guid MachinePolicyPresent = new Guid("659FCAE6-5BDB-4DA9-B1FF-CA2A178D46E0");
		public static readonly Guid FirstIpAddressArrival = new Guid("4f27f2de-14e2-430b-a549-7cd48cbc8245");
		public static readonly Guid FirstIpAddressRemoval = new Guid("cc4ba62a-162e-4648-847a-b6bdf993e335");
		public static readonly Guid UserPolicyPresent = new Guid("54FB46C8-F089-464C-B1FD-59D1B62C3B50");

		// Not in [MS-SCMR]
		public static readonly Guid RpcInterfaceEvent = new Guid("bc90d167-9470-4139-a9ba-be0bbbf5b74d");
		public static readonly Guid CustomSystemStateChange = new Guid("2d7a2816-0c5e-45fc-9ce7-570e5ecde9c9");
		// Interface types
		public static readonly Guid Bluetooth = new Guid("0850302A-B344-4fda-9BE9-90576B8D46F0");
		public static readonly Guid BluetoothMtpEnum = new Guid("C1E9BC6D-1DAE-421A-9369-CC7FF0D6E359");
		public static readonly Guid LightSensor = new Guid("97f115c8-599a-4153-8894-d2d12899918a");
		public static readonly Guid VideoCamera = new Guid("E5323777-F976-4f5b-9B55-B94699C46E44");
		public static readonly Guid CameraSensor = new Guid("24E552D7-6523-47F7-A647-D3465BF1F5CA");
		public static readonly Guid SmartcardReader = new Guid("50dd5230-ba8a-11d1-bf5d-0000f805f530");
		// ETW
		public static readonly Guid LsaSrv = new Guid("199fe037-2b82-40a9-82ac-e1d46c792b99");
		public static readonly Guid AppIdServiceTrigger = new Guid("D02A9C27-79B8-40D6-9B97-CF3F8B7B5D60");
		public static readonly Guid StartNameRes = new Guid("277c9237-51d8-5c1c-b089-f02c683e5ba7");
		public static readonly Guid NetworkProfileTrigger = new Guid("fbcfac3f-8460-419f-8e48-1f0b49cdb85e");
		public static readonly Guid KernelTm = new Guid("ce20d1c3-a247-4c41-bcb8-3c7f52c8b805");
		public static readonly Guid Smartcard = new Guid("aedd909f-41c6-401a-9e41-dfc33006af5d");
		public static readonly Guid LicensingStart = new Guid("f5528ada-be5f-4f14-8aef-a95de7281161");
		public static readonly Guid NtfsUbpm = new Guid("8e6a5303-a4ce-498f-afdb-e03a8a82b077");
		public static readonly Guid KernelPower = new Guid("aa1f73e8-15fd-45d2-abfd-e7f64f78eb11");
		public static readonly Guid WebdavLookup = new Guid("22b6d684-fa63-4578-87c9-effcbe6643c7");
		public static readonly Guid FeedbackService = new Guid("e46eead8-0c54-4489-9898-8fa79d059e0e");

		private static readonly Dictionary<Guid, TriggerTypeInfo> triggerTypeLookup = new Dictionary<Guid, TriggerTypeInfo>()
		{
			{ DomainJoin, new TriggerTypeInfo(DomainJoin,  nameof(DomainJoin)) },
			{ DomainLeave, new TriggerTypeInfo(DomainLeave,  nameof(DomainLeave)) },
			{ FirewallPortOpen, new TriggerTypeInfo(FirewallPortOpen,  nameof(FirewallPortOpen)) },
			{ FirewallPortClose, new TriggerTypeInfo(FirewallPortClose,  nameof(FirewallPortClose)) },
			{ MachinePolicyPresent, new TriggerTypeInfo(MachinePolicyPresent,  nameof(MachinePolicyPresent)) },
			{ FirstIpAddressArrival, new TriggerTypeInfo(FirstIpAddressArrival,  nameof(FirstIpAddressArrival)) },
			{ FirstIpAddressRemoval, new TriggerTypeInfo(FirstIpAddressRemoval,  nameof(FirstIpAddressRemoval)) },
			{ UserPolicyPresent, new TriggerTypeInfo(UserPolicyPresent,  nameof(UserPolicyPresent)) },

			{ RpcInterfaceEvent, new TriggerTypeInfo(UserPolicyPresent,  nameof(RpcInterfaceEvent)) },
			{ CustomSystemStateChange, new TriggerTypeInfo(CustomSystemStateChange,  nameof(CustomSystemStateChange)) },

			// Network ports
			{ Bluetooth, new TriggerTypeInfo(Bluetooth,  nameof(Bluetooth)) },
			{ BluetoothMtpEnum, new TriggerTypeInfo(BluetoothMtpEnum,  nameof(BluetoothMtpEnum)) },
			{ LightSensor, new TriggerTypeInfo(LightSensor,  nameof(LightSensor)) },
			{ VideoCamera, new TriggerTypeInfo(VideoCamera,  nameof(VideoCamera)) },
			{ CameraSensor, new TriggerTypeInfo(CameraSensor,  nameof(CameraSensor)) },
			{ SmartcardReader, new TriggerTypeInfo(SmartcardReader,  nameof(SmartcardReader)) },

			// ETW providers
			{ LsaSrv, new TriggerTypeInfo(LsaSrv,  nameof(LsaSrv)) },
			{ AppIdServiceTrigger, new TriggerTypeInfo(AppIdServiceTrigger,  "Microsoft-Windows-AppIDServiceTrigger") },
			{ StartNameRes, new TriggerTypeInfo(StartNameRes,  "Microsoft-Windows-StartNameRes") },
			{ NetworkProfileTrigger, new TriggerTypeInfo(NetworkProfileTrigger,  "Microsoft-Windows-NetworkProfileTriggerProvider") },
			{ KernelTm, new TriggerTypeInfo(KernelTm,  "Microsoft-Windows-Kernel-Tm-Trigger") },
			{ Smartcard, new TriggerTypeInfo(Smartcard,  "Microsoft-Windows-Smartcard-Trigger") },
			{ LicensingStart, new TriggerTypeInfo(LicensingStart,  "Microsoft-Windows-Kernel-Licensing-StartServiceTrigger") },
			{ NtfsUbpm, new TriggerTypeInfo(NtfsUbpm,  "Microsoft-Windows-Ntfs-UBPM") },
			{ KernelPower, new TriggerTypeInfo(KernelPower,  "Microsoft-Windows-Kernel-PowerTrigger") },
			{ WebdavLookup, new TriggerTypeInfo(WebdavLookup,  "Microsoft-Windows-WebdavClient-LookupServiceTrigger") },
			{ FeedbackService, new TriggerTypeInfo(FeedbackService,  "Microsoft-Windows-Feedback-Service-TriggerProvider") },
		};

		public static bool TryGetSubtypeInfo(Guid type, out TriggerTypeInfo info) => triggerTypeLookup.TryGetValue(type, out info);
	}

	public class ServiceTrigger
	{
		internal ServiceTrigger(
			string serviceName,
			ServiceTriggerType triggerType,
			Guid triggerSubtype,
			string? triggerTypeDescription,
			ServiceTriggerAction action,
			object[]? dataItems
			)
		{
			ServiceName = serviceName;
			TriggerType = triggerType;
			TriggerSubtype = triggerSubtype;
			TriggerTypeDescription = triggerTypeDescription;
			Action = action;
			DataItems = dataItems;
		}

		public string ServiceName { get; }
		public ServiceTriggerType TriggerType { get; }
		[Browsable(false)]
		public Guid TriggerSubtype { get; }
		[DisplayName("Subtype")]
		public string? TriggerTypeDescription { get; }
		public ServiceTriggerAction Action { get; }
		[Browsable(false)]
		public object[]? DataItems { get; }

		private object? _data0;
		public object? Data0 => (this._data0 ??= this.BuildData0());

		private object? BuildData0()
		{
			var data0 = this.DataItems?.ElementAtOrDefault(0);
			if (data0 == null)
				return null;

			if (data0 is byte[] bytes)
			{
				data0 = bytes.ToHexString();
				return data0;
			}
			else
			{
				return data0;
			}
		}
	}
}
