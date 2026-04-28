using System.Text;

namespace Titanis.Linterop.Fuse
{
	/// <summary>
	/// Describes an extended attribute.
	/// </summary>
	public struct XAttrData
	{
		public XAttrData(byte[] data)
		{
			Data = data;
			this.RequiredSize = data.Length;
		}
		public XAttrData(string? text)
		{
			Data = string.IsNullOrEmpty(text) ? [] : Encoding.UTF8.GetBytes(text);
			this.RequiredSize = this.Data.Length;
		}
		public XAttrData(int requiredSize)
		{
			RequiredSize = requiredSize;
		}

		private XAttrData(bool present)
		{
			this.RequiredSize = -1;
		}

		public static XAttrData NotPresent => new XAttrData(false);

		public byte[]? Data { get; }
		public int RequiredSize { get; }
		public bool IsPresent => this.RequiredSize >= 0;
	}
}