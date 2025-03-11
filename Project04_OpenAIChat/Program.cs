﻿// See https://aka.ms/new-console-template for more information
using System.Net.Http;
using System.Text;
using System.Text.Json;

Console.WriteLine("Hello, World!");

static async Task Main(string[] args)
{
    var apiKey = "buraya key gelecek";
    Console.WriteLine("Lütfen sorunuzu yazınız (örnek : 'Bugün hava kaç derece') : ");

    var prompt = Console.ReadLine(); // kullanıcıdan veriyi al

    using var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

    var requestbody = new
    {
        model = "gpt-3.5-turbo",
        message = new[]
        {
            new { role = "system", content = "You are a helpful assistan." },
            new { role = "user", content = prompt! },
        },
        max_tokens = 10000, // gelecek yanıt uzunluğu 
    };

    var json = JsonSerializer.Serialize(requestbody);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    try
    {
        var response = await httpClient.PostAsync(
            "https://api.openai.com/v1/chat/completions",
            content
        );
        var responseString = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var result = JsonSerializer.Deserialize<JsonElement>(responseString);
            var answer = result
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            Console.WriteLine("Open AI Cevabı : ");
            Console.WriteLine(answer);
        }
        else
        {
            Console.WriteLine($"Bir hata oluştu : {response.StatusCode}");
            Console.WriteLine(responseString);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Bir hata oluştu : {ex.Message}");
    }
}

