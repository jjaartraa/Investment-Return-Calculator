using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment_Return_Calculator
{
    class Program
    {
        public static double bilance = 0;
        public static float interest = 0;
        public static float monthlyInvesment = 0;
        public static int timePeriod = 0;
        public static float totalInterest = 0;
        public static float totalInvestment = 0;
        public static string currency = "";

        static void Main(string[] args)
        {
            bool repeat = false;
            do
            {
                Run();
                Console.WriteLine("Do you want to calculate again?" + Environment.NewLine + "Type Y / N: ");
                if (Console.ReadLine().ToString().ToUpper() == "Y")
                {
                    repeat = true;
                    bilance = 0;
                    interest = 0;
                    monthlyInvesment = 0;
                    timePeriod = 0;
                    totalInterest = 0;
                    totalInvestment = 0;
                }
                else if (Console.ReadLine().ToString().ToUpper() == "N")
                {
                    repeat = false;
                }
                else
                {
                    Console.WriteLine("Since you're not even able to answer simple YES / NO question, I don't think you deserve another try :)");
                    repeat = false;
                }
            } while (repeat);
        }

        private static void Run() {
            bool failed;
            do
            {
                Console.Clear();
                Refresh();
                Console.Write("Welcome to Investment Return Calculator" + Environment.NewLine + "Please input currency code (CZK, EUR, USD, RUB, ...):");
                failed = false;
                try
                {
                    currency = Console.ReadLine();

                    if (currency.Length != 3)
                    {
                        failed = true;
                    }
                }
                catch (Exception)
                {
                    failed = true;
                }
            } while (failed);
            do
            {
                Console.Clear();
                Refresh();
                Console.Write("Please input starting account bilance:");
                failed = false;
                try
                {
                    bilance = double.Parse(Console.ReadLine());
                    totalInvestment = (float)bilance;
                }
                catch (Exception)
                {
                    failed = true;
                }
            } while (failed);

            do
            {
                Console.Clear();
                Refresh();
                Console.Write("Please input interest rate in %: ");
                failed = false;
                try
                {
                    interest = float.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    failed = true;
                }
            } while (failed);

            do
            {
                Console.Clear();
                Refresh();
                Console.Write("Please input monthly investments: ");
                failed = false;
                try
                {
                    monthlyInvesment = float.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    failed = true;
                }
            } while (failed);

            do
            {
                Console.Clear();
                Refresh();
                Console.Write("Please input time period in months: ");
                failed = false;
                try
                {
                    timePeriod = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    failed = true;
                }
            } while (failed);

            string monthreport = "";
            for (int i = 0; i < timePeriod; i++)
            {
                double startBilance = bilance;
                totalInterest += (float)AddInterest();
                totalInvestment += monthlyInvesment;
                bilance += AddInterest() + monthlyInvesment;
                monthreport += MonthlyReport(i, startBilance);
                Refresh();
                //Console.WriteLine(monthreport);
                Console.WriteLine(MonthlyReport(i, startBilance));
                System.Threading.Thread.Sleep(100);
            }
        }
        public static void Refresh() 
        {
            string info = Environment.NewLine 
                + '\t' + " | " + "Bilance: " + bilance.ToString("###,###,###,###,###,##0.00 ") + currency + Environment.NewLine
                + '\t' + " | " + "Interest: " + interest.ToString("#,##0.0") + "% " + Environment.NewLine
                + '\t' + " | " + "Monthly Investments: " + monthlyInvesment.ToString("###,###,###,###,###,##0 ") + currency + Environment.NewLine
                + '\t' + " | " + "Total Interest: " + totalInterest.ToString("###,###,###,###,###,##0 ") + currency + Environment.NewLine
                + '\t' + " | " + "Total Investment: " + totalInvestment.ToString("###,###,###,###,###,##0 ") + currency + Environment.NewLine
                + '\t' + " | " + "Time span: " + timePeriod + " months." + Environment.NewLine;
            Console.SetCursorPosition(0,0);
            Console.WriteLine(info);
        }

        public static double AddInterest()
        {
            return ((bilance / 100) * (interest / 12));
        }

        public static string MonthlyReport(int month, double start)
        {
            string report = "";
            report += Environment.NewLine + "Month: " + (month + 1) + "."
                + Environment.NewLine + "Starting month bilance: " + start.ToString("###,###,###,###,###,##0.00") + " Kč"
                + Environment.NewLine + "Monthly nterest: " + AddInterest().ToString("###,###,###,###,###,##0.00") + " Kč"
                + Environment.NewLine + "End of the month bilance: " + bilance.ToString("###,###,###,###,###,##0.00") + " Kč" + Environment.NewLine;

            return report;
        }
    }
}
