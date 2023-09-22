using System.Numerics;

namespace PxCs.Data.Vob
{
    public class PxVobNpcData : PxVobData
    {
        public class PxVobNpcTalentData
        {
            public int talent;
            public int value;
            public int skill;
        }

        public class PxVobNpcSlotData
        {
            public bool used;
            public string name = default!;
            public int itemIndex;
            public bool inInventory;
        }

        public string npcInstance = default!;
        public Vector3 modelScale;
        public float modelFatness;
        public string[]? overlays;
        public int flags;
        public int guild;
        public int guildTrue;
        public int level;
        public int xp;
        public int xpNextLevel;
        public int lp;
        public PxVobNpcTalentData[]? talents;
        public int fightTactic;
        public int fightMode;
        public bool wounded;
        public bool mad;
        public int madTime;
        public bool player;
        public int[]? attributes;
        public int[]? hcs;
        public int[]? missions;
        public string startAiState = default!;
        public int[]? aivar;
        public string scriptWaypoint = default!;
        public int attitude;
        public int attitudeTemp;
        public int nameNr;
        public bool moveLock;
        public string[]? packed;
        public PxVobItemData[]? items;
        public PxVobNpcSlotData[]? slots;
        public bool currentStateValid;
        public string currentStateName = default!;
        public int currentStateIndex;
        public bool currentStateIsRoutine;
        public bool nextStateValid;
        public string nextStateName = default!;
        public int nextStateIndex;
        public bool nextStateIsRoutine;
        public int lastAiState;
        public bool hasRoutine;
        public bool routineChanged;
        public bool routineOverlay;
        public int routineOverlayCount;
        public int walkmodeRoutine;
        public bool weaponmodeRoutine;
        public bool startNewRoutine;
        public int aiStateDriven;
        public Vector3 aiStatePos;
        public string currentRoutine = default!;
        public bool respawn;
        public int respawnTime;
        public int[]? protection;
        public int bsInterruptableOverride;
        public int npcType;
        public int spellMana;
    }
}