using System;
using PxCs.Interface;

namespace PxCs.Data.Vm
{
    public class PxVmItemData : PxVmData
    {
        public int id;
        public string name = default!;
        public string nameId = default!;
        public int hp;
        public int hpMax;
        public PxVm.PxVmItemFlags mainFlag;
        public PxVm.PxVmItemFlags flags;
        public int weight;
        public int value;
        public int damageType;
        public int damageTotal;
        public int[] damage = default!;
        public int wear;
        public int[] protection = default!;
        public int nutrition;
        public int[] condAtr = default!;
        public int[] condValue = default!;
        public int[] changeAtr = default!;
        public int[] changeValue = default!;
        public int magic;
        public int onEquip;
        public int onUnequip;
        public int[] onState = default!;
        public int owner;
        public int ownerGuild;
        public int disguiseGuild;
        public string visual = default!;
        public string visualChange = default!;
        public string effect = default!;
        public int visualSkin;
        public string schemeName = default!;
        public int material;
        public int munition;
        public int spell;
        public int range;
        public int magCircle;
        public string description = default!;
        public string[] text = default!;
        public int[] count = default!;
        public int invZbias;
        public int invRotX;
        public int invRotY;
        public int invRotZ;
        public int invAnimate;
    }
}
