using System.Diagnostics;
using LinkCompressor.Models;
using LinkCompressor.Repository;
using LinkCompressor.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LinkCompressor.Controllers
{
    public class UrlController : Controller
    {
        private readonly ILogger<UrlController> _logger;
        private readonly IUrlCatalogRepository _repository;

        public UrlController(ILogger<UrlController> logger, IUrlCatalogRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            ViewData["HOST_URL"] = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/";
            var urlList = _repository.GetAll();
            _logger.LogInformation("{VIEW_NAME} : Request for view", nameof(Index));
            return View(urlList);
        }

        [HttpGet("/{path:required}")]
        public IActionResult Follow(string path)
        {
            if (path == null)
            {
                _logger.LogInformation("Link is Empty : {path}", path);
                return NotFound();
            }

            var shortUrl = _repository.Get(ShortURLFork.Decode(path));
            if (shortUrl == null)
            {
                _logger.LogInformation("Unknown link : {path}", path);
                return RedirectToAction(nameof(Index));
            }

            shortUrl.RedirectCounter++;
            _repository.Update(shortUrl);

            _logger.LogInformation("{VIEW_NAME} : Request for view", nameof(Follow));
            return Redirect(shortUrl.LongUrl);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        [HttpGet("/url/edit/{url:required}")]
        public IActionResult Edit(string url)
        {
            if (url == null)
            {
                _logger.LogInformation("URL is Empty : {url}", url);
                return NotFound();
            }

            var item = _repository.Get(ShortURLFork.Decode(url));
            if (item == null)
            {
                _logger.LogInformation("URL not found in DB : {url}", url);
                return NotFound();
            }

            _logger.LogInformation("{VIEW_NAME} : Request for view", nameof(Edit));
            return View(new UrlItem(item.Id, item.LongUrl));
        }

        [HttpPost("/url/edit/{url:required}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string url, UrlItem item)
        {
            if (url == null)
            {
                _logger.LogInformation("URL is Empty : {url}", url);
                return NotFound();
            }

            var id = ShortURLFork.Decode(url);

            if (ModelState.IsValid)
            {
                var viewItem = _repository.Get(id);
                if (viewItem != null)
                {
                    viewItem.LongUrl = item.LongUrl;
                    _repository.Update(viewItem);
                }
                else
                {
                    _logger.LogInformation("URL not found in DB : {url}", url);
                    return NotFound();
                }

                _logger.LogInformation("{VIEW_NAME} : Response for view", nameof(Edit));
                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }


        [HttpGet("/Url/Delete/{url:required}")]
        public IActionResult Delete(string url)
        {
            if (url == null)
            {
                _logger.LogInformation("URL is Empty : {url}", url);
                return NotFound();
            }

            var id = ShortURLFork.Decode(url);

            var item = _repository.Get(id);
            if (item != null)
            {
                _logger.LogInformation("{VIEW_NAME} : Request for view", nameof(Delete));
                return View(new UrlItem(item.Id, item.LongUrl));
            }

            _logger.LogInformation("URL not found in DB : {url}", url);
            return NotFound();
        }

        [HttpPost("/url/delete/{url:required}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string url, UrlItem item)
        {
            if (url == null)
            {
                _logger.LogInformation("URL is Empty : {url}", url);
                return NotFound();
            }

            var id = ShortURLFork.Decode(url);
            if (_repository.Delete(id) > 0)
            {
                _logger.LogInformation("{VIEW_NAME} : Response for view", nameof(Index));
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        public IActionResult Create()
        {
            _logger.LogInformation("{VIEW_NAME} : Request for view", nameof(Create));
            return View(new UrlItem());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UrlItem item)
        {
            if (item != null)
                if (ModelState.IsValid)
                {
                    _repository.Add(new UrlView(item.LongUrl));
                    _logger.LogInformation("{VIEW_NAME} : Response for view", nameof(Create));
                    return RedirectToAction(nameof(Index));
                }

            return View(item);
        }
    }
}