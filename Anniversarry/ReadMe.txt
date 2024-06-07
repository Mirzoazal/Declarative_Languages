This solution contains the following JSON files:

	team_schema.json
		- a schema for the JSON team data using JSON Schema (draft 7)

	team_good.json 
		- a data file that contains information about a team of hockey players
		- valid according to the schema team_schema.json

	team_bad.json
		- another data file that contains information about a team of hockey players
		- NOT valid according to the schema team_schema.json

This solution also contains the following two projects:

	HockeyTeam
		- a C# project for C# console application that reads a JSON team file
		specified by the user and reports all the information in the console window
		- does NOT validate the JSON team file against the schema
		- uses the Newtonsoft.Json NuGet package to convert JSON to an object

	HocketTeamWithValidation
		- a variation of the HockeyTeam project that has all the same features, but also
		validates a JSON file against the schema contained in the file team_schema.json
		- uses the Newtonsoft.Json NuGet package to convert JSON to an object
		- uses the Newtonsoft.Json.Schema NuGet package to process the validations

======================================================================================
IMPORTANT USAGE NOTE:
	To execute one of the above projects it should first be configured as the 
	"Startup Project". This can be achieved as follows: right-click on the selected
	project in the Solution Explorer window and select "Set as StartUp Project".
======================================================================================
