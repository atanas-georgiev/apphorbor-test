namespace WcfChatService
{
    using System;
    using System.Configuration;
    using System.Text;

    using RabbitMQ.Client;

    public static class SendMq
    {
        const string connString = "amqp://kagkkxnu:KtOwBDLjNxqjwQj51XP-bkJLOhfmo2ji@hare.rmq.cloudamqp.com/kagkkxnu";

        public static void Send(ChatMessage message)
        {
            ConnectionFactory connFactory = new ConnectionFactory
            {
                Uri = connString
            };

            using (var conn = connFactory.CreateConnection())
            {
                using (var channel = conn.CreateModel())
                {
                    // The message we want to put on the queue
                    //var message = DateTime.Now.ToString("F");

                    // the data put on the queue must be a byte array
                    var data = Encoding.UTF8.GetBytes("AAAAAAAAAAAAAAAAA");

                    // ensure that the queue exists before we publish to it
                    channel.QueueDeclare("queue1", false, false, false, null);

                    // publish to the "default exchange", with the queue name as the routing key
                    channel.BasicPublish("", "queue1", null, data);
                }
            }
        }
    }
}
