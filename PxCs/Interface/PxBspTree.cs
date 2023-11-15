using System;
using System.Numerics;
using System.Runtime.InteropServices;
using PxCs.Data.BspTree;
using PxCs.Data.Struct;
using PxCs.Extensions;

namespace PxCs.Interface
{
	public class PxBspTree
	{
		private const string DLLNAME = PxPhoenix.DLLNAME;

		public enum PxBspTreeMode
		{
			PxBspIndoor = 0,
			PxBspOutdoor = 1,
		}

		[DllImport(DLLNAME)] public static extern PxBspTreeMode pxBspGetMode(IntPtr bsp);
		[DllImport(DLLNAME)] public static extern ulong pxBspGetPolygonIndicesLength(IntPtr bsp);
		[DllImport(DLLNAME)] public static extern IntPtr pxBspGetPolygonIndices(IntPtr bsp, out ulong size);
		[DllImport(DLLNAME)] public static extern ulong pxBspGetLeafPolygonIndicesLength(IntPtr bsp);
		[DllImport(DLLNAME)] public static extern IntPtr pxBspGetLeafPolygonIndices(IntPtr bsp, out ulong size);
		[DllImport(DLLNAME)] public static extern ulong pxBspGetPortalPolygonIndicesLength(IntPtr bsp);
		[DllImport(DLLNAME)] public static extern IntPtr pxBspGetPortalPolygonIndices(IntPtr bsp, out ulong size);
		[DllImport(DLLNAME)] public static extern ulong pxBspGetLeafNodeIndicesLength(IntPtr bsp);
		[DllImport(DLLNAME)] public static extern IntPtr pxBspGetLeafNodeIndices(IntPtr bsp, out ulong size);
		[DllImport(DLLNAME)] public static extern ulong pxBspGetLightPointsLength(IntPtr bsp);
		[DllImport(DLLNAME)] public static extern Vector3 pxBspGetLightPoint(IntPtr bsp, ulong idx);
		[DllImport(DLLNAME)] public static extern ulong pxBspGetSectorsLength(IntPtr bsp);
		[DllImport(DLLNAME)] public static extern void pxBspGetSector(IntPtr bsp, ulong idx, out IntPtr name, out IntPtr nodeIndices,
			out ulong nodeIndicesLength, out IntPtr portalPolygonIndices, out ulong portalPolygonIndicesLength);
		[DllImport(DLLNAME)] public static extern ulong pxBspGetNodesLength(IntPtr bsp);
		[DllImport(DLLNAME)] public static extern void pxBspGetNode(IntPtr bsp, ulong idx, out Vector4 plane, out PxAABBData bbox,
			out uint polygonIndex, out uint polygonCount, out int frontNodeIndex, out int backNodeIndex,
			out int parentNodeIndex);


		public static uint[] GetPolygonIndices(IntPtr bsp)
		{
			return pxBspGetPolygonIndices(bsp, out ulong size).MarshalAsArray<uint>((uint)size);
		}

		public static uint[] GetLeafPolygonIndices(IntPtr bsp)
		{
			return pxBspGetLeafPolygonIndices(bsp, out ulong size).MarshalAsArray<uint>((uint)size);
		}
		
		public static uint[] GetPortalPolygonIndices(IntPtr bsp)
		{
			return pxBspGetPortalPolygonIndices(bsp, out ulong size).MarshalAsArray<uint>((uint)size);
		}
		
		public static ulong[] GetLeadNodeIndices(IntPtr bsp)
		{
			return pxBspGetLeafNodeIndices(bsp, out ulong size).MarshalAsArray<ulong>((uint)size);
		}

		public static Vector3[] GetLightPoints(IntPtr bsp)
		{
			var length = pxBspGetLightPointsLength(bsp);
			var array = new Vector3[length];

			for (var i = 0u; i < length; ++i)
			{
				array[i] = pxBspGetLightPoint(bsp, i);
			}

			return array;
		}

		public static PxBspSectorData[] GetSectors(IntPtr bsp)
		{
			var length = pxBspGetSectorsLength(bsp);
			var array = new PxBspSectorData[length];
			
			for (var i = 0u; i < length; ++i)
			{
				pxBspGetSector(bsp, i, out IntPtr name, out IntPtr nodeIndices,
					out ulong nodeIndicesLength, out IntPtr portalPolygonIndices, out ulong portalPolygonIndicesLength);

				array[i] = new PxBspSectorData
				{
					name = name.MarshalAsString(),
					nodeIndices = nodeIndices.MarshalAsArray<uint>((uint)nodeIndicesLength),
					portalPolygonIndices = portalPolygonIndices.MarshalAsArray<uint>((uint)portalPolygonIndicesLength)
				};
			}

			return array;
		}
		
		public static PxBspNodeData[] GetNodes(IntPtr bsp)
		{
			var length = pxBspGetNodesLength(bsp);
			var array = new PxBspNodeData[length];
			
			for (var i = 0u; i < length; ++i)
			{
				array[i] = new PxBspNodeData();
				pxBspGetNode(bsp, i, out array[i].plane, out array[i].bbox, out array[i].polygonIndex,
					out array[i].polygonCount, out array[i].frontNodeIndex, out array[i].backNodeIndex,
					out array[i].parentNodeIndex);
			}

			return array;
		}
	}
}