using Newtonsoft.Json;
using System;

namespace OTUS_ReflectionHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            long iterationCount = 100000;
            (TimeSpan sTime, TimeSpan dTime) reslt = new();
        start:
            Console.Clear();
            Console.WriteLine($"CUSTOM {iterationCount}");
            reslt = TestPerfomanceHelper.SerializationTest(CustomSerializeTest, CustomDeserializeTest<Sample>, Sample.Get(), iterationCount);
            Console.WriteLine($"Serialization {reslt.sTime}");
            Console.WriteLine($"Deserialization {reslt.dTime}");
            Console.WriteLine($"CSV Data: {Environment.NewLine}");
            Console.WriteLine(Serializer.SerializeToCSV(Sample.Get()));
            Console.WriteLine(new string('-', Console.WindowWidth));

            
            Console.WriteLine($"NewtonsoftJSON {iterationCount}");
            reslt = TestPerfomanceHelper.SerializationTest(NewtonsoftJSONSerializeTest, NewtonsoftJSONDeserializeTest<Sample>, Sample.Get(), iterationCount);
            Console.WriteLine($"Serialization {reslt.sTime}");
            Console.WriteLine($"Deserialization {reslt.dTime}");
            Console.WriteLine(new string('-', Console.WindowWidth));

            Console.WriteLine("ConsoleWriteLine");
            TimeSpan swtime = new TimeSpan();
            bool isok;
            TestPerfomanceHelper.Measure(CWTest, ref swtime, "SOMETEXTSOMETEXTSOMETEXTSOMETEXTSOMETEXTSOMETEXTSOMETEXTSOMETEXTSOMETEXT", out isok);
            Console.WriteLine(swtime);
            Console.WriteLine(new string('-', Console.WindowWidth));

            Console.ReadLine();
            goto start;
        }

        static string CustomSerializeTest<T>(T obj)
        {
            return Serializer.SerializeToCSV(obj);
        }
        static T CustomDeserializeTest<T>(string data)
        {
            return Serializer.DeserializeFromCSV<T>(data);
        }

        static string NewtonsoftJSONSerializeTest<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        static T NewtonsoftJSONDeserializeTest<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
        static bool CWTest(string sometext)
        {
            Console.WriteLine(sometext);
            return true;
        }
    }
}
