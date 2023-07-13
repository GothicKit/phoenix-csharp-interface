using System.Collections.Generic;
using System.Numerics;
using PxCs.Data.Animation;

namespace PxCs.Data.Vob
{
    public enum PxMoverBehavior : uint
    {
        toggle = 0,
        triggerControl = 1,
        openTimed = 2,
        loop = 3,
        singleKeys = 4,
    }

    public enum PxMoverLerpMode : uint
    {
        curve = 0,
        linear = 1,
    }

    public enum PxMoverSpeedMode : uint
    {
        seg_constant = 0,
        slow_start_end = 1,
        slow_start = 2,
        slow_end = 3,
        seg_slow_start_end = 4,
        seg_slow_start = 5,
        seg_slow_end = 6,
    }

    public class PxVobTriggerMoverData : PxVobTriggerData
    {
        public PxMoverBehavior behaviour = PxMoverBehavior.toggle;
        public float touchBlockerDamage = 0;
        public float stayOpenTimeSec = 0;
        public bool locked = true;
        public bool autoLink = false;
        public bool auto_rotate = false;

        public float speed = 0;
        public PxMoverLerpMode lerp_mode = PxMoverLerpMode.curve;
        public PxMoverSpeedMode speed_mode = PxMoverSpeedMode.seg_constant;

        public List<PxAnimationSampleData>? keyframes;

        public string? sfx_open_start;
        public string? sfx_open_end;
        public string? sfx_transitioning;
        public string? sfx_close_start;
        public string? sfx_close_end;
        public string? sfx_lock;
        public string? sfx_unlock;
        public string? sfx_use_locked;

        // Save-game only variables
        public Vector3 s_act_key_pos_delta;
        public float s_act_keyframe_f;
        public int s_act_keyframe;
        public int s_next_keyframe;
        public float s_move_speed_unit;
        public float s_advance_dir;
        public uint s_mover_state;
        public int s_trigger_event_count;
        public float s_stay_open_time_dest;
    }
}
