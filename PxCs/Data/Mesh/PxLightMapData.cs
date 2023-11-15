using System.Numerics;
using PxCs.Data.Texture;

namespace PxCs.Data.Mesh
{
    public class PxLightMapData
    {
        public PxTextureData image = default!;
        public Vector3[] normals = default!;
        public Vector3 origin;
    }
}