#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log, IAsyncCollector<string> outputSbMsg)
{
    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody);

    string ruleName = data?.rule?.displayName.ToString() ?? "";
    switch (ruleName)
    {
        case "Low light level":
            await outputSbMsg.AddAsync(JsonConvert.SerializeObject(new { light = "ON" }));
            break;
        case "High light level":
            await outputSbMsg.AddAsync(JsonConvert.SerializeObject(new { light = "OFF" }));
            break;
        case "Low temperature":
            await outputSbMsg.AddAsync(JsonConvert.SerializeObject(new { temperature = "ON" }));
            break;
        case "High temperature":
            await outputSbMsg.AddAsync(JsonConvert.SerializeObject(new { temperature = "OFF" }));
            break;
        default:
            break;
    }

    return new OkResult();
}