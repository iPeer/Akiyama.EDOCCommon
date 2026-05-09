using CsvHelper;
using CsvHelper.Configuration;

namespace Akiyama.EDOCCommon.Spansh
{
    public class PlottedRoute
    {


        public List<RouteEntry> Jumps = [];

        public static PlottedRoute FromFile(string path)
        {
            PlottedRoute route = new();

            // Determine whether we're dealing with a JSON file, or a CSV file
            // Do we trust the extension or not? (Would a user deliberately change it to fuck with us? Well, if they do, they'll crash their shit so that's on them I guess
            if (Path.GetExtension(path) == ".csv")
            {
                CsvConfiguration cfg = CsvConfiguration.FromAttributes<RouteEntry>();
                using (StreamReader r = new StreamReader(path))
                {
                    using (CsvReader csv = new CsvReader(r, cfg))
                    {
                        route.Jumps = csv.GetRecords<RouteEntry>().ToList();
                    }
                }
            }
            else if (Path.GetExtension(path) == ".json")
            {

            }
            else
            {
                MessageBox.Show("The file provided was not a valid Spansh route export", "Incorrect file type");
            }


            return route;
        }


    }
}
