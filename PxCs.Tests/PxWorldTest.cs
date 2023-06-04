using System;
using System.Linq;
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
            var vdfPtr = LoadVdf("Data/worlds.VDF");

            var worldPtr = PxWorld.pxWorldLoadFromVdf(vdfPtr, "world.zen");
            Assert.True(worldPtr != IntPtr.Zero, "World couldn't be loaded inside vdf.");

            PxWorld.pxWorldDestroy(worldPtr);
            DestroyVdf(vdfPtr);
        }

        [Fact]
        public void Test_load_WayPointsAndEdges()
        {
            var vdfPtr = LoadVdf("Data/worlds.VDF");

            var worldPtr = PxWorld.pxWorldLoadFromVdf(vdfPtr, "world.zen");
            Assert.True(worldPtr != IntPtr.Zero, "World couldn't be loaded inside vdf.");

            var wayPoints = PxWorld.GetWayPoints(worldPtr);
            Assert.Equal(2784L, wayPoints.LongLength);
            Assert.True(wayPoints[0].name == "LOCATION_28_07", "WayPoint name is wrong with >" + wayPoints[0].name + "<");

            var wayEdges = PxWorld.GetWayEdges(worldPtr);
            Assert.Equal(3500L, wayEdges.LongLength);
            Assert.True(wayEdges[0].a == 20, "WayEdge.a is wrong with >" + wayEdges[0].a + "<");

            PxWorld.pxWorldDestroy(worldPtr);
            DestroyVdf(vdfPtr);
        }

        [Fact]
        public void Test_load_Vobs()
        {
            var vdfPtr = LoadVdf("Data/worlds.VDF");
            var worldPtr = PxWorld.pxWorldLoadFromVdf(vdfPtr, "world.zen");

            var vobs = PxWorld.GetVobs(worldPtr);

            var chest = vobs[0].childVobs!.First(i => i.type == PxWorld.PxVobType.PxVob_oCMobContainer);
            Assert.IsType<PxVobMobContainerData>(chest);

            PxWorld.pxWorldDestroy(worldPtr);
            DestroyVdf(vdfPtr);
        }
    }
}
