using ms_dcom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.IO;

namespace Titanis.Msrpc.Msdcom
{
	using ms_dcom;
	using System.Diagnostics;
	using System.Runtime.InteropServices;
	using Titanis.Security;
	using Titanis.Winterop;

	internal class ScmActivatorClient : RpcServiceClient<IRemoteSCMActivatorClientProxy>
	{
		private readonly DcomClient _dcom;
		// [MS-DCOM] <23>
		private const int CLSCTX_REMOTE_SERVER = 0x10;

		internal ScmActivatorClient(DcomClient dcom)
		{
			this._dcom = dcom;
		}

		// [MS-DCOM] § 1.9
		/// <inheritdoc/>
		public sealed override bool SupportsDynamicTcp => true;
		// [MS-DCOM] § 1.9
		/// <inheritdoc/>
		public sealed override int WellKnownTcpPort => 135;
		// [MS-DCOM] § 3.2.4.1.1.2
		/// <inheritdoc/>
		public sealed override string? ServiceClass => ServiceClassNames.RpcSs;
		// [MS-DCOM] § 2.2
		/// <inheritdoc/>
		public sealed override bool SupportsNdr64 => false;


		public async Task<ActivationResult> CreateInstance(
			Guid clsid,
			bool isClassFactory,
			COMVERSION version,
			Guid[] interfaceIDs,
			ushort[] protseqs,
			System.Threading.CancellationToken cancellationToken)
		{
			// TODO: Parameterize session ID
			// TODO: Parameterize console flag

			string serverName = "WIN10-TEST";
			ClientContext clientCtx = new ClientContext();

			ActPropertiesIn actIn = new ActPropertiesIn()
			{
				// [MS-DCOM] § 2.2.22.2.2 - SpecialPropertiesData
				SpecialPropertiesData = new SpecialPropertiesData
				{
					dwSessionId = 0xFFFF_FFFF,
					fRemoteThisSessionId = 0,
					fClientImpersonating = 0,
					fPartitionIDPresent = 0,
					dwDefaultAuthnLvl = (int)RpcAuthLevel.PacketIntegrity,
					guidPartition = Guid.Empty,
					dwPRTFlags = 0,
					dwOrigClsctx = CLSCTX_REMOTE_SERVER,
					// Sent by Windows
					dwFlags = 2,
				},
				// [MS-DCOM] § 2.2.22.2.1 - InstantiationInfoData
				InstantiationInfoData = new InstantiationInfoData
				{
					classId = clsid,
					classCtx = CLSCTX_REMOTE_SERVER,
					actvflags = 0,
					fIsSurrogate = 0,
					cIID = (uint)interfaceIDs.Length,
					instFlag = 0,
					pIID = new RpcPointer<Guid[]>(interfaceIDs),
					thisSize = 136,
					clientCOMVersion = this._dcom.NegotiatedVersion
				},
				// [MS-DCOM] § 2.2.22.2.5
				ActivationContextInfoData = new ActivationContextInfoData
				{
					clientOK = 0,
					pIFDClientCtx = new RpcPointer<MInterfacePointer>(this._dcom.Wrap(clientCtx).AsMInterfacePtr()),
					pIFDPrototypeCtx = null
				},
				// [MS-DCOM] § 2.2.22.2.7
				SecurityInfoData = new SecurityInfoData
				{
					dwAuthnFlags = 0,
					// [MS-DCOM] <35>
					pServerInfo = new RpcPointer<COSERVERINFO>(new COSERVERINFO
					{
						// [MS-DCOM] <36>
						pwszName = new RpcPointer<string>(serverName)
					}),
					pdwReserved = null
				},
				// [MS-DCOM] § 2.2.22.2.6
				LocationInfoData = new LocationInfoData
				{

				},
				// [MS-DCOM] § 2.2.22.2.4
				ScmRequestInfoData = new ScmRequestInfoData
				{
					pdwReserved = null,
					remoteRequest = new RpcPointer<customREMOTE_REQUEST_SCM_INFO>(new customREMOTE_REQUEST_SCM_INFO
					{
						// [MS-DCOM] <33>
						ClientImpLevel = 2,
						cRequestedProtseqs = 1,
						pRequestedProtseqs = new RpcPointer<ushort[]>(new ushort[]
						{
							(ushort)ProtocolId.Tcp4
						})
					})
				}
			};
			var classRef = this._dcom.Wrap<IRpcObject>(actIn);

			RpcPointer<ms_dcom.ORPCTHAT> pThat = new();
			RpcPointer<MInterfacePointer> pActProperties = new RpcPointer<MInterfacePointer>(classRef.AsMInterfacePtr());
			RpcPointer<RpcPointer<MInterfacePointer>> ppActProperties = new();
			var res = (Hresult)await this._proxy.RemoteCreateInstance(
				new RpcPointer<ORPCTHIS>() { value = new ORPCTHIS() { version = version } },
				pThat,
				null,
				pActProperties,
				ppActProperties,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			var actOut = await this._dcom.Unwrap<ActPropertiesOut>(ppActProperties.value.value.abData, cancellationToken).ConfigureAwait(false);

			customREMOTE_REPLY_SCM_INFO scmReply = actOut.ScmReplyInfoData.remoteReply.value;
			PropsOutInfo propsOut = actOut.PropsOutInfo;
			return new ActivationResult()
			{
				Oxid = scmReply.Oxid,
				OxidBinding = DualStringArray.FromIdl(scmReply.pdsaOxidBindings.value),
				IpidRemUnknown = scmReply.ipidRemUnknown,
				AuthLevelHint = (RpcAuthLevel)scmReply.authnHint,
				ComVersion = scmReply.serverVersion,
				QueryInterfaceResults = ActivationClient.ProcessQiResults(propsOut.phresults.value, propsOut.ppIntfData.value)
			};
		}
	}

	// [MS-DCOM] § 2.2.22.2 - Activation Properties
	abstract class ActProperty
	{
		private protected ActProperty(Guid clsid)
		{
			this.Clsid = clsid;
		}

		public Guid Clsid { get; }
		public abstract void WriteTo(RpcEncoder encoder);

		public static ActProperty_Struct<T> CreateFixed<T>(Guid clsid, ref readonly T struc)
			where T : struct, IRpcFixedStruct
		{
			return new Msdcom.ActProperty_Struct<T>(clsid, struc);
		}
	}

	sealed class ActProperty_Struct<T> : ActProperty
		where T : IRpcFixedStruct
	{
		public ActProperty_Struct(Guid clsid, ref readonly T struc)
			: base(clsid)
		{
			this._struc = struc;
		}

		private readonly T? _struc;

		public sealed override void WriteTo(RpcEncoder encoder)
		{
			encoder.SerializeType1(encoder =>
			{
				encoder.WriteFixedStruct(this._struc, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this._struc);
			});
		}
	}

	class ActUnmarshaler : IUnmarshaler
	{
		public Objref Unmarshal(ByteMemoryReader reader)
		{
			return new ActPropertiesIn();
		}
	}

	// [MS-DCOM] § 2.2.22
	abstract class ActProperties : Objref_Custom, IRpcObject, ICustomDcomMarshal
	{
		// [MS-DCOM] <22>
		private const int MSHCTX_DIFFERENTMACHINE = 2;

		protected abstract ActProperty[] GetActivationProperties();

		// [MS-DCOM] § 2.2.22 - Activation Properties BLOB
		protected sealed override void WriteObjectData(ByteWriter writer)
		{
			var props = this.GetActivationProperties();

			// TODO: Should this use NDR or NDR64 if negotiated?


			var callContext = new RpcCallContext(null);

			Guid[] propIds = new Guid[props.Length];
			uint[] propSizes = new uint[props.Length];
			Memory<byte> propData;
			// Serialize properties
			{
				ByteWriter propWriter = new ByteWriter();
				var propEncoder = RpcEncoding.MsrpcNdr.CreateEncoder(propWriter, callContext);
				for (int i = 0; i < props.Length; i++)
				{
					var prop = props[i];
					propIds[i] = prop.Clsid;

					int offPropStart = propWriter.Position;
					prop.WriteTo(propEncoder);
					int offPropEnd = propWriter.Position;
					propSizes[i] = (uint)(offPropEnd - offPropStart);
				}

				propData = propWriter.GetData();
			}

			// [MS-DCOM] § 2.2.22 - Activation Properties BLOB
			var offActBlobSize = writer.Position;
			// ActBlob.dwSize
			writer.Advance(4);
			// ActBlob.dwReserved
			writer.WriteUInt32LE(0);
			int offCustomHeader;

			// Serialize custom header 
			{

				// [MS-DCOM] § 2.2.22.1 - CustomHeader
				CustomHeader hdr = new CustomHeader
				{
#if DEBUG
					totalSize = 0xaa55aa55,
					headerSize = 0xbb66bb66,
#endif
					dwReserved = 0,
					// [MS-DCOM] <22>
					destCtx = MSHCTX_DIFFERENTMACHINE,
					cIfs = (uint)props.Length,
					classInfoClsid = Guid.Empty,
					pclsid = new RpcPointer<Guid[]>(propIds),
					pSizes = new RpcPointer<uint[]>(propSizes)
				};
				var encoder = RpcEncoding.MsrpcNdr.CreateEncoder(writer, callContext);
				offCustomHeader = writer.Position;
				int offCustHdrSize = 0;
				encoder.SerializeType1(encoder =>
				{
					ByteWriter writer = encoder.GetWriter();
					offCustHdrSize = writer.Position;
					encoder.WriteFixedStruct(hdr, NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(hdr);
					encoder.Align(NdrAlignment._8Byte);
				});


				// CustomHeader.headerSize
				{
					int offCustHdrEnd = writer.Position;
					writer.SetPosition(offCustHdrSize);
					// CustomHeader.totalSize
					// CustomHeader.headerSize
					int cbCustHdr = offCustHdrEnd - offCustomHeader;
					writer.WriteInt32LE(cbCustHdr + propData.Length);
					writer.WriteInt32LE(cbCustHdr);
					writer.SetPosition(offCustHdrEnd);
				}
			}

			writer.WriteBytes(propData.Span);


			var offEnd = writer.Position;
			// ActBlob.dwSize
			writer.SetPosition(offActBlobSize);
			writer.WriteInt32LE(offEnd - offCustomHeader);
			writer.SetPosition(offEnd);
		}

		Objref ICustomDcomMarshal.CreateObjref()
		{
			return this;
		}
	}

	// [MS-DCOM] § 3.1.2.5.2.3.3
	sealed class ActPropertiesIn : ActProperties
	{
		internal ActPropertiesIn()
		{
		}

		public sealed override Guid Iid => DcomIds.IID_IActivationPropertiesIn;
		public sealed override object GetObject()
		{
			return this;
		}

		public sealed override Guid MarshalClsid => DcomIds.CLSID_ActivationPropertiesIn;

		// [MS-DCOM] § 2.2.22.2.1
		public InstantiationInfoData InstantiationInfoData { get; set; }
		// [MS-DCOM] § 2.2.22.2.4
		public ScmRequestInfoData ScmRequestInfoData { get; set; }
		// [MS-DCOM] § 2.2.22.2.6
		public LocationInfoData LocationInfoData { get; set; }
		// [MS-DCOM] § 2.2.22.2.7
		public SecurityInfoData SecurityInfoData { get; set; }
		// [MS-DCOM] § 2.2.22.2.5
		public ActivationContextInfoData ActivationContextInfoData { get; set; }
		// [MS-DCOM] § 2.2.22.2.3
		public InstanceInfoData InstanceInfoData { get; set; }
		// [MS-DCOM] § 2.2.22.2.2
		public SpecialPropertiesData SpecialPropertiesData { get; set; }

		protected override ActProperty[] GetActivationProperties()
		{
			return new ActProperty[]
			{
				ActProperty.CreateFixed(DcomIds.CLSID_SpecialSystemProperties, this.SpecialPropertiesData),
				ActProperty.CreateFixed(DcomIds.CLSID_InstantiationInfo, this.InstantiationInfoData),
				ActProperty.CreateFixed(DcomIds.CLSID_ActivationContextInfo, this.ActivationContextInfoData),
				ActProperty.CreateFixed(DcomIds.CLSID_SecurityInfo, this.SecurityInfoData),
				ActProperty.CreateFixed(DcomIds.CLSID_ServerLocationInfo, this.LocationInfoData),
				ActProperty.CreateFixed(DcomIds.CLSID_ScmRequestInfo, this.ScmRequestInfoData),
			};
		}
	}

	class ActPropertiesOut : ActProperties
	{
		// [MS-DCOM] § 2.2.22.2.8
		public ScmReplyInfoData ScmReplyInfoData { get; set; }
		// [MS-DCOM] § 2.2.22.2.9
		public PropsOutInfo PropsOutInfo { get; set; }

		public override Guid MarshalClsid => DcomIds.CLSID_ActivationPropertiesOut;

		public override Guid Iid => DcomIds.IID_IActivationPropertiesOut;

		public override object GetObject()
		{
			return this;
		}

		protected override ActProperty[] GetActivationProperties()
		{
			return new ActProperty[]
			{
				ActProperty.CreateFixed(DcomIds.CLSID_ScmReplyInfo, this.PropsOutInfo),
				ActProperty.CreateFixed(DcomIds.CLSID_ScmReplyInfo, this.ScmReplyInfoData),
			};
		}
	}

	class ActPropertiesOutUnmarshaler : IUnmarshaler
	{
		public Objref Unmarshal(ByteMemoryReader reader)
		{
			var cbActBlob = reader.ReadInt32LE();

			var decoder = RpcEncoding.MsrpcNdr.CreateDecoder(reader, new RpcCallContext(null));
			var hdr = decoder.DeserializeType1(r =>
			{
				var hdr = decoder.ReadFixedStruct<CustomHeader>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral(ref hdr);
				return hdr;
			});

			ActPropertiesOut actProps = new ActPropertiesOut();
			Guid[] propIds = hdr.pclsid?.value;
			if (propIds != null)
			{
				for (int i = 0; i < propIds.Length; i++)
				{
					Guid propId = propIds[i];
					var propSize = hdr.pSizes.value[i];
					var offProp = reader.Position;

					decoder = RpcEncoding.MsrpcNdr.CreateDecoder(reader, new RpcCallContext(null));

					if (propId == DcomIds.CLSID_ScmReplyInfo)
					{
						actProps.ScmReplyInfoData = decoder.DeserializeType1(dec =>
						{
							var s = dec.ReadFixedStruct<ScmReplyInfoData>(NdrAlignment.NativePtr);
							dec.ReadStructDeferral(ref s);
							return s;
						});
					}
					else if (propId == DcomIds.CLSID_PropsOutInfo)
					{
						actProps.PropsOutInfo = decoder.DeserializeType1(dec =>
						{
							var s = dec.ReadFixedStruct<PropsOutInfo>(NdrAlignment.NativePtr);
							dec.ReadStructDeferral(ref s);
							return s;
						});
					}
					else
					{
						// TODO: Bad data
					}

					var offPropEnd = (int)(offProp + propSize);
					Debug.Assert(reader.Position <= offPropEnd);
					reader.Position = offPropEnd;
				}
			}

			return actProps;
		}
	}
}
