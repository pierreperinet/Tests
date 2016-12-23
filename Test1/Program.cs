using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Test1
{
    // http://www.newtonsoft.com/json/help/html/CreateJsonDynamic.htm
    class Program
    {
        static void Main()
        {  
            Console.WriteLine("--------------------------");

            var x = new JObject
                {
                    { "serverId", 100 },
                    { "iscsiIqn", "12345678" }
                };

            Console.WriteLine(x);

            dynamic y = x;

            Console.WriteLine("serverId : {0}", y.serverId);
            Console.WriteLine("iscsiIqn : {0}", y.iscsiIqn);
            Console.WriteLine("--------------------------");

            dynamic product = new JObject();
            product.ProductName = "Product1";
            product.Enabled = true;
            product.Price = 4.90m;
            product.StockCount = 9000;
            product.StockValue = 44100;
            product.Tags = new JArray("Tag1", "Tag2");
            Console.WriteLine(product);
            Console.WriteLine("--------------------------");

            Run();
            Console.WriteLine("--------------------------");

            Run2();
            Console.WriteLine("--------------------------");

            Run3();
            Console.WriteLine("--------------------------");
            Console.ReadLine();
        }

        private static void Run()
        {
            //var json = @"{""serverId"":""1"",""iscsiIqn"":""2""}";
            var h = new DynamicHostData
            {
                ServerId = "1",
                IscsiIqn = "2"
            };

            var xxx = JObject.FromObject(h);
            DynamicHostData x2 = xxx.ToObject<DynamicHostData>();
            Console.WriteLine("serverId : " + x2.ServerId);
            Console.WriteLine("iscsiIqn : " + x2.IscsiIqn);

            var json2 = h.ToJson();
            var h2 = new DynamicHostData(json2);

            Console.WriteLine("serverId : " + h.ServerId);
            Console.WriteLine("iscsiIqn : " + h.IscsiIqn);

            Console.WriteLine("serverId : " + h2.ServerId);
            Console.WriteLine("iscsiIqn : " + h2.IscsiIqn);
        }

        private static void Run2()
        {
            string json = @"{""name"":""asdf"",""teamname"":""b"",""email"":""c"",""players"":[""1"",""2""]}";
            var user = new User(json);

            Console.WriteLine("Name : " + user.Name);
            Console.WriteLine("Teamname : " + user.Teamname);
            Console.WriteLine("Email : " + user.Email);
            Console.WriteLine("Players:");

            foreach (var player in user.Players)
                Console.WriteLine(player);
        }

        private static void Run3()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var json = @"{""Name"":""Pierre"",""teamname"":""Team1"",""email"":""e""}";
            dynamic user = JsonConvert.DeserializeObject(json);
            Console.WriteLine("Name : " + user.name);
            Console.WriteLine("Name : " + user.Name);
            Console.WriteLine("Name : " + user.GetValue("name", StringComparison.InvariantCultureIgnoreCase));
            Console.WriteLine("Teamname : " + user.teamname);
            Console.WriteLine("Email : " + user.email);
            Console.WriteLine("Players:");
        }
    }

    public class DynamicHostData
    {
        private const string ServerIdAttributeName = "ServerId";
        private const string IscsiIqnAttributeName = "IscsiIqn";

        public DynamicHostData()
        {
            
        }

        public DynamicHostData(string json)
        {
            JToken dataJToken = JObject.Parse(json);
            ServerId = (string)dataJToken[ServerIdAttributeName];
            IscsiIqn = (string)dataJToken[IscsiIqnAttributeName];
        }

        public DynamicHostData(JObject jObject)
        {
            JToken dataJToken = JObject.FromObject(jObject);
            ServerId = (string)dataJToken[ServerIdAttributeName];
            IscsiIqn = (string)dataJToken[IscsiIqnAttributeName];
        }

        public string ServerId { get; set; }
        public string IscsiIqn { get; set; }

        public string ToJson2()
        {
            return "{\"serverId\":\"" + ServerId + "\", \"iscsiIqn\":\"" + IscsiIqn + "\"}";
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class User
    {
        public User(string json)
        {
            JToken jUser = JObject.Parse(json);
            Name = (string)jUser["name"];
            Teamname = (string)jUser["teamname"];
            Email = (string)jUser["email"];
            Players = jUser["players"].ToArray();
        }

        public string Name { get; set; }
        public string Teamname { get; set; }
        public string Email { get; set; }
        public Array Players { get; set; }
    }
}
