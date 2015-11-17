namespace WcfChatService
{
    using System;

    public class Chat : IChat
    {
        public void SendMessage(ChatMessage message)
        {
            SendMq.Send(message);
        }

        public ChatMessage[] GetAllMessages()
        {
            return null;
        }
    }
}
