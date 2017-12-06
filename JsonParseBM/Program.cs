using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParseBM
{
    class Program
    {
        static void Main(string[] args)
        {

            //var deserialiser = new SimpleObjectDeserialiser();

            var file = Path.Combine(Directory.GetCurrentDirectory(), "Test.json");

            //using (var reader = new JsonTextReader(new StreamReader(file)))
            //{
            //    while (reader.Read())
            //    {
            //        if (reader.TokenType == JsonToken.StartObject)
            //        {
            //            var obj = deserialiser.Deserialise(reader);
            //        }
            //    }
            //}

            TestJsonNet(file);
            TestCustom(file);

            Console.ReadKey();
        }

        private static void TestJsonNet(string file)
        {
            Console.WriteLine("TestJsonNet");
            var watch = new Stopwatch();

            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            JsonSerializer serializer = new JsonSerializer();

            watch.Start();
            

            using (var reader = new JsonTextReader(new StreamReader(file)))
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        var obj = serializer.Deserialize<SimpleObject>(reader);
                    }
                }
            }

            watch.Stop();

            Console.WriteLine(" Time Elapsed {0} ms", watch.Elapsed.TotalMilliseconds);
        } 

        private static void TestCustom(string file)
        {
            Console.WriteLine("TestCustom");
            var watch = new Stopwatch();

            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var deserialiser = new SimpleObjectDeserialiser();

            watch.Start();


            using (var reader = new JsonTextReader(new StreamReader(file)))
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        var obj = deserialiser.Deserialise(reader);
                    }
                }
            }

            watch.Stop();

            Console.WriteLine(" Time Elapsed {0} ms", watch.Elapsed.TotalMilliseconds);
        }
    }
}
