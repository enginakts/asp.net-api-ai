using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Project03_RapidApi.ViewModel;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("************************************* \n");

        var client = new HttpClient(); // HttpClient oluşturuldu
        List<ApiSeriesViewModel> apiSeriesViewModels = new();

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get, // HTTP Get isteği
            RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/series/"), // API adresi
        };

        // API anahtarı güvenli bir yerden alınmalıdır.
        request.Headers.Add("x-rapidapi-key", "a8e72a8b4emsh581c48605985518p10581djsnd907f93b7fda");
        request.Headers.Add("x-rapidapi-host", "imdb-top-100-movies.p.rapidapi.com");

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            // JSON verisini nesneye dönüştür
            apiSeriesViewModels = JsonConvert.DeserializeObject<List<ApiSeriesViewModel>>(body);
            Console.WriteLine(apiSeriesViewModels);

            foreach (var series in apiSeriesViewModels)
            {
                Console.WriteLine("---------------");
                Console.WriteLine(
                    series.rank
                        + " - "
                        + series.title
                        + " - Film Puanı : "
                        + series.rating
                        + " - Yapım Yılı : "
                        + series.year
                );
            }
        }

        Console.WriteLine("\n*************************************");
        Console.ReadLine();
    }
}
