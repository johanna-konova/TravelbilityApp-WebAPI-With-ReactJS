namespace TravelbilityApp.Core.DTOs.Common
{
    public class PagedResultDto<T>
    {
        public IEnumerable<T> Items { get; init; } = null!;
        public int TotalCount { get; init; }
        public int CurrentPageNumber { get; init; }
        public int ItemsPerPage { get; init; }
    }
}
