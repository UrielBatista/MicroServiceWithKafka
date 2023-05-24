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
                    rider.AddConsumer<ConsumerKafkaMessageRealTime>();

                    rider.AddProducer<KafkaMessage>(configurationsKafka.ConsumerTopic);
                    rider.AddProducer<KafkaMessageReceivePerson>("people-topic");

                    rider.UsingKafka((context, k) =>
                    {
                        k.Host(server: configurationsKafka.ServerHost, configure: hostCfg =>
                        {
                            hostCfg.UseSsl(sslCfg =>
                            {
                                sslCfg.EnableCertificateVerification = false;
                            });
                            k.TopicEndpoint<KafkaMessageReceivePerson>(
                                topicName: "people-topic",
                                groupId: configurationsKafka.GroupId,
                                configure: topicConfig =>
                                {
                                    topicConfig.AutoOffsetReset = AutoOffsetReset.Earliest;
                                    topicConfig.ConfigureConsumer<ConsumerMessageRealTime>(context);
                                });
                            k.TopicEndpoint<KafkaMessage>(
                                topicName: configurationsKafka.ConsumerTopic,
                                groupId: configurationsKafka.GroupId,
                                configure: topicConfig =>
                                {
                                    topicConfig.AutoOffsetReset = AutoOffsetReset.Earliest;
                                    topicConfig.ConfigureConsumer<ConsumerKafkaMessageRealTime>(context);
                                });
                        });
                    });
                });
            });
        }
    }
}
