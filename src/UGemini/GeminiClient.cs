using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UGemini.Enums;
using UGemini.Models;

namespace UGemini;

public class GeminiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GeminiClient(string apiKey, HttpClient? httpClient = null)
    {
        _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        _httpClient = httpClient ?? new HttpClient();
    }

    public async Task<string?> GenerateTextAsync(string prompt, GeminiModel model)
    {
        var request = new GeminiRequest
        {
            Contents = new List<Content>
            {
                new Content
                {
                    Parts = new List<Part>
                    {
                        new Part { Text = prompt }
                    }
                }
            }
        };

        var endpoint = $"https://generativelanguage.googleapis.com/v1beta/{model.ToModelName()}:generateContent?key={_apiKey}";

        var response = await _httpClient.PostAsJsonAsync(endpoint, request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<GeminiResponse>();
        return result?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;
    }
}