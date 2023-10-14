using System.Numerics;
using PxCs.Data.Struct;

namespace PxCs.Data.BspTree
{
	public class PxBspNodeData
	{
		public Vector4 plane;
		public PxAABBData bbox;
		public uint polygonIndex;
		public uint polygonCount;
		public int frontNodeIndex;
		public int backNodeIndex;
		public int parentNodeIndex;
	}
}