using System;

namespace PxCs.Data.Vm
{
    [Serializable]
    public class PxVmPfxData : PxVmData
    {
		// 1) Emitter: zeitliches  Austoss-Verhalten, particles-per-second
		public float					ppsValue;
		public string					ppsScaleKeys = default!;
		public bool						ppsIsLooping;
		public bool						ppsIsSmooth;
		public float					ppsFPS;
		public string					ppsCreateEm;
		public float					ppsCreateEmDelay;

		// 2) Emitter: raeumliches Austoss-Verhalten
		public string					shpType = default!;		//	"point, line, box, circle, sphere, mesh"
		public string					shpFOR = default!;		//	"object,world"
		public string					shpOffsetVec = default!;
		public string					shpDistribType = default!;//	"RAND, UNIFORM, WALK"
		public float					shpDistribWalkSpeed;
		public bool						shpIsVolume;
		public string					shpDim = default!;		//	"", "30", "10 20 30", "30", "30", "" //	line: nur 1 Dimension !=0 // shape Dimensions
		public string					shpMesh = default!;		//	"cross.3ds"
		public bool						shpMeshRender;
		public string					shpScaleKeys = default!;	//	"[1.0] [0.8 0.9 0.2] [1.0]"
		public bool						shpScaleIsLooping;
		public bool						shpScaleIsSmooth;
		public float					shpScaleFPS;

		// 3) Partikel: Start Richtung/Speed:
		public string					dirMode = default!;		//	"DIR, TARGET, MESH_POLY"
		public string					dirFOR = default!;		//	"OBJECT, WORLD"
		public string					dirModeTargetFOR = default!;
		public string					dirModeTargetPos = default!;//	"30 23 67"
		public float					dirAngleHead;
		public float					dirAngleHeadVar;
		public float					dirAngleElev;
		public float					dirAngleElevVar;
		public float					velAvg;
		public float					velVar;

		// 4) Partikel: Lebensdauer
		public float					lspPartAvg;
		public float					lspPartVar;

		// 5) Partikel: Flugverhalten (gravity, nicht-linear?, mesh-selfRot?,..)
		// grav: a) nur Y, b) XYZ, c) auf Ziel zu steuern
		// public string  flyMode_S;								//	"LINEAR, LIN_SINUS,.."
		// flyMeshSelfRotSpeedMin, flyMeshSelfRotSpeedMax
		public string					flyGravity = default!;
		public bool						flyCollDet;

		// 6) Partikel: Visualisierung
		public string					visName = default!;		//	"NAME_V0_A0.TGA/.3DS"	(Variation, Animation)
		public string					visOrientation = default!;//	"NONE, VELO"
		public bool						visTexIsQuadPoly;			//	0=triMesh, 1=quadMesh
		public float					visTexAniFPS;
		public bool						visTexAniIsLooping;			//	0=oneShot, 1=looping

		// color		(nur Tex, lifeSpan-Sync)
		public string					visTexColorStart = default!;
		public string					visTexColorEnd = default!;

		// size-ani		(nur Tex, lifeSpan-Sync)
		public string					visSizeStart = default!;
		public float					visSizeEndScale;

		// alpha		(lifeSpan-Sync)
		public string					visAlphaFunc = default!;
		public float					visAlphaStart;
		public float					visAlphaEnd;

		// 7) misc effects

		// trail
		public float					trlFadeSpeed;
		public string					trlTexture = default!;
		public float					trlWidth;

		// marks
		public float					mrkFadeSpeed;
		public string					mrkTexture = default!;
		public float					mrkSize;
    }
}
