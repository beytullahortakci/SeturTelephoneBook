using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TelephoneBook.Domain.Entities;
using TelephoneBook.Domain.Enums;
using TelephoneBook.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using TelephoneBook.Domain.Common;
using System;

namespace TelephoneBook.Infrastructure.Consumers
{
    public class ReportWorker : BackgroundService
    {
        private readonly RabbitMqSettings _settings;
        private IConnection _connection;
        private IModel _channel;

        private readonly IServiceProvider _serviceProvider;

        public ReportWorker(IOptions<RabbitMqSettings> options,IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _settings = options.Value;

            var factory = new ConnectionFactory()
            {
                HostName = _settings.HostName,
                Port = _settings.Port,
                UserName = _settings.UserName,
                Password = _settings.Password,
                VirtualHost = _settings.VirtualHost
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "report-queue", durable: false, exclusive: false, autoDelete: false);
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var reportData = JsonSerializer.Deserialize<ReportMessage>(message);

                Console.WriteLine($"Received report: {reportData.Report}");

                var filter = Builders<Report>.Filter.Eq(p => p.Id, reportData.Report.Id);
                var repository = _serviceProvider.GetRequiredService<IRepository<Report>>();

                var reports = await repository.FilterAsync(filter);
                
                var report = new Report
                {
                    CreatedDate = DateTime.Now,
                    ReportDetail = new ReportDetail()
                    {
                        ContactCount = reports.Count(),
                        PhoneNumberCount = 0
                    },
                    Status = ReportStatus.Completed
                };

                await repository.UpdateAsync(reportData.Report.Id, report);

                _channel.BasicAck(ea.DeliveryTag, false);

                await Task.Yield();
            };

            _channel.BasicConsume(queue: "report-queue", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }

    public class ReportMessage
    {
        public Report Report { get; set; }
    }
}
