// src/UGemini/Models/GeminiRequest.cs
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UGemini.Models;

// GeminiRequest is a class that represents a request to the Gemini API.
// It contains a list of contents, each of which contains a list of parts.
// The contents and parts are used to specify the input data for the Gemini API.
public class GeminiRequest
{
    public List<Content> Contents { get; set; } = new();
}

// Content is a class that represents a single content item in a GeminiRequest.
// It contains a list of parts, each of which contains a text string.
// The parts are used to specify the input data for the Gemini API.
public class Content
{
    public List<Part> Parts { get; set; } = new();
}

// Part is a class that represents a single part of a content item in a GeminiRequest.
// It contains a text string that specifies the input data for the Gemini API.
// The text string is used to specify the input data for the Gemini API.
public class Part
{
    [JsonPropertyName("text")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; set; }

    [JsonPropertyName("inline_data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InlineData? InlineData { get; set; }
}

// InlineData is a class that represents inline data in a GeminiRequest.
// It contains a MIME type and base64-encoded data.
// The MIME type specifies the type of data being sent, and the base64-encoded data is the actual data.
// The inline data is used to specify the input data for the Gemini API.
// The MIME type is used to specify the type of data being sent, and the base64-encoded data is the actual data.
public class InlineData
{
    [JsonPropertyName("mime_type")]
    public string MimeType { get; set; } = "image/png";

    [JsonPropertyName("data")]
    public string Base64Data { get; set; } = string.Empty;
}