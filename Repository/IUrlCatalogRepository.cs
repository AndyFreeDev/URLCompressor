using System.Collections.Generic;
using LinkCompressor.Models;

namespace LinkCompressor.Repository
{
    public interface IUrlCatalogRepository
    {
        UrlView Add(UrlView item);

        UrlView Get(int? id);
        IList<UrlView> GetAll();

        int Update(UrlView item);

        int Delete(int? id);
    }
}