using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServerSentEvents
{
    public class ServerSentEventsService : IDisposable
    {
        private readonly Uri _uri;
        private readonly Action<MessageEvent> _messageEventHandler;
        private bool _running = true;
        private const int ReconnectDelay = 500;  // milliseconds
        private int _reconnectDelay = ReconnectDelay;

        public ServerSentEventsService(Uri uri, Action<MessageEvent> messageEventHandler)
        {
            _uri = uri;
            _messageEventHandler = messageEventHandler;
        }

        public void Start()
        {
            _running = true;
            var task = Task.Factory.StartNew(() =>
            {
                Task.Factory.StartNew(async () =>
                {
                    await ReadServerSentEventsAsync();
                });
            });
            task.Wait();
        }

        public void Dispose()
        {
            _running = false;
        }

        private async Task ReadServerSentEventsAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/event-stream"));

                while (_running)
                {
                    try
                    {
                        using (var httpResponseMessage = await httpClient.GetAsync(_uri, HttpCompletionOption.ResponseHeadersRead))
                        using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync())
                            await ReadStreamAsync(stream);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unexpected exception caught while requesting GET on uri '{_uri.AbsoluteUri}");
                        Console.WriteLine(ex);
                    }
                    finally
                    {
                        await Task.Delay(_reconnectDelay);
                    }
                }
            }
        }

        public async Task ReadStreamAsync(Stream stream)
        {
            var streamReader = new StreamReader(stream);
            string line;
            MessageEvent messageEvent = null;

            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                line = line.Trim();

                if (string.IsNullOrEmpty(line))
                {
                    if (messageEvent != null)
                    {
                        _messageEventHandler(messageEvent);
                        messageEvent = null;
                    }
                    continue;
                }

                var indexOfColumn = line.IndexOf(":", StringComparison.Ordinal);

                if (indexOfColumn == 0)
                {
                    // it's a comment
                    continue;
                }

                string fieldName;
                string fieldValue;

                if (indexOfColumn == -1)
                {
                    fieldName = string.Empty;
                    fieldValue = line;
                }
                else
                {
                    fieldName = line.Substring(0, indexOfColumn).Trim();
                    fieldValue = line.Substring(indexOfColumn + 1).Trim();
                }

                switch (fieldName)
                {
                    case "event":
                        if (messageEvent == null)
                        {
                            messageEvent = new MessageEvent();
                        }
                        messageEvent.Type = fieldValue;
                        break;
                    case "data":
                        if (messageEvent == null)
                        {
                            messageEvent = new MessageEvent();
                        }
                        messageEvent.Data = string.IsNullOrEmpty(messageEvent.Data) ? fieldValue : $"{messageEvent.Data}\n{fieldValue}";
                        break;
                    case "id":
                        if (messageEvent == null)
                        {
                            messageEvent = new MessageEvent();
                        }
                        messageEvent.Id = fieldValue;
                        break;
                    case "retry":
                        if (messageEvent == null)
                        {
                            messageEvent = new MessageEvent();
                        }
                        int retry;
                        if (int.TryParse(fieldValue, out retry))
                        {
                            messageEvent.Retry = retry;
                        }
                        break;
                        // Default: ignore
                }
            }
        }

        public void ReadStream(Stream stream)
        {
            var streamReader = new StreamReader(stream);
            string line;
            MessageEvent messageEvent = null;

            while ((line = streamReader.ReadLine()) != null)
            {
                line = line.Trim();

                if (string.IsNullOrEmpty(line))
                {
                    if (messageEvent != null)
                    {
                        DispatchMessageEvent(messageEvent);
                        messageEvent = null;
                    }
                    continue;
                }

                if (line.StartsWith(":"))
                {
                    continue;
                }

                string fieldName;
                string fieldValue;

                if (line.Contains(":"))
                {
                    fieldName = line.Substring(0, line.IndexOf(':')).Trim();
                    fieldValue = line.Substring(line.IndexOf(':') + 1).Trim();
                }
                else
                {
                    fieldName = string.Empty;
                    fieldValue = line;
                }

                switch (fieldName)
                {
                    case "event":
                        if (messageEvent == null)
                        {
                            messageEvent = new MessageEvent();
                        }
                        messageEvent.Type = fieldValue;
                        break;
                    case "data":
                        if (messageEvent == null)
                        {
                            messageEvent = new MessageEvent();
                        }
                        messageEvent.Data = string.IsNullOrEmpty(messageEvent.Data) ? fieldValue : $"{messageEvent.Data}\n{fieldValue}";
                        break;
                    case "id":
                        if (messageEvent == null)
                        {
                            messageEvent = new MessageEvent();
                        }
                        messageEvent.Id = fieldValue;
                        break;
                    case "retry":
                        if (messageEvent == null)
                        {
                            messageEvent = new MessageEvent();
                        }
                        int retry;
                        if (int.TryParse(fieldValue, out retry))
                        {
                            messageEvent.Retry = retry;
                        }
                        break;
                }
            }
        }

        private void DispatchMessageEvent(MessageEvent messageEvent)
        {
            _reconnectDelay = messageEvent.Retry == 0 ? ReconnectDelay : messageEvent.Retry;
            _messageEventHandler(messageEvent);
        }

        public class MessageEvent
        {
            public string Type { get; set; } = string.Empty;
            public string Data { get; set; } = string.Empty;
            public string Id { get; set; } = string.Empty;
            public int Retry { get; set; }
        }
    }
}
