using System.Diagnostics;

namespace Common
{
    public static class MyStopwatch
    {
        public static long ElapsedMilliseconds(string stopwatchName = "DEBUG")
        {
            var sw = MyContainer<Stopwatch>.GetInstance(stopwatchName);
            sw.Stop();
            var result = sw.ElapsedMilliseconds;
            sw.Restart();
            return result;
        }

        public static long ElapsedTicks(string stopwatchName = "DEBUG")
        {
            var sw = MyContainer<Stopwatch>.GetInstance(stopwatchName);
            sw.Stop();
            var result = sw.ElapsedTicks;
            sw.Restart();
            return result;
        }

        public static System.TimeSpan Elapsed(string stopwatchName = "DEBUG")
        {
            var sw = MyContainer<Stopwatch>.GetInstance(stopwatchName);
            sw.Stop();
            var result = sw.Elapsed;
            sw.Restart();
            return result;
        }
    }
}
