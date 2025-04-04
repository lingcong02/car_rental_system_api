namespace car_rental_system_api.ViewModel
{
    public class ImageViewModel
    {
        public string Path { get; set; } = string.Empty;
    }

    public class ImageRequestViewModel
    {
        public int vehicleId { get; set; }
        public string Path { get; set; } = string.Empty;
    }
}
