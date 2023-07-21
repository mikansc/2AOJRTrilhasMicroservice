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
        private IModel channel = null;
        private IConnection connection = null;

        public RabbitMqListener(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            bool bindCreated = false, queueDeclared = false;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                ContinuationTimeout = TimeSpan.FromSeconds(120)
            };


            this.connection = factory.CreateConnection();
            this.channel = connection.CreateModel();

            if (!queueDeclared)
                channel.QueueDeclare("instyga-notifications", durable: true,
                         exclusive: false,
                         autoDelete: false);

            queueDeclared = true;

            if (!bindCreated)
                channel.QueueBind("instyga-notifications", "amq.direct", "");

            bindCreated = true;
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
            await Task.Yield();
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
