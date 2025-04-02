namespace car_rental_system_api.Helper
{
    public class GeneralHelper
    {
        public static string GenerateBookingNo()
        {
            string timestamp = DateTime.UtcNow.ToString("yyyyMMdd-HHmmss");
            string randomPart = new Random().Next(1000, 9999).ToString(); // 4-digit random number
            return $"{timestamp}-{randomPart}";
        }
    }
}
