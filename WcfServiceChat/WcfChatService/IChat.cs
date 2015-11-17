namespace WcfChatService
{
    using System;
    using System.Runtime.Serialization;
    using System.ServiceModel;

    [ServiceContract]
    public interface IChat
    {

        [OperationContract]
        void SendMessage(ChatMessage message);

        [OperationContract]
        ChatMessage[] GetAllMessages();
    }

    [DataContract]
    public class ChatMessage
    {        
        [DataMember]
        public string Sender { get; set; }

        [DataMember]
        public DateTime Time { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
