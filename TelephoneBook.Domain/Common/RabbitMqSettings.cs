namespace TelephoneBook.Domain.Common
{
    public class RabbitMqSettings
    {
        public string HostName { get; set; }
        public int Port { get; set; } = 5672;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; } = "/";
        //public string Exchange { get; set; }
        //public string Queue { get; set; }
        //public string RoutingKey { get; set; }
        public int RetryCount { get; set; } = 3;
    }
}
