using e6Logic;

namespace e6App
{
    public class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllLines(args[0]);

            var output = Enumerable.Empty<Picture>();



            if (args[1] == "monthly")
            {

                output = e6Logic.e6Logic.Month(data);

                foreach (var dataSet in output)
                {
                    Console.WriteLine(dataSet.Pic);
                    foreach (var month in dataSet.Times)
                    {
                        Console.WriteLine("\t" + month.STime + ": " + " " + month.Count);
                    }
                    Console.WriteLine("Total Amount: " + dataSet.TotalCount);
                    Console.WriteLine();
                }
            }
            else if (args[1] == "hourly")
            {

                output = e6Logic.e6Logic.Hourly(data);

                foreach (var dataSet in output)
                {
                    Console.WriteLine(dataSet.Pic);
                    foreach (var month in dataSet.Times)
                    {
                        Console.WriteLine("\t" + month.STime + " => " + " " + month.Count + "%");
                    }
                    Console.WriteLine("Total Amount: " + dataSet.TotalCount);
                    Console.WriteLine();
                }
            }
            else if (args[1] == "photographers")
            {

                var photographers = e6Logic.e6Logic.Photographers(data);

                foreach (var dataSet in photographers)
                {
                    Console.WriteLine(dataSet.name + ":\t" + dataSet.count);
                }
            }
        }
    }
}