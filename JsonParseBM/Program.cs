using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

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

            var _dict = new Dictionary<string, SimpleObjectList>();

            using (var reader = new JsonTextReader(new StreamReader(file)))
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        var obj = serializer.Deserialize<SimpleObject>(reader);

                        SimpleObjectList _obj = null;

                        if (_dict.TryGetValue(obj.Name, out _obj))
                        {
                            _obj.List.Add(obj);
                            _obj.Total += obj.Count;
                        }
                        else
                        {
                            _obj = new SimpleObjectList(obj.Name, obj.Count);
                            _obj.List.Add(obj);
                            _dict[obj.Name] = _obj;
                        }
                    }
                }
            }

            var orderedList = _dict.Values.OrderByDescending(e => e.Total);

            var selectedList = orderedList.Take(10);

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
