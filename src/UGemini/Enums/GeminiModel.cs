// src/UGemini/Enums/GeminiModel.cs
using System;

namespace UGemini.Enums;

// GeminiModel is an enum that represents the different models of the Gemini device.
// It is used to specify which model to use when creating a new Gemini device instance.
// The models are:
// - Gemini20Flash: The Gemini 2.0 Flash model.
// - Gemini20FlashLite: The Gemini 2.0 Flash Lite model.
// - Gemini15Pro: The Gemini 1.5 Pro model.
// - Gemini15Flash: The Gemini 1.5 Flash model.
// - Gemini15Flash8B: The Gemini 1.5 Flash 8B model.
public enum GeminiModel
{
    Gemini20Flash,
    Gemini20FlashLite,
    Gemini15Pro,
    Gemini15Flash,
    Gemini15Flash8B
}

// This extension method converts the GeminiModel enum to a string representation.
// The string representation is used to specify the model when creating a new Gemini device instance.
// The string representation is in the format "models/{model-name}".
// For example, GeminiModel.Gemini20Flash will be converted to "models/gemini-2.0-flash".
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