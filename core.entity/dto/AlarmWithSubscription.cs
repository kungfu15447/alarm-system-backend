namespace core.entity.dto
{
    public class AlarmWithSubscription
    {
        public int AlarmId { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public bool IsSubscribed { get; set; }
    }
}