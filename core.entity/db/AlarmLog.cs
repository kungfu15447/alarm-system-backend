namespace AlarmSystem.Core.Entity.DB {
    public class AlarmLog
    {
        public Machine Machine { get; set; }
        public Alarm Alarm { get; set; }
        public long Date { get; set; }
    }
}