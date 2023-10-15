using System;

namespace PxCs.Data.BspTree
{
	[Serializable]
	public class PxBspSectorData
	{
		public string name;
		public uint[] nodeIndices;
		public uint[] portalPolygonIndices;
	}
}