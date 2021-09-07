using System;

namespace MrowrPurr {
    class Program {
        static int enterPressCount = 0;
        static void Main(string[] args) {
            var bot = new MrowrBot();
            Console.WriteLine("Running MrowrBot");
            Console.WriteLine("[PRESS <ENTER> TWICE TO EXIT]");
            bot.Connect();
            while (enterPressCount < 2) {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                    enterPressCount++;
                else
                    enterPressCount = 0;
            }
            Console.WriteLine("Existing MrowrBot...");
            bot.Disconnect();
        }
    }
}

