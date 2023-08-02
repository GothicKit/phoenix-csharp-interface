using System;
using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxMaterialTest : PxPhoenixTest
    {
        [Fact]
        public void Test_load_World_Material()
        {
            var vfsPtr = LoadVfs("Data/worlds.VDF");

            var worldPtr = PxWorld.pxWorldLoadFromVfs(vfsPtr, "world.zen");
            Assert.True(worldPtr != IntPtr.Zero, "World couldn't be loaded inside vdf.");

            var mesh = PxWorld.pxWorldGetMesh(worldPtr);

            var materials = PxMesh.GetMaterials(mesh);
            Assert.Equal(2263u, materials.LongLength);

            var testMaterial = materials[10];
            Assert.True(testMaterial.name == "OWODWATRGRASSMIMOUNTAINCLOSE", "Material name is wrong with >" + testMaterial.name + "<");
            Assert.True(testMaterial.texture == "OWODWATRGRASSMIMOUNTAINCLOSE.TGA", "TExture name is wrong with >" + testMaterial.texture + "<");
            Assert.True(testMaterial.color != 0, "Color has no value");
            Assert.True(testMaterial.group == PxMaterial.PxMaterialGroup.PxMaterialGroup_Stone, "Stone as materialGroup is expected, but got ");
            PxWorld.pxWorldDestroy(worldPtr);
            DestroyVfs(vfsPtr);
        }
    }
}
