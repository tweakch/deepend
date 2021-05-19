using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Deepend.Converters
{
    public class DirectoryInfoConverter : JsonConverter<DirectoryInfo>
    {
        private string _filesToInclude;

        public DirectoryInfoConverter(string filesToInclude)
        {
            _filesToInclude = filesToInclude;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DirectoryInfo);
        }

        public override DirectoryInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return null; //Doesn't handle deserializing
        }

        public override void Write(Utf8JsonWriter writer, DirectoryInfo value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString(nameof(value.FullName), value.FullName);
            writer.WriteBoolean(nameof(value.Exists), value.Exists);

            if (!value.Exists) return;

            writer.WriteString(nameof(value.CreationTime), value.CreationTime);

            writer.WriteStartArray("files");
            foreach (var file in value.EnumerateFiles())
            {
                writer.WriteStringValue(file.Name);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("directories");
            foreach (var dir in value.EnumerateDirectories(_filesToInclude))
            {
                writer.WriteStringValue(dir.Name);
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}
