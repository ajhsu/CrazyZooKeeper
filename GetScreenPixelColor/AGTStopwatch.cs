using System;
using System.Diagnostics;

namespace com.artgital.ajhsu
{
    public static class AGTStopwatch
    {
        private static Stopwatch watch = new Stopwatch();
        private static long temperaryStorage = 0;
        private static long result = 0;

        public static void QuickStart()
        {
            watch.Start();
            temperaryStorage = watch.ElapsedMilliseconds;
        }

        public static long QuickStop()
        {
            watch.Stop();
            result = watch.ElapsedMilliseconds - temperaryStorage;
            Console.WriteLine("AGTStopwatch::Time Elapsed: " + result + " ms.");
            return result;
        }

        public static void Dispose()
        {
            watch = null;
        }
    }
}
