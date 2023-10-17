class Program
{
    static void main(String[] args)
    {
        if (args.Length != 4)
        {
            Console.WriteLine("Wrong arguments");
            return;
        }

        string fileName = args[0];
        string groupingColumn = args[1];
        string numericColumn = args[2];
        int outputCount = int.Parse(args[3]);

        var lines = File.ReadAllLines(fileName);

        if (lines.Length < 2)
        {
            Console.WriteLine("No data to process");
            return;
        }

        string[] header = lines[0].Split('\t');

        if (!header.Contains(groupingColumn) || !header.Contains(numericColumn))
        {
            Console.WriteLine("Columns not found.");
            return;
        }

        int col = Array.IndexOf(header, groupingColumn)
        int num = Array.IndexOf(header, numericColumn)

        var data = lines
            .Skip(1)
            .Select(line => line.Split('\t'))
            .Select(stringArrs => new
            {
                GroupingColumn = stringArrs[col],
                NumericValue = int.Parse(stringArrs[num])
            });

        var groupedData = data
            .GroupBy(item => item.GroupingColumn)
            .Select(group => new
            {
                GroupingColumn = group.Key,
                NumericValue = group.Sum(item => item.NumericValue)
            }).OrderByDescending(item => item.NumericValue).Take(outputCount);


        int maxCount = (args.Length == 4) ? int.Parse(args[3]) : groupedData.Count();
        int maxLength = groupedData.Take(maxCount).Max(n => n.GroupingColumn.Length);
        int baseAmount = groupedData.First().NumericValue;

        foreach (var items in groupedData)
        {
            Console.Write(items.GroupingColumn.PadLeft(maxLength) + " | ");
            Console.BackgroundColor = ConsoleColor.Red;
            int rate = items.NumericValue * 100 / baseAmount;
            for (int j = 1; j <= rate; j++)
            {
                Console.Write(" ");
            }

            Console.ResetColor();
            Console.WriteLine();
        }
    }
}