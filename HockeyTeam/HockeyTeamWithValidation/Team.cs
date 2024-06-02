/*
 * File:            Team.cs
 * Date:            May 8, 2023
 * Course:          INFO-3138
 * Description:     Defines the Team and Player classes 
 */

namespace HockeyTeamWithValidation
{
    class Address
    {
        public string StreetAddress { get; set; } = "";
        public string Municipality { get; set; } = "";
        public string Province { get; set; } = "";
        public string PostalCode { get; set; } = "";
    } // end class Address

    class Anniversary
    {
        public string Names { get; set; } = "";
        public string AnniversaryDate { get; set; } = "";
        public string AnniversaryType { get; set; } = "";
        public string Description { get; set; } = "";
        public string Email { get; set; } = "";
        public Address Address { get; set; } = new Address();
    } // end class Anniversary

    class AnniversarriesArray
    {
        public Anniversary[] Anniversaries { get; set; } = Array.Empty<Anniversary>();
    } // end class Team
}
