using System.Numerics;

namespace PxCs.Data.Misc
{
    /// <summary>
    /// Represents an oriented bounding box (OBB).
	///
	/// In contrast to regular bounding boxes, oriented bounding boxes:
    /// https://en.wikipedia.org/wiki/Minimum_bounding_box#Arbitrarily_oriented_minimum_bounding_box
    /// may be rotated in the coordinate system and don't have to align with its axes.
    /// </summary>
    public class PxOBBData
    {
        public Vector3 center;
        public Vector3[]? axes; // exactly [3]
        public Vector3[]? halfWidth;

        PxOBBData[]? children;
    }
}
