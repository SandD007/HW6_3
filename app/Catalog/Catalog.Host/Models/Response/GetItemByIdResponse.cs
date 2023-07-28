namespace Catalog.Host.Models.Response
{
    public class GetItemByIdResponse<T>
    {
        public IEnumerable<T> Data { get; init; } = null!;
    }
}
