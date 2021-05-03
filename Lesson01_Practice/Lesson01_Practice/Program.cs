using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lesson01_Practice
{
    class Program
    {
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _fileName = "result.txt";
        private static readonly string _host = "https://jsonplaceholder.typicode.com/posts";
        private static readonly int _min = 1;
        private static readonly int _max = 99;

        static async Task Main(string[] args)
        {

            Console.WriteLine("Вас приветствует программа сохранения постов с сайта https://jsonplaceholder.typicode.com/posts");

            int fromPostId = GetPostIdFromUser(_min, _max, "Введите номер поста, с которого начать сохранение");
            int toPostId = GetPostIdFromUser(fromPostId, _max, "Введите номер поста, которым закончить сохранение");

            var tasks = new List<Task<Post>>();

            try
            {
                for (int i = fromPostId; i <= toPostId; i++)
                {
                    tasks.Add(GetPostById(i));
                }

                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine($"Посты с {fromPostId} по {toPostId} получены");

            var posts = tasks.Where(t => t.IsFaulted == false).Select(t => t.Result).ToList();

            try
            {
                using (StreamWriter sw = new StreamWriter(_fileName, false, System.Text.Encoding.UTF8))
                {
                    foreach (var post in posts)
                    {
                        await sw.WriteLineAsync(post.ToString());
                    }
                }

                Console.WriteLine("Посты сохранены в файл");
            }
            catch(Exception Ex)
            {
                Console.WriteLine($"Ошибка записи в файл : {Ex}");
            }

            Console.WriteLine("Для выхода нажмите любую клавишу.");

            Console.ReadKey();
        }

        static int GetPostIdFromUser(int from, int to, string message)
        {
            Console.WriteLine($"{message}, от {from} до {to}: ") ;

            int result = 0;

            bool isChoose = false;

            while (!isChoose)
            {
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    if (result >= from && result <= to)
                    {
                        isChoose = true;
                    }
                    else
                    {
                        Console.WriteLine($"Необходимо ввести число от {from} до {to}, попробуйте ещё раз.");
                    }
                }
                else
                {
                    Console.WriteLine("Необходимо ввести целое число, попробуйте ещё раз.");
                }
            }

            return result;
        }

        static async Task<Post> GetPostById(int id)
        {
            try
            {
                var response = await _client.GetAsync($"{_host}/{id}");
                var content = await response.Content.ReadAsStringAsync();
                var post = JsonConvert.DeserializeObject<Post>(content);
                return post;
            }
            catch(Exception Ex)
            {
                throw new Exception($"Ошибка при получении поста {id} : {Ex}");
            }
        }
    }
}
