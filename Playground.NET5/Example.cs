using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Playground.NET5
{
	public class Example
	{
		public int Id { get; set; }

		public string Name { get; set; }

		[JsonConverter(typeof(ExampleTypeConvertor))]
		public ExampleTypeEnum ExampleType { get; set; }
	}

	public enum ExampleTypeEnum
	{
		[Description("This_is_One")]
		One = 1,
		[Description("This_is_Two")]
		Two = 2,
		[Description("This_is_Three")]
		Three = 3,
	}

	public class ExampleTypeConvertor : JsonConverter<ExampleTypeEnum>
	{
		public override ExampleTypeEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var description = reader.GetString();
			var values = Enum.GetValues<ExampleTypeEnum>();

			var enumType = typeof(ExampleTypeEnum);
			foreach (var value in values)
			{
				var memberInfos = enumType.GetMember(value.ToString());
				var memberInfo = memberInfos.FirstOrDefault(item => item.DeclaringType == enumType);
				var descriptionAttribute = memberInfo?.GetCustomAttribute<DescriptionAttribute>();
				if (descriptionAttribute?.Description == description)
					return value;
			}

			return default(ExampleTypeEnum);
		}

		public override void Write(Utf8JsonWriter writer, ExampleTypeEnum value, JsonSerializerOptions options)
		{
			var enumType = typeof(ExampleTypeEnum);
			var memberInfos = enumType.GetMember(value.ToString());
			var memberInfo = memberInfos.FirstOrDefault(item => item.DeclaringType == enumType);
			var descriptionAttribute = memberInfo?.GetCustomAttribute<DescriptionAttribute>();

			writer.WriteStringValue(descriptionAttribute?.Description);
		}
	}
}