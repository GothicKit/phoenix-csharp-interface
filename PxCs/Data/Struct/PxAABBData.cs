﻿using System;
using System.Numerics;

namespace PxCs.Data.Struct
{
    /// <summary>
    /// Represents a axis-aligned bounding box (AABB).
    /// 
    /// Needs to be struct as it will be auto-marshaled.
    /// </summary>
    [Serializable]
    public struct PxAABBData
    {
        public Vector3 min;
        public Vector3 max;
    }
}
