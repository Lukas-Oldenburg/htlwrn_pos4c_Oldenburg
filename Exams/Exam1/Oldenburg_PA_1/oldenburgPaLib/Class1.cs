
namespace oldenburgPaLib
{

    // HAU: ℹ️ not used code
public record Revenue(int Id, int sum);

    // HAU: ℹ️ use proper naming - Class1 is not good
    //         file name should be the same as class name 
    public class Class1
    {
        /// <summary>
        /// summarizes all total prices of all products per order id
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static string[] GetTotalPerOrder(List<Order> lines)
        {
            return lines
                .Select(line => line.id)
                .GroupBy(order => order.Value)
                .Select(groupedById => $"{groupedById.Key},{groupedById.Count()}")
                .ToArray();
        }
    }
}