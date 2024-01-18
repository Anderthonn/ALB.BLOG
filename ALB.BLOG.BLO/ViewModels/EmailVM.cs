namespace ALB.BLOG.BLO.ViewModels
{
    public class EmailVM
    {
        public string Name { get; set; }
        public string UserEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string MacAddress { get; set; } = "";
        public DateTime ShippingDate { get; set; }
    }
}