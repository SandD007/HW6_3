namespace Catalog.Host.Models.Requests
{
    public class CreateUpdateTypeRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
