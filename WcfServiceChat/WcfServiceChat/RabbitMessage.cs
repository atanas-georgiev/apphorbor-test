namespace WcfServiceChat
{
    using System.Collections.Generic;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class RabbitMessage
    {
        //amqp://9C5Djfc8:dYyTjbGPYvSJyVJB9EiH3liKpSzWyUEe@black-holly-59.bigwig.lshift.net:10916/u10ILtGVrwfV
        private const string RabbitmqBigwigUrl = "amqp://kagkkxnu:KtOwBDLjNxqjwQj51XP-bkJLOhfmo2ji@hare.rmq.cloudamqp.com/kagkkxnu";
        private const string QueueName = "chatMessage";

        public static void Send(string message)
        {
            var factory = new ConnectionFactory();
            factory.HostName = RabbitmqBigwigUrl;
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                channel.QueueDeclare(QueueName, true, false, false, null);

                // Set delivery mode (1 = non Persistent | 2 = Persistent)
                var props = channel.CreateBasicProperties();
                props.DeliveryMode = 2;
                                
                byte[] body = System.Text.Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(string.Empty, QueueName, props, body);                
            }
        }

        public static string Receive()
        {
            var messages = string.Empty;
            var factory = new ConnectionFactory();
            factory.HostName = RabbitmqBigwigUrl;
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(QueueName, true, false, false, null);

                var consumer = new QueueingBasicConsumer(channel);

                channel.BasicConsume(QueueName, false, consumer);

                //while (true)
                //{
                    var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                    
                    byte[] body = ea.Body;
                    string message = System.Text.Encoding.UTF8.GetString(body);
                    messages = message;

                    // Add some time to simulate processing
                    //Thread.Sleep(5000);

                    // Acknowledge message received and processed
                 //   System.Console.WriteLine(" Processed ", message);
                    channel.BasicAck(ea.DeliveryTag, false);
               // }
            }

            return messages;
        }

    }
}
