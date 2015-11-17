namespace WcfServiceChat
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.ServiceModel;

    [ServiceContract]
    public interface IServiceChat
    {

        [OperationContract]
        void SendMessage(string message);

        [OperationContract]
        string GetAllMessages();
    }
}
