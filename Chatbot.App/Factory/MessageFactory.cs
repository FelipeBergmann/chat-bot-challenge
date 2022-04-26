using Chatbot.App.Models;

namespace Chatbot.App.Factory
{
    public static class MessageFactory
    {
        public static Message CreateMessage(string userName, string body, string myUserName, DateTime sentAt)
        {
            return new Message(userName, body, myUserName, sentAt);
        }

        public static Message CreateMessage(string userName, string body, bool mine, DateTime sentAt)
        {
            return new Message(userName, body, mine, sentAt);
        }
    }
}
