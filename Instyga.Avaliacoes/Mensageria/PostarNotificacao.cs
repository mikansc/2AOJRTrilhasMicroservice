using Avaliacoes.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Instyga.Avaliacoes.Mensageria
{

    public static class PostarNotificacao
    {
        public static void Postar(Notificacao notificacao)
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                UserName = "guest",
                Password = "guest",
                ContinuationTimeout = TimeSpan.FromSeconds(120)
            };

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    var properties = channel.CreateBasicProperties();
                    var messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(notificacao));
                    const string exchangeName = "amq.direct";

                    channel.BasicPublish(exchangeName, "", properties, messageBytes);
                }
            }
        }
    }
}
