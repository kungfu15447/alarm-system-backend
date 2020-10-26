namespace AlarmSystem.Core.Entity.Dto {
    public class AlarmLog
    {
        public int Id { get; set; }
        public Machine Machine { get; set; }
        public Alarm Alarm { get; set; }
        public long Date { get; set; }
    }
}