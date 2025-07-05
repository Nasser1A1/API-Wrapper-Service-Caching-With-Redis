namespace Weather_API_Wrapper_Service.DTOs
{
    public class WeatherRequestDto
    {
        public string Location { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
