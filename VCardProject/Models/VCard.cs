namespace VCardProject.Models
{
    public class VCard
    {
        [JsonProperty("name")]
        public Name Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("location")]
        public Location Location { get; set; }
    }
    public class Name
    {
        [JsonProperty("first")]
        public string Firstname { get; set; }
        [JsonProperty("last")]
        public string Surname { get; set; }
    }
    public class Location
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
    }
}
