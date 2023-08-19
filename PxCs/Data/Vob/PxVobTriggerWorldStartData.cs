namespace PxCs.Data.Vob
{
    public class PxVobTriggerWorldStartData : PxVobData
    {
        public string target = default!;
        public bool fireOnce;

        // Save-game only variables
        public bool sHasFired;
    }
}
