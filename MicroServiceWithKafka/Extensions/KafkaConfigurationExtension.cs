using Confluent.Kafka;
using MassTransit;
using MicroServiceWithKafka.Configurations;
using MicroServiceWithKafka.Consumer;
using MicroServiceWithKafka.MessageDto;

namespace MicroServiceWithKafka.Extensions
{
    public static class KafkaConfigurationExtension
    {
        public static IServiceCollection AddKafkaConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.Configure<KafkaConfiguration>(configuration.GetSection("configurations"));
            var configurationsKafka = configuration.GetSection("configurations").Get<KafkaConfiguration>();

            return services.AddMassTransit(x =>
            {
                x.UsingInMemory();
                x.AddRider(rider =>
                {
                    rider.AddConsumer<ConsumerMessageRealTime>();
                    rider.AddProducer<KafkaMessage>(configurationsKafka.ConsumerTopic);

                    rider.UsingKafka((context, k) =>
                    {
                        k.Host(server: configurationsKafka.ServerHost, configure: hostCfg =>
                        {
                            hostCfg.UseSasl(saslCfg =>
                            {
                                saslCfg.SecurityProtocol = Confluent.Kafka.SecurityProtocol.SaslSsl;
                                saslCfg.Mechanism = SaslMechanism.Plain;
                                saslCfg.Username = configurationsKafka.Username;
                                saslCfg.Password = configurationsKafka.Password;
                            });
                            hostCfg.UseSsl(sslCfg =>
                            {
                                sslCfg.EnableCertificateVerification = false;
                            });
                            k.TopicEndpoint<KafkaMessage>(
                                topicName: configurationsKafka.ConsumerTopic,
                                groupId: configurationsKafka.GroupId,
                                configure: topicConfig =>
                                {
                                    topicConfig.AutoOffsetReset = AutoOffsetReset.Earliest;
                                    topicConfig.ConfigureConsumer<ConsumerMessageRealTime>(context);
                                });
                        });
                    });
                });
            });
        }
    }
}
