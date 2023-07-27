using System;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;
using PxCs.Data.Vob;
using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxWorldTest : PxPhoenixTest
    {
        [Fact]
        public void Test_load_World()
        {
            var vfsPtr = LoadVfs("Data/worlds.VDF");

            var worldPtr = PxWorld.pxWorldLoadFromVfs(vfsPtr, "world.zen");
            Assert.True(worldPtr != IntPtr.Zero, "World couldn't be loaded inside vdf.");

            PxWorld.pxWorldDestroy(worldPtr);
            DestroyVfs(vfsPtr);
        }

        [Fact]
        public void Test_load_WayPointsAndEdges()
        {
            var vfsPtr = LoadVfs("Data/worlds.VDF");

            var worldPtr = PxWorld.pxWorldLoadFromVfs(vfsPtr, "world.zen");
            Assert.True(worldPtr != IntPtr.Zero, "World couldn't be loaded inside vdf.");

            var wayPoints = PxWorld.GetWayPoints(worldPtr);
            Assert.Equal(2784L, wayPoints.LongLength);
            Assert.True(wayPoints[0].name == "LOCATION_28_07", "WayPoint name is wrong with >" + wayPoints[0].name + "<");

            var wayEdges = PxWorld.GetWayEdges(worldPtr);
            Assert.Equal(3500L, wayEdges.LongLength);
            Assert.True(wayEdges[0].a == 20, "WayEdge.a is wrong with >" + wayEdges[0].a + "<");

            PxWorld.pxWorldDestroy(worldPtr);
            DestroyVfs(vfsPtr);
        }

        [Fact]
        public void Test_load_Vobs()
        {
            var vfsPtr = LoadVfs("Data/worlds.VDF");
            var worldPtr = PxWorld.pxWorldLoadFromVfs(vfsPtr, "world.zen");

            var vobs = PxWorld.GetVobs(worldPtr);

            var item = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_oCItem);
            Assert.IsType<PxVobItemData>(item);
            
            var chest = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_oCMobContainer);
            Assert.IsType<PxVobMobContainerData>(chest);

            // In world.zen there is no Trigger. Therefore commenting this line out.
            // var trigger = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCTrigger);
            // Assert.IsType<PxVobTriggerData>(trigger);

            var triggerLevelChange = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_oCTriggerChangeLevel);
            Assert.IsType<PxVobTriggerChangeLevelData>(triggerLevelChange);

            var zoneFogDefault = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCZoneZFogDefault);
            Assert.IsType<PxVobZoneFogData>(zoneFogDefault);

            var sound = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCVobSound);
			Assert.IsType<PxVobSoundData>(sound);
			
            var soundDaytime = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCVobSoundDaytime);
			Assert.IsType<PxVobSoundDaytimeData>(soundDaytime);

            var decalVob = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCVob && i.visualType == PxWorld.PxVobVisualType.PxVobVisualDecal);
            Assert.True(decalVob.vobDecal.HasValue);
            
			PxWorld.pxWorldDestroy(worldPtr);
            DestroyVfs(vfsPtr);
        }
    }
}
