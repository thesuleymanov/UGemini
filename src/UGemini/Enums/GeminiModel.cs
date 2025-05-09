using System;

namespace UGemini.Enums;

public enum GeminiModel
{
    Gemini20Flash,
    Gemini20FlashLite,
    Gemini15Pro,
    Gemini15Flash,
    Gemini15Flash8B
}

public static class GeminiModelExtensions
{
    public static string ToModelName(this GeminiModel model) => model switch
    {
        GeminiModel.Gemini20Flash      => "models/gemini-2.0-flash",
        GeminiModel.Gemini20FlashLite  => "models/gemini-2.0-flash-lite",
        GeminiModel.Gemini15Pro        => "models/gemini-1.5-pro-latest",
        GeminiModel.Gemini15Flash      => "models/gemini-1.5-flash-latest",
        GeminiModel.Gemini15Flash8B    => "models/gemini-1.5-flash-8b-latest",
        _ => throw new ArgumentOutOfRangeException()
    };
}