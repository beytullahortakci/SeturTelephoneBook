using AutoMapper;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TelephoneBook.Application.Interfaces;
using TelephoneBook.Domain.Common;
using TelephoneBook.Domain.Entities;
using TelephoneBook.Domain.Enums;
using TelephoneBook.Infrastructure.Interfaces;

namespace TelephoneBook.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IRepository<Report> _repository;
        private readonly RabbitMqSettings _rabbitMqSettings;
        public ReportService(IRepository<Report> repository, IOptions<RabbitMqSettings> options)
        {
            _repository = repository;
            _rabbitMqSettings = options.Value;
        }

        public async Task<Result<Report>> CreateReportAsync(string location)
        {
           var entity= await _repository.AddAsync(new Report()
            {
                CreatedDate = DateTime.Now,
                Status = ReportStatus.Preparing,
                ReportDetail = new ReportDetail()
                {
                    Location = location
                }
            });

            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqSettings.HostName,
                Port = _rabbitMqSettings.Port,
                UserName = _rabbitMqSettings.UserName,
                Password = _rabbitMqSettings.Password,
                VirtualHost = _rabbitMqSettings.VirtualHost
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "report-queue", durable: false, exclusive: false, autoDelete: false);

            var message = JsonSerializer.Serialize(new { Report = entity });
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: "report-queue", basicProperties: null, body: body);

            return new Result<Report>(true, null, null);
        }

        public async Task<Result<List<Report>>> GetAllReportsAsync()
        {
            var data = await _repository.GetAllAsync();
            return new Result<List<Report>>(true, null, data.ToList());
        }

        public async Task<Result<Report?>> GetReportByIdAsync(string id)
        {
            var contact = await _repository.GetByIdAsync(id);
            if (contact == null)
                return new Result<Report?>(false, "Report not found", null);

            return new Result<Report?>(true, null, contact);
        }

        public Task<Result<Report>> ReportCompletedAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
