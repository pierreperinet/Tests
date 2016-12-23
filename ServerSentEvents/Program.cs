using System;
using System.IO;

namespace ServerSentEvents
{
    [System.Runtime.InteropServices.Guid("6F6A1353-A423-4C22-8E3D-A189A63010AA")]
    class Program
    {
        private const string Hostname = "localhost";
        private const string Events =
            "event: alert\n" +
            "id: a1\n" +
            "data: alert data here\n" +
            "data: more alert data here\n" +
            "retry: 100\n" +
            "\n" +
            ":bla : bla\n" +
            "\n" +
            "\n" +
            "\n" +
            "event: alert\n" +
            "id: a2\n" +
            "data: alert data here\n" +
            "retry: 200\n" +
            "\n" +
            "event: alert\n" +
            "id: a3\n" +
            "data: alert3 data here\n" +
            "retry: 300\n" +
            "\n";

        static void Main(string[] args)
        {
            var uri = new UriBuilder(Uri.UriSchemeHttps, Hostname, 8081, "svmc/v1/internal/health/notify", "?severity=CRITICAL&types=ALARMS,CLUSTER&frequency=30&groups=G1,G2,G3").Uri;
            var serverSentEventsService = new ServerSentEventsService(uri, MessageEventHandler);
            //serverSentEventsService.Start();
            using (var stream = GenerateStreamFromString(Events))
            {
                //serverSentEventsService.ReadStream(stream);
                serverSentEventsService.ReadStreamAsync(stream).Wait();
            }
            Console.ReadLine();
        }

        private static void MessageEventHandler(ServerSentEventsService.MessageEvent messageEvent)
        {
            if (messageEvent == null)
                return;
            Console.WriteLine($"\nMessageEvent\n  Type: {messageEvent.Type}\n  Data: {messageEvent.Data}\n  Id: {messageEvent.Id}\n  Retry: {messageEvent.Retry}");
        }

        private static Stream GenerateStreamFromString(string s)
        {
            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream);
            streamWriter.Write(s);
            streamWriter.Flush();
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
