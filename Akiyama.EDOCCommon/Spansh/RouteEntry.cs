using CsvHelper.Configuration.Attributes;
using System.Text.Json.Serialization;

namespace Akiyama.EDOCCommon.Spansh
{
    [CultureInfo("en-GB")]
    public class RouteEntry
    {

        /// <summary>
        /// The name of the system.
        /// </summary>
        [Name("System Name")]
        [JsonPropertyName("name")]
        public string SystemName { get; set; }

        /// <summary>
        /// The distance (in lightyears) of this jump.
        /// </summary>
        [Name("Distance")]
        [JsonPropertyName("distance")]
        public double Distance { get; set; }

        /// <summary>
        /// The distance (in lightyears) remaining until the destination.
        /// </summary>
        [Name("Distance Remaining")]
        [JsonPropertyName("distance_to_destination")]
        public double DistanceRemaining { get; set; }

        /// <summary>
        /// The amount of fuel (in tonnes) left in the ship's fuel tank after completing this jump.
        /// </summary>
        [Name("Fuel Left")]
        [JsonPropertyName("fuel_in_tank")]
        public double FuelLeft { get; set; }

        /// <summary>
        /// The amount of fuel (in tonnes) this jump will use.
        /// </summary>
        [Name("Fuel Used")]
        [JsonPropertyName("fuel_used")]
        public double FuelUsed { get; set; }

        /// <summary>
        /// Whether the system is question is a refuel point.
        /// </summary>
        [Name("Refuel")]
        [BooleanTrueValues(["Yes", "yes", "1"])]
        [BooleanFalseValues(["No", "no", "0"])]
        [JsonPropertyName("must_refuel")]
        public bool Refuel { get; set; }

        /// <summary>
        /// Whether the system contains at least one Neutron star.
        /// </summary>
        [Name("Neutron Star")]
        [BooleanTrueValues(["Yes", "yes", "1"])]
        [BooleanFalseValues(["No", "no", "0"])]
        [JsonPropertyName("has_neutron")]
        public bool Neutron { get; set; }

        /// <summary>
        /// The <see langword="UInt64"/> indentifier for the system.
        /// </summary>
        /// <remarks>
        /// Only available when a route is parsed from a JSON export. When another import source is used, this value will always be <see langword="0"/>.
        /// </remarks>
        [Ignore]
        [JsonPropertyName("id64")]
        public UInt64 SystemID64 { get; set; } = 0;

        /// <summary>
        /// Whether the system in question has at least one scoopable star.
        /// </summary>
        /// <remarks>
        /// Only available when a route is parsed from a JSON export. When another import source is used, this value will always be <see langword="false"/>.
        /// </remarks>
        [Ignore]
        [JsonPropertyName("is_scoopable")]
        public bool Scoopable { get; set; } = false;

        /// <summary>
        /// Whether this jump has been completed or not
        /// </summary>
        /// <remarks>
        /// This is a meta variable designed to be used by plugins and should not be modified by other sources outside of them.
        /// </remarks>
        [Ignore]
        [JsonIgnore]
        public bool Completed { get; set; } = false;

    }
}
