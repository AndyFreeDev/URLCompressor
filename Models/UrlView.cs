using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LinkCompressor.Utils;

namespace LinkCompressor.Models
{
    [Table("URL_CATALOG")]
    public class UrlView
    {
        public UrlView()
        {
            CreationDate = DateTime.Now;
        }

        public UrlView(string longUrl)
        {
            LongUrl = longUrl;
            CreationDate = DateTime.Now;
        }

        private string _shortUrl = null;

        [Column("ID")]
        public int Id { get; set; }
        
        [Column("LONG_URL")]
        [Required]
        [Url]
        public string LongUrl { get; set; }

        [NotMapped]
        public string ShortUrl
        {
            get
            {
                if (_shortUrl == null)
                {
                    _shortUrl = ShortURLFork.Encode(Id);
                }
                return _shortUrl;
            }
        }

        [Column("REDIRECT_COUNT")]
        public int RedirectCounter { get; set; }

        [Column("CREATION_DATE")]
        public DateTime CreationDate { get; set; }
    }
}