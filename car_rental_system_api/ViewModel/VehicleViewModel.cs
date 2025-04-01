namespace car_rental_system_api.ViewModel
{
    public class VehicleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Model { get; set; }
        public string PlatNo { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<ImageViewModel> Image { get; set; } = new();
    }
}
