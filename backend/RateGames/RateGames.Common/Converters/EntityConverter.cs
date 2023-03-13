using RateGames.Common.Contracts;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace RateGames.Common.Converters;

/// <summary>
/// Converter for <see cref="IEntity"/> types.
/// </summary>
public class EntityConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert) => typeof(IEntity).IsAssignableFrom(typeToConvert);
    public override JsonConverter? CreateConverter(
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var generic = typeof(InnerEntityConverter<>).MakeGenericType(typeToConvert);
        return Activator.CreateInstance(generic) as JsonConverter;
    }
    private class InnerEntityConverter<TEntity> : JsonConverter<TEntity>
        where TEntity : IEntity, new()
    {
        public override TEntity? Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out var id))
            {
                return new() { Id = id };
            }

            var defaultConverter = JsonSerializerOptions.Default.GetConverter(typeToConvert)
                as JsonConverter<TEntity> ?? throw new NotSupportedException($"Cannot find default converter for type {typeToConvert}");
            var newOptions = new JsonSerializerOptions(options);
            newOptions.Converters.Insert(0, defaultConverter);

            return JsonSerializer.Deserialize<TEntity>(ref reader, newOptions);
        }
        public override void Write(Utf8JsonWriter writer, TEntity value, JsonSerializerOptions options)
        {
            var defaultConverter = JsonSerializerOptions.Default.GetConverter(typeof(TEntity)) as JsonConverter<TEntity>
                ?? throw new NotSupportedException($"Cannot find default converter for type {typeof(TEntity)}");

            var newOptions = new JsonSerializerOptions(options);
            newOptions.Converters.Insert(0, defaultConverter);

            JsonSerializer.Serialize(writer, value, newOptions);
        }
    }
}