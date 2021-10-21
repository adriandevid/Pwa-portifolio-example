namespace AdrianP.Models
{
    public class UserNotify
    {
        public long id { get; set; }
        public string userIdentity { get; set; }
        public string endPoint { get; set; }
        public string p256dh { get; set; }
        public string auth { get; set; }
    }
}
