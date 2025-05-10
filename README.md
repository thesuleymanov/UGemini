![Header](https://github.com/thesuleymanov/UGemini/blob/main/images/header.jpg?raw=true)

**UGemini** is a modern .NET client for Google's Gemini API (Generative Language API). It allows easy interaction with multiple Gemini models like Gemini 2.0 Flash, Gemini 1.5 Pro, and more.

![.NET](https://img.shields.io/badge/.NET-Standard%202.0-blue)
![License](https://img.shields.io/github/license/thesuleymanov/UGemini)

## 🚀 Quick Start

```bash
dotnet add package UGemini
```

```csharp
var client = new GeminiClient("your_api_key");
var result = await client.GenerateTextAsync("Hello, Gemini!", GeminiModel.Gemini20Flash);
Console.WriteLine(result);
```

## 📦 Supported Models

* Gemini 2.0 Flash

* Gemini 2.0 Flash Lite

* Gemini 1.5 Pro

* Gemini 1.5 Flash

* Gemini 1.5 Flash-8B

## 📚 Documentation

* [Getting Started](https://github.com/thesuleymanov/UGemini/blob/main/docs/getting-started.md)

* [Model Reference](https://github.com/thesuleymanov/UGemini/blob/main/docs/models.md)

* [Roadmap](https://github.com/thesuleymanov/UGemini/blob/main/docs/roadmap.md)

## 🤝 Contributing

See [CONTRIBUTING.md](https://github.com/thesuleymanov/UGemini/blob/main/CONTRIBUTING.md) to get involved.

## 📜 License

MIT — see [LICENSE](https://github.com/thesuleymanov/UGemini/blob/main/LICENSE)