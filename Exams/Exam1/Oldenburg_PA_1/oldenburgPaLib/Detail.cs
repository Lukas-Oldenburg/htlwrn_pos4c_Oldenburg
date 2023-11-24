namespace oldenburgPaLib
{
    public class Detail
    {
        // HAU: ❌ don't use public fields - use properties 
        public string? product;
        public int? amount;
        public int? price;
        public int? total;
        public Detail(string[] line)
        {
            product = line[1];
            amount = int.Parse(line[2]);
            price = int.Parse(line[3]);
            total = int.Parse(line[4]);
        }
    }
}