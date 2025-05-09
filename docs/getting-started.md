# Getting Started

## Installation

```bash
dotnet add package UGemini
```

## Example

```csharp
var client = new GeminiClient("YOUR_API_KEY");
var result = await client.GenerateTextAsync("Explain how AI works", GeminiModel.Gemini15Pro);
Console.WriteLine(result);
```

## Where to Get API Key

Visit [Google AI Studio](https://aistudio.google.com) and go to the "API Keys" section.