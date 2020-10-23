namespace AlarmSystem.Core.Entity.Dto {
    public class AlarmWatch {
        public int AlarmId { get; set; }
        public Alarm Alarm { get; set; }
        public string WatchId { get; set; }
    }
}