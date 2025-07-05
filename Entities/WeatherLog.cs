namespace Weather_API_Wrapper_Service.Entities
{
    public class WeatherLog
    {
        public Guid Id { get; set; }
        public string Location { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public double Temperature { get; set; }
        public string Condition { get; set; } = string.Empty;
        public string ResponseSource { get; set; } = "API"; // "Cache" or "API"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
