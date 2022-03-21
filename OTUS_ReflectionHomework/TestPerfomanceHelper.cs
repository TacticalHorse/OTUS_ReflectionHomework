using System;
using System.Diagnostics;

namespace OTUS_ReflectionHomework
{
    class TestPerfomanceHelper
    {
        public static void Measure<T, TResult>(Func<T, TResult> func, ref TimeSpan time, in T arg, out TResult output)
        {
            Stopwatch SW = new Stopwatch();
            SW.Start();

            output = func.Invoke(arg);

            SW.Stop();
            time += SW.Elapsed;
        }

        public static (TimeSpan sTime, TimeSpan dTime) SerializationTest<T> (Func<T,string> sFunc, Func<string,T> dFunc, T obj, long execCount)
        {
            TimeSpan stime = new TimeSpan();
            TimeSpan dtime = new TimeSpan();
            

            for (int i = 0; i < execCount; i++)
            {
                string data;
                T DeserializeObj;
                Measure(sFunc, ref stime, in obj, out data);
                Measure(dFunc, ref dtime, in data, out DeserializeObj);
            }

            return (stime, dtime);
        }
    }
}
