// src/UGemini/Models/GeminiResponse.cs
using System.Collections.Generic;

namespace UGemini.Models;

// GeminiResponse is a class that represents the response from the Gemini API.
// It contains a list of candidates, each of which contains a content item.
// The candidates and content items are used to specify the output data from the Gemini API.
public class GeminiResponse
{
    public List<Candidate>? Candidates { get; set; }
}

// Candidate is a class that represents a single candidate item in a GeminiResponse.
// It contains a content item that specifies the output data from the Gemini API.
// The content item is used to specify the output data from the Gemini API.
public class Candidate
{
    public Content? Content { get; set; }
}