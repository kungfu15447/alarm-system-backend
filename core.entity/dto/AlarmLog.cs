namespace AlarmSystem.Core.Entity.Dto {
    public class AlarmLog
    {
        public string MachindId { get; set; }
        public Machine Machine { get; set; }
        public string AlarmId { get; set; }
        public Alarm Alarm { get; set; }
        public long Date { get; set; }
    }
}