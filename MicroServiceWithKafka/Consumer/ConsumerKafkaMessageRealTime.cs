﻿using MassTransit;
using MicroServiceWithKafka.MessageDto;

namespace MicroServiceWithKafka.Consumer
{
    public class ConsumerKafkaMessageRealTime : IConsumer<KafkaMessage>
    {
        public Task Consume(ConsumeContext<KafkaMessage> context)
        {
            Console.WriteLine($"Consummer message: {context.Message.Value}");
            return Task.CompletedTask;
        }
    }
}