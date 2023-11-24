using oldenburgPaLib;
using System.Runtime.CompilerServices;

// HAU: ℹ️ use better namespaces structure like OrderStatistics.App 
namespace oldenburgPaApp
{
    // HAU: ℹ️ why is program partial?
    //         partial keyword is intended to split code of one class into two files
    public partial class Program
    {
        static void Main(string[] args)
        {
            Processor proc = new Processor();

            // HAU: ℹ️ was not specified - first argument was intended to be --order or --customer
            var filename = args[0];

            // HAU: ℹ️ not used variable "header" - Skip(2)
            var header = File.ReadAllLines(filename).Take(2);
            var data = File.ReadAllLines(filename);

            var orders = Processor.CheckType(data);

            // HAU: ℹ️ use better naming - Class1 is not good
            var summarizedRevenue = Class1.GetTotalPerOrder(orders);
            foreach (var order in summarizedRevenue) 
            {
                Console.WriteLine(order.ToString());
            }

        }
    }
}