using Observatory.Framework.Files.Journal;
using System.Numerics;

namespace Akiyama.EDOCCommon.Elite
{
    public static class EliteUtilities
    {

        /// <summary>
        /// Calculate the distance in light years between the 2 specified coordinates.
        /// </summary>
        /// <param name="pos1">Coordinates of the first system</param>
        /// <param name="pos2">Coordinates of the second system</param>
        /// <returns>A <see langword="double"/> representing the distance between the two systems</returns>
        public static double CalculateDistance(Vector3 pos1, Vector3 pos2) => CalculateDistance(pos1.X, pos1.Y, pos1.Z, pos2.X, pos2.Y, pos2.Z);
        /// <summary>
        /// Calculate the distance in light years between the 2 specified coordinates.
        /// </summary>
        /// <param name="x1">The X coordinate of the first system</param>
        /// <param name="y1">The Y coordinate of the first system</param>
        /// <param name="z1">The Z coordinate of the first system</param>
        /// <param name="x2">The X coordinate of the second system</param>
        /// <param name="y2">The Y coordinate of the second system</param>
        /// <param name="z2">The Z coordinate of the second system</param>
        /// <returns>A <see langword="double"/> representing the distance between the two systems</returns>
        public static double CalculateDistance(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            return Math.Sqrt(Math.Pow(x1 + x2, 2) + Math.Pow(y1 + y2, 2) + Math.Pow(z1 + z2, 2));
        }

        /// <summary>
        /// Converts <paramref name="scan"/> into a string representation of the body the scan belongs to.
        /// </summary>
        /// <param name="scan">The <see cref="Scan"/> object to convert</param>
        /// <returns>A <see langword="string"/> representation of the body provided in the <see cref="Scan"/></returns>
        public static string FixedBodyClassFromScan(Scan scan)
        {
            string _class = FixedBodyClass(scan.StarType ?? scan.PlanetClass);
            if (scan.TerraformState == "Terraformable") { _class += " (terraformable)"; }
            return _class;
        }

        /// <summary>
        /// Convert a body or planet's class into one which more closely matches those seen in-game. Most bodies don't require any changes and will return the same value that was provided.
        /// </summary>
        /// <remarks>
        /// <b>Note</b>: While this method will convert star types, if you know you're dealing with a star it is recommended to use <seealso cref="FixStarClass(string)"/> instead.
        /// </remarks>
        /// <param name="bodyClass">The planet/body class to convert</param>
        /// <returns>A <see langword="string"/> with the converted class</returns>
        public static string FixedBodyClass(string bodyClass)
        {

            if (bodyClass.StartsWith("Sudarsky"))
            {
                /*
                 * Because it's simpler than creating code to do it:
                 * 
                 * Sudarsky class I gas giant
                 * `-> lass I gas giant
                 *  `-> Class I gas giant
                 */
                return "C" + bodyClass[10..];
            }

            return FixStarClass(bodyClass);

        }

        /// <summary>
        /// Changes the provided star class into its "proper" name (ie. "DAB" => "White Dwarf")
        /// </summary>
        /// <remarks>
        /// <br /><b>Note</b>: Single-letter star classes (OBAFGKMTYL) require no conversion and will be returned as-is.
        /// </remarks>
        /// <param name="starClass">The star class to be converted</param>
        /// <returns>A <see langword="string"/> with the converted value</returns>
        public static string FixStarClass(string starClass)
        {
            switch (starClass)
            {
                case "N":
                    return "Neutron star";
                case "D" or "DA" or "DAB" or "DAO" or "DAZ" or "DAV" or "DB" or "DBZ" or "DBV" or "DO" or "DOV" or "DQ" or "DC" or "DCV" or "DX":
                    return "White Dwarf";

                case "W" or "WN" or "WNC" or "WC" or "WO":
                    return "Wolf-Rayet star";

                case "AeBe":
                    return "Herbig Ae/Be star";

                case "H":
                    return "Black hole";
                case "SupermassiveBlackHole":
                    return "Supermassive Black Hole";

                case "K_OrangeGiant" or "M_RedGiant":
                    return $"{starClass.Split('_')[0]} (Giant)";

                case "B_BlueWhiteSuperGiant" or "A_BlueWhiteSuperGiant" or "F_WhiteSuperGiant" or "G_WhiteSuperGiant" or "M_RedSuperGiant":
                    return $"{starClass.Split('_')[0]} (Super Giant)";

                default:
                    return starClass;
            }
        }

        /// <summary>
        /// Takes a given star class and converts it into its respective colour designation. Eg: <c>"O" => "O (Blue-white)</c>"
        /// </summary>
        /// <param name="starType">The star type to convert</param>
        /// <returns>A <see langword="string"/> of the provided star class' colour designation</returns>
        public static string StarColourName(string starType)
        {
            switch (starType)
            {
                case "O" or "B" or "A":
                    return $"{starType} (Blue-white)";
                case "F":
                    return $"{starType} (White)";
                case "G":
                    return $"{starType} (White-yellow)";
                case "K":
                    return $"{starType} (Yellow-orange)";
                case "M":
                    return $"{starType} (Red dwarf)";

                case "T" or "Y" or "L":
                    return $"{starType} (Brown dwarf)";

                case "N":
                    return "Neutron star";
                case "D" or "DA" or "DAB" or "DAO" or "DAZ" or "DAV" or "DB" or "DBZ" or "DBV" or "DO" or "DOV" or "DQ" or "DC" or "DCV" or "DX":
                    return $"White dwarf ({starType})";

                case "W" or "WN" or "WNC" or "WC" or "WO":
                    return $"Wolf-Rayet ({starType})";

                case "AeBe":
                    return "Herbig Ae/Be";

                case "H":
                    return "Black hole";
                case "SupermassiveBlackHole":
                    return "Supermassive Black Hole";

                case "K_OrangeGiant":
                    return "K (Orange Giant)";

                case "B_BlueWhiteSuperGiant" or "A_BlueWhiteSuperGiant":
                    return $"Blue-white Super Giant ({starType.Split('_')[0]})";

                case "F_WhiteSuperGiant" or "G_WhiteSuperGiant":
                    return $"White Super Giant ({starType.Split('_')[0]})";

                case "M_RedGiant":
                    return "M (Red Giant)";

                case "M_RedSuperGiant":
                    return "M (Red Super Giant)";

                default:
                    return starType;
            }
        }

        // The might be useful eventually, idk
        /// <summary>
        /// Converts a given star type into a integer for a colour representing that star's class
        /// </summary>
        /// <remarks>
        /// <b>Note</b>: These values are largely arbitrary as there are no "official" designations, but they should be fairly representitive of their star.
        /// </remarks>
        /// <param name="starType">The star type to convert</param>
        /// <returns>An <see langword="int"/> representation of a hexadecimal colour value for the given star type</returns>
        public static int StarTypeColourInt(string starType)
        {
            switch (starType)
            {
                case "O":
                    return 0x0;
                case "B" or "B_BlueWhiteSuperGiant":
                    return 0xb6d3db;
                case "A" or "A_BlueWhiteSuperGiant":
                    return 0xbcc8cf;
                case "F" or "F_WhiteSuperGiant":
                    return 0xd4c49b;
                case "G" or "G_WhiteSuperGiant":
                    return 0xe9b066;
                case "K" or "K_OrangeGiant":
                    return 0xf6a84b;
                case "M" or "M_RedGiant" or "M_RedSuperGiant":
                    return 0xcb673f;

                case "T" or "Y" or "L":
                    return 0x500c2e;

                // Neutron and white dwarf
                case "N" or "D" or "DA" or "DAB" or "DAO" or "DAZ" or "DAV" or "DB" or "DBZ" or "DBV" or "DO" or "DOV" or "DQ" or "DC" or "DCV" or "DX":
                    return 0xb5b8f4;

                case "H" or "SupermassiveBlackHole":
                    return 0x595959;

                default:
                    return 0x0;
            }
        }
    }
}
