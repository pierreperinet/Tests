using System;
using System.IO;
using System.Threading.Tasks;

namespace AsyncTaskAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<int> task = ReadFileAsync(@".\AsyncTaskAwait.exe");

            //do stuff while file read is taking place asynchronously.
            var dir = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current directory is {dir}");

            var bytes = ReadFileAsync(@".\AsyncTaskAwait.exe").Result; // synchronous call
            Console.WriteLine($@"Read {bytes} bytes successfully");

            Console.ReadLine();
        }
        private static async Task<int> ReadFileAsync(string filePath)
        {
            var bytesRead = 0;
            try
            {
                using (var fileStream = File.OpenRead(filePath))
                {
                    var readBuffer = new Byte[fileStream.Length];
                    bytesRead = await fileStream.ReadAsync(readBuffer, 0, (int)fileStream.Length).ConfigureAwait(false);
                    Console.WriteLine("Read {0} bytes successfully from file {1}", bytesRead, filePath);
                    //await Task.Delay(5000);
                    return bytesRead;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception occurred while reading file {0}.", filePath);
                return bytesRead;
            }
        }
    }
}
