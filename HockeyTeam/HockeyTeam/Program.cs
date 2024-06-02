/*
 * Program:         HockeyTeam
 * File:            Program.cs
 * Date:            May 8, 2023
 * Course:          INFO-3138
 * Description:     Reports all the data contained in a JSON 'team' file.
 */

using System;

using System.IO;        // File class
using Newtonsoft.Json;  // JsonConvert class

namespace HockeyTeam
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hockey Team Player List\n-----------------------");

            // Repeatedly prompt user for data file path until user enters nothing
            bool done = false;
            do
            {
                // Ask the user to input a json data file path
                Console.Write("\nEnter the path name of a JSON team file (or press enter to quit): ");
                string pathName = Console.ReadLine() ?? "";

                if (string.IsNullOrEmpty(pathName))
                {
                    // All done!
                    Console.WriteLine("\nNothing to do. Shutting down.\n");
                    done = true;
                }
                else
                {
                    // Attempt to read the json data file into a string variable
                    if (ReadFile(pathName, out string jsonData))
                    {
                        DisplayTeamInfo(jsonData);
                    }
                    else
                    {
                        // Read operation for data failed
                        Console.WriteLine("\nERROR:\tUnable to read the data file. Try another path or press enter to quit.");
                    }
                }
            } while (!done);

        } // end Main()

        // Attempts to read the json file specified by 'path' into the string 'json'
        // Returns 'true' if successful or 'false' if it fails
        private static bool ReadFile(string path, out string json)
        {
            try
            {
                // Read JSON file data 
                json = File.ReadAllText(path);
                return true;
            }
            catch
            {
                json = "";
                return false;
            }
        } // end ReadFile()

        private static void DisplayTeamInfo(string json)
        {
            try
            {
                // Read JSON file and convert to a Team object
                Team team = JsonConvert.DeserializeObject<Team>(json) ?? new();

                // Report team info
                Console.WriteLine($"\n\tTeam: {team.Name}");
                Console.WriteLine("\n\tRoster...\n");
                foreach (Player p in team.Players)
                {
                    // Report player info
                    Console.WriteLine($"\t#{p.Number}: {p.Name}, {p.Position}");
                }
            }
            catch (JsonException)
            {
                Console.WriteLine("\nERROR: Failed to convert data in the JSON file to a Team object");
            }
        } // end DisplayTeamInfo()
    }
}
