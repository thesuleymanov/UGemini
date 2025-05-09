using UGemini;
using UGemini.Enums;

var apiKey = "YOUR_API_KEY_HERE";

var client = new GeminiClient(apiKey);

var response = await client.GenerateTextAsync("Explain how AI works", GeminiModel.Gemini20Flash);

Console.WriteLine("Gemini says:");
Console.WriteLine(response);