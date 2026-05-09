using CsvHelper.Configuration.Attributes;
using System.Text.Json.Serialization;

namespace Akiyama.EDOCCommon.Spansh
{
    [CultureInfo("en-GB")]
    public class RouteEntry
    {

        [Name("System Name")]
        [JsonPropertyName("name")]
        public string SystemName { get; set; }

        [Name("Distance")]
        [JsonPropertyName("distance")]
        public decimal Distance { get; set; }

        [Name("Distance Remaining")]
        [JsonPropertyName("distance_to_destination")]
        public decimal DistanceRemaining { get; set; }

        [Name("Fuel Left")]
        [JsonPropertyName("fuel_in_tank")]
        public decimal FuelLeft { get; set; }

        [Name("Fuel Used")]
        [JsonPropertyName("fuel_used")]
        public decimal FuelUsed { get; set; }

        [Name("Refuel")]
        [BooleanTrueValues(["Yes", "yes", "1"])]
        [BooleanFalseValues(["No", "no", "0"])]
        [JsonPropertyName("must_refuel")]
        public bool Refuel { get; set; }

        [Name("Neutron Star")]
        [BooleanTrueValues(["Yes", "yes", "1"])]
        [BooleanFalseValues(["No", "no", "0"])]
        [JsonPropertyName("has_neutron")]
        public bool Neutron { get; set; }

    }
}
