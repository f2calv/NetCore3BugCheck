using System;
using System.Threading.Tasks;
namespace ConsoleApp22
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var obj = new SharedLib.OpenSslTest();
            await obj.TryConnect();
            Console.WriteLine("hit any key to exit...");
            Console.ReadKey();
        }
    }
}