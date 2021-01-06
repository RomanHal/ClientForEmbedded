using System;
using System.Threading;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Timers;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = false;
            timer.Start();
            Thread.Sleep(1001);
            timer.Start();
            Thread.Sleep(200);
            timer.Stop();
            timer.Start();

            Thread.Sleep(850);

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
           
        }

        [TestMethod]
        public void TestMethod2()
        {
            double seconds = 0.5;
            double seconds2 = 0.1;
            double seconds3 = 0.6;
            double seconds4 = 1.2;
            var xd = Convert.ToUInt32(seconds);
            var xd2 = Convert.ToUInt32(seconds2);
            var xd3 = Convert.ToUInt32(seconds3);
            var xd4 = Convert.ToUInt32(seconds4);
            Console.WriteLine(xd);
            Console.WriteLine(xd2);
            Console.WriteLine(xd3);
            Console.WriteLine(xd4);



        }
    }
}
