using System;
using System.Threading.Tasks;
namespace ConsoleApp30
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var obj = new SharedLib.Class1();
            await obj.TryConnect();
            Console.WriteLine("hit any key to exit...");
            Console.ReadKey();
        }
    }
}