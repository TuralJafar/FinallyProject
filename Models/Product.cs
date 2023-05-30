namespace FinallyProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }

    }
}
