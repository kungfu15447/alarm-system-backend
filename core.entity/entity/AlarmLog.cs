namespace AlarmSystem.Core.Entity.Entity {
    public class AlarmLog
    {
        public Machine Machine { get; set; }
        public Alarm Alarm { get; set; }
        public long Date { get; set; }
    }
}