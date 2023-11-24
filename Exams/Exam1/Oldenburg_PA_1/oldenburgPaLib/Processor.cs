
using System.Linq;

// HAU: ℹ️ use better namespaces structure like OrderStatistics.Lib
namespace oldenburgPaLib
{
    public class Processor
    {
        public static List<Order> CheckType(string[] data)
        {
            string[] lines = data.Skip(2).ToArray();
            List<Order> orders = new List<Order>();
            Order? order = null;
            Detail? detail = null;
            foreach (string line in lines)
            {
                if (line.StartsWith("ORDER"))
                {
                    if (order != null)
                    {
                        // HAU: ❌ use List.Add instead
                        //         IEnumerable.Append returns a new instance IEnumerable 
                        //         because of this the order never gets appended to the original orders list
                        orders.Append(order);

                        // HAU> fixed this for follow up checks
                        orders.Add(order);
                        // <HAU
                    }

                    order = new Order(line.Split("\t"));
                }
                if (line.StartsWith("DETAIL"))
                {
                    detail = new Detail(line.Split("\t"));
                    order.addDetail(detail);
                }
            }
            return orders;
        }
    }
}