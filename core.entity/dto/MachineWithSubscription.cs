namespace AlarmSystem.Core.Entity.Dto
{
    public class MachineWithSubscription
    {
        public string MachineId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsSubscribed { get; set; }
    }
}