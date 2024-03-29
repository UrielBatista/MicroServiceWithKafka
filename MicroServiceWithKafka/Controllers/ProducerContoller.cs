using MicroServiceWithKafka.Producer;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceWithKafka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProducerContoller : ControllerBase
    {
        private readonly IKafkaMessageProducer kafkaMessageProducer;
        private readonly ILogger<ProducerContoller> _logger;

        public ProducerContoller(IKafkaMessageProducer kafkaMessageProducer, ILogger<ProducerContoller> logger)
        {
            this.kafkaMessageProducer = kafkaMessageProducer;
            _logger = logger;
        }

        [HttpPost(Name = "ProducerTopic")]
        public async Task<ActionResult> Post([FromBody] string message)
        {
            await kafkaMessageProducer.ProducerMessage(message);
            return Ok();
        }
    }
}