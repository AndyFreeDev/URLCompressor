using System.ComponentModel.DataAnnotations;

namespace LinkCompressor.Models
{
    public class UrlItem
    {
        public UrlItem()
        {
        }

        public UrlItem(in int id, string longUrl)
        {
            Id = id;
            LongUrl = longUrl;
        }

        public int Id { get; set; }
        [Required]
        [Url]
        public string LongUrl { get; set; }
    }
}