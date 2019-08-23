using System;
using System.Net.Http;
using System.Threading.Tasks;
namespace SharedLib
{
    public class Class1
    {
        public async Task TryConnect()
        {
            var _client = new HttpClient
            {
                BaseAddress = new Uri("https://api-fxpractice.oanda.com/v3/"),
                Timeout = TimeSpan.FromSeconds(5)
            };
            try
            {
                using (var response = await _client.GetAsync("accounts").ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                        Console.WriteLine("complete success!");
                    else
                        Console.WriteLine("failed ...due to authentication failure which is OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                if (ex.InnerException != null)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(ex.InnerException.ToString());
                }
            }
        }
    }
}
