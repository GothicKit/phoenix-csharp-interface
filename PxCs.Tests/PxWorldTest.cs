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

            // var npc = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_oCNpc);
            // Assert.IsType<PxVobNpcData>(npc);

            // var moverController = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCMoverController);
            // Assert.IsType<PxVobMoverControllerData>(moverController);

            var pfxController = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCPFXController);
            Assert.IsType<PxVobPfxControllerData>(pfxController);

            var animate = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCVobAnimate);
            Assert.IsType<PxVobAnimateData>(animate);

            // var lensFlare = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCVobLensFlare);
            // Assert.IsType<PxVobLensFlareData>(lensFlare);

            var light = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCVobLight);
            Assert.IsType<PxVobLightData>(light);

            // var messageFilter = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCMessageFilter);
            // Assert.IsType<PxVobMessageFilterData>(messageFilter);

            // var codeMaster = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCCodeMaster);
            // Assert.IsType<PxVobCodeMasterData>(codeMaster);

            // var triggerWorldStart = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCTriggerWorldStart);
            // Assert.IsType<PxVobTriggerListData>(triggerWorldStart);

            // var touchDamage = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_oCTouchDamage);
            // Assert.IsType<PxVobTouchDamageData>(touchDamage);

            // var triggerUntouch = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCTriggerUntouch);
            // Assert.IsType<PxVobTriggerUntouchData>(triggerUntouch);

            // var earthquake = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCEarthquake);
            // Assert.IsType<PxVobEarthQuakeData>(earthquake);

            var chest = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_oCMobContainer);
            Assert.IsType<PxVobMobContainerData>(chest);

            // In world.zen there is no Trigger. Therefore commenting this line out.
            // var trigger = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCTrigger);
            // Assert.IsType<PxVobTriggerData>(trigger);

            var triggerList = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCTriggerList);
            Assert.IsType<PxVobTriggerListData>(triggerList);

            // var triggerScript = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_oCTriggerScript);
            // Assert.IsType<PxVobTriggerScriptData>(triggerScript);

            var triggerLevelChange = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_oCTriggerChangeLevel);
            Assert.IsType<PxVobTriggerChangeLevelData>(triggerLevelChange);

            var moverTrigger = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCMover);
            Assert.IsType<PxVobTriggerMoverData>(moverTrigger);

            var sound = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCVobSound);
            Assert.IsType<PxVobSoundData>(sound);

            var soundDaytime = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCVobSoundDaytime);
            Assert.IsType<PxVobSoundDaytimeData>(soundDaytime);

            var zoneFogDefault = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCZoneZFogDefault);
            Assert.IsType<PxVobZoneFogData>(zoneFogDefault);

            var decalVob = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCVob && i.visualType == PxWorld.PxVobVisualType.PxVobVisualDecal);
            Assert.True(decalVob.vobDecal.HasValue);

            PxWorld.pxWorldDestroy(worldPtr);
            DestroyVfs(vfsPtr);
        }
    }
}
