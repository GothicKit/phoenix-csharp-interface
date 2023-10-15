using System;

namespace PxCs.Data.Mesh
{
    [Serializable]
	public struct PxPolygonData
	{
		public uint materialIndex;
		public int lightmapIndex; // -1 if no lightmap assigned
		public PxPolygonFlagsData flags;
		public uint[] vertexIndices;
		public uint[] featureIndices;
	}
}