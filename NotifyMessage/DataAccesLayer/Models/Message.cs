namespace NotifyMessage.DataAccesLayer.Models
{
    public class Message
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public IEnumerable<int> Recipients { get; set; }
    }
}
