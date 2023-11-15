using System;
using System.Linq;
using System.Numerics;
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

            var pfxController = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCPFXController);
            Assert.IsType<PxVobPfxControllerData>(pfxController);

            var animate = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCVobAnimate);
            Assert.IsType<PxVobAnimateData>(animate);

            var light = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCVobLight);
            Assert.IsType<PxVobLightData>(light);

            var chest = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_oCMobContainer);
            Assert.IsType<PxVobMobContainerData>(chest);

            var triggerList = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_zCTriggerList);
            Assert.IsType<PxVobTriggerListData>(triggerList);

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

        [Fact]
        public void Test_load_BspTree()
        {
            var vfsPtr = LoadVfs("Data/worlds.VDF");
            var worldPtr = PxWorld.pxWorldLoadFromVfs(vfsPtr, "world.zen");
            var bspPtr = PxWorld.pxWorldGetBspTree(worldPtr);

            var mode = PxBspTree.pxBspGetMode(bspPtr);
            Assert.Equal(PxBspTree.PxBspTreeMode.PxBspOutdoor, mode);

            var polygonIndices = PxBspTree.GetPolygonIndices(bspPtr);
            Assert.Equal(480135, polygonIndices.Length);
            Assert.Equal(102u, polygonIndices[150]);
            
            var sectors = PxBspTree.GetSectors(bspPtr);
            Assert.Equal(299, sectors.Length);
            Assert.Equal("WALD11", sectors[0].name);
            Assert.Equal(9, sectors[0].nodeIndices.Length);
            Assert.Equal(24, sectors[0].portalPolygonIndices.Length);
            
            var nodes = PxBspTree.GetNodes(bspPtr);
            Assert.Equal(6644, nodes.Length);
            Assert.Equal(new Vector4(1, 0, 0, 18540.0156f), nodes[0].plane);
            Assert.Equal(1, nodes[0].frontNodeIndex);
            Assert.Equal(1599, nodes[0].backNodeIndex);
            Assert.Equal(-1, nodes[0].parentNodeIndex);
            Assert.Equal(0u, nodes[0].polygonIndex);
            Assert.Equal(0u, nodes[0].polygonCount);
            
            PxWorld.pxWorldDestroy(worldPtr);
            DestroyVfs(vfsPtr);
        }
    }
}
