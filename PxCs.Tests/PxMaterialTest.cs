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
            var materialCount = PxMesh.pxMshGetMaterialCount(mesh);
            Assert.Equal(2263u, materialCount);

            var material = PxMesh.pxMshGetMaterial(mesh, 10); // Picking some random element.
            Assert.True(material != IntPtr.Zero, "Material couldn't be loaded.");
            
            var color = PxMaterial.pxMatGetColor(material);
            var name = PxMaterial.pxMatGetName(material);
            var texture = PxMaterial.pxMatGetTexture(material);
            Assert.True(color != 0, "Color has no value");
            Assert.True(name == "OWODWATRGRASSMIMOUNTAINCLOSE", "Material name is wrong with >" + name + "<");
            Assert.True(texture == "OWODWATRGRASSMIMOUNTAINCLOSE.TGA", "TExture name is wrong with >" + texture + "<");

            PxWorld.pxWorldDestroy(worldPtr);
            DestroyVdf(vdfPtr);
        }
    }
}
