using System.Text.Json;
using Confluent.Kafka;
using journey_service.Events;

namespace journey_service.Services;

public class KafkaProducerService
{
    private readonly IProducer<string, string> _producer;

    public KafkaProducerService()
    {
        var config = new ProducerConfig { BootstrapServers = "localhost:9092" }; // TODO: config
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task SendMessageAsync<T>(string topic, string key, T kafkaEvent) where T : BaseEvent
    {
        var value = JsonSerializer.Serialize(kafkaEvent);
        var message = new Message<string, string> { Key = key, Value = value };
        var result = await _producer.ProduceAsync(topic, message);
        Console.WriteLine($"Sent: {result.Message.Value} to {result.TopicPartitionOffset}");
    }
}
