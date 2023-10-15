using System;

namespace PxCs.Data.Mesh
{
	[Serializable]
	public struct PxPolygonFlagsData
	{
		public byte isPortal;
		public byte isOccluder;
		public byte isSector;
		public byte shouldRelight;
		public byte isOutdoor;
		public byte isGhostOccluder;
		public byte isDynamicallyLit;
		public ushort sectorIndex;
		public byte isLod;
		public byte normalAxis;
	}
}