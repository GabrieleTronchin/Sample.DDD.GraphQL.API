using System.Text.Json;
using System.Text.Json.Serialization;

namespace EventSource.Application.Account
{
    //https://devblogs.microsoft.com/dotnet/system-text-json-in-dotnet-8/

    [JsonSourceGenerationOptions(
    JsonSerializerDefaults.Web,
    AllowTrailingCommas = true,
    UseStringEnumConverter = true,
    DefaultBufferSize = 10)]
    [JsonSerializable(typeof(CreateOrderRequest))]
    public partial class AppJsonSerializerContext : JsonSerializerContext
    {

    }

}
