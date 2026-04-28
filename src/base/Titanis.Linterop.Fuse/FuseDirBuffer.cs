namespace Titanis.Linterop.Fuse;

struct FuseDirBuffer
{
	internal FuseDirBuffer(fuse_req_t req, size_t maxSize)
	{
		int cbBuf = (int)Math.Min((uint)int.MaxValue, maxSize.value.ToUInt64());
		this._bytes = new byte[cbBuf];
		this.req = req;
	}

	private readonly byte[] _bytes;
	private readonly fuse_req_t req;
	private int _writeIndex;

	internal bool TryAppend(in stat stat, string name, off_t nextOffset)
	{
		var cbRem = this._bytes.Length - this._writeIndex;

		int cbConsumed;
		unsafe
		{
			fixed (byte* pBuf = this._bytes)
			{
				cbConsumed = (int)FuseNativeMethods.fuse_add_direntry(
					this.req,
					pBuf + this._writeIndex,
					cbRem,
					name,
					in stat,
					nextOffset
					).value.ToUInt32();
			}
		}
		if (cbConsumed > cbRem)
			return false;

		this._writeIndex += cbConsumed;
		return true;
	}

	internal ArraySegment<byte> AsSegment()
	{
		return new ArraySegment<byte>(this._bytes, 0, this._writeIndex);
	}
}
