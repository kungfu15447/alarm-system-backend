namespace AlarmSystem.Core.Entity.Dto {
    public class MachineWatch {
        public string MachineId { get; set; }
        public Machine Machine { get; set; }
        public string WatchId { get; set; }
    }
}