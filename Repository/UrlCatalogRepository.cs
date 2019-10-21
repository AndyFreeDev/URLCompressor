using System.Collections.Generic;
using System.Linq;
using LinkCompressor.Models;

namespace LinkCompressor.Repository
{
    public class UrlCatalogRepository : IUrlCatalogRepository
    {
        private readonly MySQLContext _context;

        public UrlCatalogRepository(MySQLContext context)
        {
            _context = context;
        }

        public UrlView Add(UrlView item)
        {
            _context.UrlView.Add(item);
            _context.SaveChanges();

            return item;
        }

        public UrlView Get(int? id)
        {
            return _context.UrlView.Find(id);
        }

        public IList<UrlView> GetAll()
        {
            return _context.UrlView.ToList();
        }

        public int Update(UrlView item)
        {
            _context.UrlView.Update(item);
            return _context.SaveChanges();
        }

        public int Delete(int? id)
        {
            var item = Get(id);
            if (item != null)
            {
                _context.Remove(item);
                return _context.SaveChanges();
            }

            return 0;
        }
    }
}