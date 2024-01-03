namespace VCardProject.Models
{
    public class VCardData
    {
        [JsonProperty("results")]
        public List<VCard> VCardResults { get; set; }
    }
}
