using System;
using System.Net.Http;
using System.Threading.Tasks;
namespace SharedLib
{
    public class OpenSslTest
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
                        Console.WriteLine("failed due to authentication failure - which is OK for the purpose of this test");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                if (ex.InnerException != null)
                    Console.WriteLine(ex.InnerException.ToString());
            }
        }
    }
}
