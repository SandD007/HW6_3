namespace Catalog.Host.Models.Requests
{
    public class CreateUpdateBrandRequest
    {
        public int Id { get; set; }
        public string Brand { get; set; } = null!;
    }
}
