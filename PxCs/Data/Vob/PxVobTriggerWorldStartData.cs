namespace PxCs.Data.Vob
{
    public class PxVobTriggerWorldStartData : PxVobData
    {
        public string? target;
        public bool fireOnce;

        // Save-game only variables
        public bool sHasFired;
    }
}
