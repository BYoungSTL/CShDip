namespace DTO;

public class PagedListModel
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public bool CanNext { get; set; }
    public bool CanPrevious { get; set; }
}