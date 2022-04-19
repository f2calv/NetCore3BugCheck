var obj = new SharedLib.OpenSslTest();
await obj.TryConnect();
Console.WriteLine("hit any key to exit...");
Console.ReadKey();
