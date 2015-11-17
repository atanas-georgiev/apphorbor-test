namespace WcfServiceChat
{
    using System.Collections.Generic;

    public class ServiceChat : IServiceChat
    {
        public void SendMessage(string message)
        {
            RabbitMessage.Send(message);
        }

        public IEnumerable<string> GetAllMessages()
        {
            return RabbitMessage.Receive();
        }
    }
}
