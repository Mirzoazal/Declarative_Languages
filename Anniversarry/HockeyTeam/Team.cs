/*
 * File:            Team.cs
 * Date:            May 8, 2023
 * Course:          INFO-3138
 * Description:     Defines the Team and Player classes 
 */

namespace HockeyTeam
{
    class Player
    {
        public string Name { get; set; } = "";
        public ushort Number { get; set; }
        public string Position { get; set; } = "";
    } // end class Player

    class Team
    {
        public string Name { get; set; } = "";
        public Player[] Players = Array.Empty<Player>();
    } // end class Team
}