namespace AlarmSystem.Core.Entity.Dto {
    public class AlarmLog
    {
        public Machine Machine { get; set; }
        public string MachineId { get; set; }
        public Alarm Alarm { get; set; }
        public int AlarmId { get; set; }
        public long Date { get; set; }
    }
}