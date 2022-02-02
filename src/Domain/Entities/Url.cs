namespace Domain.Entities;
public class Url
{
    public Guid Id { get; set; }
    public string ShortUrl { get; set; }
    public int Count { get; set; }
    public DateTime CreatedDateUrl { get; set; }
    public string OriginalUrl { get; set; }
    public ICollection<Click> Clicks { get; set; }
}
