using System;
using System.Threading;

namespace WordCounter.Infrastructure
{
    /// <summary>
    /// https://blogs.msdn.microsoft.com/brada/2005/06/11/showing-progress-in-a-console-windows/
    /// ДОбавлена ассинхронность
    /// </summary>
    class ConsoleSpiner
    {
        int counter;

        Thread spinThread;

        public ConsoleSpiner()
        {
            counter = 0;
            spinThread = null;
        }

        public void Start(string message)
        {
            if (spinThread != null)
                Stop();
            spinThread = new Thread(Spin);
            spinThread.Start(message);
        }

        public void Stop()
        {
            if (spinThread != null)
            {
                spinThread.Abort();
                spinThread = null;
            }
        }

        private void Spin(object o)
        {
            try
            {
                Console.Write("{0} ", o);
                while (true)
                {
                    counter++;
                    switch (counter % 4)
                    {
                        case 0: Console.Write("/"); break;
                        case 1: Console.Write("-"); break;
                        case 2: Console.Write("\\"); break;
                        case 3: Console.Write("-"); break;
                    }
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    Thread.Sleep(500);
                }
            }
            catch (ThreadAbortException e)
            {
                Console.WriteLine();

                return;
            }
        }
    }
}
