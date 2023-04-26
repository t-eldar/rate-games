using System.Text.Json.Serialization;
using System.Text.Json;

using RateGames.Common.Contracts;
using RateGames.Common.Utils;

namespace RateGames.Common.Converters;

public class IdOrConverterFactory : JsonConverterFactory
{
	public override bool CanConvert(Type typeToConvert) =>
		typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(IdOr<>);

	public override JsonConverter? CreateConverter(
		Type typeToConvert,
		JsonSerializerOptions options
	)
	{
		var type = typeToConvert.GetGenericArguments()[0];
		var generic = typeof(IdOrConverter<>).MakeGenericType(type);
		var instance = Activator.CreateInstance(generic) as JsonConverter;

		return instance;
	}

	private class IdOrConverter<TEntity> : JsonConverter<IdOr<TEntity>>
		where TEntity : IEntity, new()
	{
		public override IdOr<TEntity>? Read(
			ref Utf8JsonReader reader,
			Type typeToConvert,
			JsonSerializerOptions options
		)
		{
			if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out var id))
			{
				return new IdOr<TEntity>(id);
			}
			var result = JsonSerializer.Deserialize<TEntity>(ref reader, options);

			return new IdOr<TEntity>(result!);
		}

		public override void Write(
			Utf8JsonWriter writer,
			IdOr<TEntity> value,
			JsonSerializerOptions options
		)
		{
			if (value.Value is null)
			{
				JsonSerializer.Serialize(writer, value.Id, options);
				return;
			}
			JsonSerializer.Serialize(writer, value.Value, options);
		}
	}
}
