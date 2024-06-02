/*
 * Program:         HockeyTeamWithValidation
 * File:            Program.cs
 * Date:            May 8, 2023
 * Course:          INFO-3138
 * Description:     Reports all the data contained in a JSON 'team' file.
 * 
 *                  This version VALIDATES the JSON data file (specified by the user) 
 *                  against a JSON schema.
 */

using System;

using System.Collections.Generic;   // IList<> class
using System.IO;                    // File class
using Newtonsoft.Json;              // JsonConvert class
using Newtonsoft.Json.Schema;       // JSchema  class
using Newtonsoft.Json.Linq;         // JObject class
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HockeyTeamWithValidation
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hockey Team Player List (Validated)\n-----------------------------------");

            // Attempt to read the json schema file into a string variable
            if (ReadFile("team_schema.json", out string jsonSchema))
            {
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
                            // Validate the json data against the schema
                            if (ValidateTeamData(jsonData, jsonSchema, out IList<string> messages)) // Note: messages parameter is optional
                            {
                                Console.WriteLine($"\nData file is valid.");

                                DisplayTeamInfo(jsonData);
                            }
                            else
                            {
                                Console.WriteLine($"\nERROR:\tData file is invalid.\n");

                                // Report validation error messages
                                foreach (string msg in messages)
                                    Console.WriteLine($"\t{msg}");
                            }
                        }
                        else
                        {
                            // Read operation for data failed
                            Console.WriteLine("\nERROR:\tUnable to read the data file. Try another path or press enter to quit.");
                        }
                    }
                } while (!done);
            }
            else
            {
                // Read operation for schema failed
                Console.WriteLine("ERROR:\tUnable to read the schema file.");
            }
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

        // Reports the information contained in the parameter 'json' in the console window
        private static void DisplayTeamInfo(string json)
        {
            try
            {
                // Read JSON file and convert to a Team object
                AnniversarriesArray Anyarr = JsonConvert.DeserializeObject<AnniversarriesArray>(json) ?? new();

                // Report team info
                Console.WriteLine("┌────────────────────────────────────────────────────────────────┐");
                Console.WriteLine("│            ANNIVERSARY MINDER ~ Selected Anniversaries         │");
                Console.WriteLine("├────────────────────────────────────────────────────────────────┤");
                Console.WriteLine("└────────────────────────────────────────────────────────────────┘");

                foreach (Anniversary a in Anyarr.Anniversaries)
                {
                    // Report player info
                    Console.WriteLine($"\t#{a.AnniversaryDate}: {a.Names}, {a.AnniversaryType}");
                }
            }
            catch (JsonException)
            {
                Console.WriteLine("\nERROR: Failed to convert data in JSON file to a Team object");
            }
        } // end DisplayTeamInfo()

        // Validates the json data specified by the parameter 'jsonData' against the schema
        // specified by the parameter 'jsonSchema'
        // Returns 'true' if valid or 'false' if invalid
        // Also populates the out parameter 'messages' with validation error messages if invalid
        private static bool ValidateTeamData(string jsonData, string jsonSchema, out IList<string> messages)
        {
            JSchema schema = JSchema.Parse(jsonSchema);
            JObject team = JObject.Parse(jsonData);
            return team.IsValid(schema, out messages);
        } // end ValidateTeamData()
    }
}
