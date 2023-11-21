
using System.Linq;
using System.Text.Json;


namespace e6Logic
{



    public record Time(string STime, double Count);
    public record Picture(string Pic, IEnumerable<Time> Times, double TotalCount);
    public record Photographers(string name, int count);

    public class e6Logic
    {


        public static IEnumerable<Picture> Month(string[] data)
        {
            var dataMonth = data.Skip(1)
               .Select(line => line.Split("\t"))
               .Select(line => new
               {
                   pic = line[0],
                   time = line[1][..7]
               })
               .GroupBy(entry => entry.pic)

               .Select(group =>
               {
                   var month = group
                   .GroupBy(entry => entry.time)
                   .Select(entry => new Time(entry.Key, entry.Count()))
                   .OrderBy(entry => entry.STime);

                   return new Picture(group.Key, month, group.Count());

               })
               .OrderBy(entry => entry.Pic);

            return dataMonth;
        }


        public static IEnumerable<Picture> Hourly(string[] data)
        {
            var dataTime = data.Skip(1)
                .Select(line => line.Split("\t"))
                .Select(entry => new
                {
                    pic = entry[0],
                    time = entry[2].Split(":").ToArray()[0]
                })
                .GroupBy(entry => entry.pic)
                .Select(group =>
                {

                    var time = group.GroupBy(entry => entry.time)
                    .Select(entry => new Time(entry.Key, Math.Round((double)entry.Count() * 100 / group.Count(), 2)))
                    .OrderBy(entry => int.Parse(entry.STime));

                    return new Picture(group.Key, time, Math.Round(time.Sum(entry => entry.Count), 0));

                }).OrderBy(entry => entry.Pic);

            return dataTime;
        }


        public static IEnumerable<Photographers> Photographers(String[] data)
        {
            var photos = JsonSerializer.Deserialize<List<PhotoEntry>>(File.ReadAllText("photographers.json"));

            var dataPhotos = data.Skip(1)
               .Select(line => line.Split("\t"))
               .Select(line => new
               {
                   pic = line[0],
                   time = line[1][..7]
               });

            var dataPhotographers = dataPhotos
                .GroupBy(entry => photos.First(photo => photo.Pic == entry.pic).TakenBy) // Group by photographer name
                .Select(group =>
                {
                    var name = group.Key;

                    return new Photographers(name, group.Count());

                })
                .OrderByDescending(entry => entry.count);

            return dataPhotographers;
        }
    }
}