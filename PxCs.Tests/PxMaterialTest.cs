using System;
using Xunit;

namespace PxCs.Tests
{
    public class PxMaterialTest : PxPhoenixTest
    {
        [Fact]
        public void Test_load_World_Material()
        {
            var vdfPtr = LoadVdf("Data/worlds.VDF");

            var worldPtr = PxWorld.pxWorldLoadFromVdf(vdfPtr, "world.zen");
            Assert.True(worldPtr != IntPtr.Zero, "World couldn't be loaded inside vdf.");

            var mesh = PxWorld.pxWorldGetMesh(worldPtr);

            var materials = PxMesh.GetMaterials(mesh);
            Assert.Equal(2263u, materials.LongLength);

            var testMaterial = materials[10];
            Assert.True(testMaterial.name == "OWODWATRGRASSMIMOUNTAINCLOSE", "Material name is wrong with >" + testMaterial.name + "<");
            Assert.True(testMaterial.texture == "OWODWATRGRASSMIMOUNTAINCLOSE.TGA", "TExture name is wrong with >" + testMaterial.texture + "<");
            Assert.True(testMaterial.color != 0, "Color has no value");

            PxWorld.pxWorldDestroy(worldPtr);
            DestroyVdf(vdfPtr);
        }
    }
}
