namespace MicroServiceWithKafka.Configurations
{
    public class KafkaConfiguration
    {
        public string ServerHost { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConsumerTopic { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
    }
}
