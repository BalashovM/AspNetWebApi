using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lesson01_Practice
{
    class Program
    {
        private static readonly HttpClient _client = new HttpClient();
        static async Task Main(string[] args)
        {

            var result = await GetPosts();

            Console.WriteLine("Hello World!");
        }

        static async Task<List<Post>> GetPosts()
        {
            var response = await _client.GetAsync("https://jsonplaceholder.typicode.com/posts");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error");
            }

            var content = await response.Content.ReadAsStringAsync();

            return new List<Post>();
        }


    }
}
