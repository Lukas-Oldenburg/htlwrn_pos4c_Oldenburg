

// HAU: ℹ️ hint: use single line namespace declarations 
namespace oldenburgPaLib
{
    // HAU: ℹ️ do use a clean coding style - make blank lines between methods
    public class Order
    {
        // HAU: ❌ don't use public fields - use properties 
        public int? id;
        public string? customer;
        public string? country;
        public List<Detail>? details;
        public Order(string[] line)
        {
            id = int.Parse(line[1]);
            customer = line[2];
            country = line[3];
        }
        // HAU: ℹ️ method names should start with uppercase and use PascalCase
        public void addDetail(Detail detail)
        {
            if (details != null)
            {
                details.Add(detail);
            }
        }
        // HAU: ℹ️ method names should start with uppercase and use PascalCase
        // HAU: ❌ don not use getters or/and setters methods - use properties
        public int getId()
        {
            // HAU: ❌ recursive method call
            return this.getId();
        }
    }
}