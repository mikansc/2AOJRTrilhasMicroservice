using Instyga.Notificacoes.Services;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using Notificacoes.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Instyga.Notificacoes.HostedServices
{

    public class RabbitMqListener : IHostedService
    {
        private readonly IServiceProvider serviceProvider;

        public RabbitMqListener(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory { 
                HostName = "rabbitmq",
                UserName = "guest",
                Password = "guest",
                ContinuationTimeout = TimeSpan.FromSeconds(120)
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("instyga-notifications", durable: true,
                     exclusive: false,
                     autoDelete: false);

            channel.QueueBind("instyga-notifications", "amq.direct", "");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                using (var scope = serviceProvider.CreateScope()) // this will use `IServiceScopeFactory` internally
                {
                    try
                    {
                        var service = scope.ServiceProvider.GetService<INotificacoesService>();
                        var body = eventArgs.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        var notificacao = JsonSerializer.Deserialize<Notificacao>(message);

                        service.Incluir(notificacao);
                    }
                    catch
                    {
                    }
                }
            };

            channel.BasicConsume(queue: "instyga-notifications",
                     autoAck: true,
                     consumer: consumer);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            return;
        }
    }
}
