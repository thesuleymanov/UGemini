// MIT License

// Copyright (c) 2025 Elvin Suleymanov

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

// src/UGemini/GeminiClient.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UGemini.Enums;
using UGemini.Models;

namespace UGemini;

// GeminiClient is a class that provides methods to interact with the Gemini API.
// It allows you to generate text using the Gemini model.
// It requires an API key to authenticate requests.
// The API key can be obtained from the Gemini API documentation.
public class GeminiClient
{
    // The HttpClient instance used to send requests to the Gemini API.
    // It is used to send HTTP requests and receive HTTP responses.
    private readonly HttpClient _httpClient;
    // The API key used to authenticate requests to the Gemini API.
    // It is required to access the Gemini API.
    private readonly string _apiKey;

    // Initializes a new instance of the GeminiClient class with the specified API key and HttpClient.
    // If no HttpClient is provided, a new instance will be created.
    public GeminiClient(string apiKey, HttpClient? httpClient = null)
    {
        _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        _httpClient = httpClient ?? new HttpClient();
    }

    // Generates text using the specified prompt and Gemini model.
    // The prompt is the input text that will be used to generate the output text.
    // The model specifies which Gemini model to use for text generation.
    // The method sends a request to the Gemini API and returns the generated text.
    // If the request fails, it will throw an exception.
    // The method is asynchronous and returns a Task<string?>.
    // The string returned is the generated text.
    // If no text is generated, it will return null.
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

    // Generates text using the specified prompt and image file.
    // The prompt is the input text that will be used to generate the output text.
    // The image file is the input image that will be used to generate the output text.
    // The model specifies which Gemini model to use for text generation.
    // The method sends a request to the Gemini API and returns the generated text.
    // If the request fails, it will throw an exception.
    // The method is asynchronous and returns a Task<string?>.
    // The string returned is the generated text.
    // If no text is generated, it will return null.
    // The image file must be a valid image file and must exist on the file system.
    // The method reads the image file, converts it to a base64 string, and includes it in the request.
    // The image file must be in one of the supported formats: .png, .jpg, .jpeg, .webp.
    // The MIME type of the image file is determined based on its extension.
    // The supported MIME types are: image/png, image/jpeg, image/webp.
    public async Task<string?> GenerateTextWithImageAsync(string prompt, string imagePath, GeminiModel model)
    {
        if (!File.Exists(imagePath))
            throw new FileNotFoundException($"Image file not found: {imagePath}");

        var imageBytes = await Task.Run(() => File.ReadAllBytes(imagePath));
        var base64Image = Convert.ToBase64String(imageBytes);

        var request = new GeminiRequest
        {
            Contents = new List<Content>
            {
                new Content
                {
                    Parts = new List<Part>
                    {
                        new Part { Text = prompt },
                        new Part
                        {
                            InlineData = new InlineData
                            {
                                MimeType = GetMimeType(imagePath),
                                Base64Data = base64Image
                            }
                        }
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

    // Generates text using the specified prompt and base64-encoded image.
    // The prompt is the input text that will be used to generate the output text.
    // The base64Image is the base64-encoded image string that will be used as visual input.
    // The mimeType specifies the type of image (e.g., "image/png").
    // The model specifies which Gemini model to use for text generation.
    // The method sends a request to the Gemini API and returns the generated text.
    // If the request fails, it will throw an exception.
    // The method is asynchronous and returns a Task<string?>.
    // The string returned is the generated text.
    // If no text is generated, it will return null.
    public async Task<string?> GenerateTextWithImageAsync(string prompt, string base64Image, string mimeType, GeminiModel model)
    {
        if (string.IsNullOrWhiteSpace(base64Image))
            throw new ArgumentException("Base64 image data cannot be null or empty.", nameof(base64Image));

        var request = new GeminiRequest
        {
            Contents = new List<Content>
            {
                new Content
                {
                    Parts = new List<Part>
                    {
                        new Part { Text = prompt },
                        new Part
                        {
                            InlineData = new InlineData
                            {
                                MimeType = mimeType,
                                Base64Data = base64Image
                            }
                        }
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

    // Gets the MIME type of the specified file based on its extension.
    // The MIME type is used to specify the type of data being sent in the request.
    // The method throws a NotSupportedException if the file extension is not supported.
    // The supported file extensions are: .png, .jpg, .jpeg, .webp.
    private static string GetMimeType(string filePath)
    {
        var ext = Path.GetExtension(filePath).ToLowerInvariant();
        return ext switch
        {
            ".png" => "image/png",
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".webp" => "image/webp",
            _ => throw new NotSupportedException($"Unsupported image format: {ext}")
        };
    }
}