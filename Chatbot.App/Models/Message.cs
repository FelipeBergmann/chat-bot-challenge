namespace Chatbot.App.Models
{
    public class Message
    {
        public Message(string userName, string body, string myUserName, DateTime sentAt) : this(userName, body, false, sentAt)
        {
            Mine = IsMine(myUserName);
        }

        public Message(string username, string body, bool mine, DateTime sentAt)
        {
            Username = username;
            Body = body;
            Mine = mine;
            SentAt = sentAt;
        }

        public string Username { get; set; }
        public string Body { get; set; }
        public bool Mine { get; set; }
        public DateTime SentAt { get; set; }

        public bool IsNotice => Body.StartsWith("[Notice]");

        public string CSS => Mine ? "sent" : "received";

        private bool IsMine(string userName) => Username.Equals(userName, StringComparison.OrdinalIgnoreCase);
    }
}
