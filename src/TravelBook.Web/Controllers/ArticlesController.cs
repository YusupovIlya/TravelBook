using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelBook.Core.ProjectAggregate;
using TravelBook.Web.ViewModels.ArticleViewModels;

namespace TravelBook.Web.Controllers
{
    public class ArticlesController : BaseController
    {
        private readonly ILogger<TravelController> _logger;
        private readonly IMapper _mapper;
        private readonly ITravelRepository _travelRepository;
        public ArticlesController(UserManager<IdentityUser> userManager,
                                ILogger<TravelController> logger,
                                IMapper mapper,
                                ITravelRepository travelRepository) : base(userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _travelRepository = travelRepository;
        }

        // GET: Articles
        //public async Task<IActionResult> Index()
        //{
        //    var appDbContext = _context.Articles.Include(a => a.Travel);
        //    return View(await appDbContext.ToListAsync());
        //}

        [HttpGet]
        public async Task<IActionResult> FullArticle(int articleId)
        {
            try
            {
                (string ownerId, Article article) = await _travelRepository.GetArticleById(articleId);
                if (CheckAccessByUserId(ownerId))
                {
                    var articleModel = _mapper.Map<ArticleViewModel>(article);
                    return View(articleModel);
                }
                else
                    return Forbid();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> TravelArticles(int travelId)
        {
            try
            {
                (string ownerId, Article[] articles) = await _travelRepository.GetArticlesByTravelId(travelId);
                if (CheckAccessByUserId(ownerId))
                {
                    var articlesModel = _mapper.Map<IEnumerable<ArticleViewModel>>(articles);
                    return View("~/Views/Articles/ListArticles.cshtml", articlesModel);
                }
                else
                    return Forbid();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        //// GET: Articles/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Articles == null)
        //    {
        //        return NotFound();
        //    }

        //    var article = await _context.Articles
        //        .Include(a => a.Travel)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (article == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(article);
        //}

        //// GET: Articles/Create
        //public IActionResult Create()
        //{
        //    ViewData["TravelId"] = new SelectList(_context.Travels, "Id", "UserId");
        //    return View();
        //}

        //// POST: Articles/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Title,Text,TravelId,Id")] Article article)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(article);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["TravelId"] = new SelectList(_context.Travels, "Id", "UserId", article.TravelId);
        //    return View(article);
        //}

        //// GET: Articles/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Articles == null)
        //    {
        //        return NotFound();
        //    }

        //    var article = await _context.Articles.FindAsync(id);
        //    if (article == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["TravelId"] = new SelectList(_context.Travels, "Id", "UserId", article.TravelId);
        //    return View(article);
        //}

        //// POST: Articles/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Title,Text,TravelId,Id")] Article article)
        //{
        //    if (id != article.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(article);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ArticleExists(article.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["TravelId"] = new SelectList(_context.Travels, "Id", "UserId", article.TravelId);
        //    return View(article);
        //}

        //// GET: Articles/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Articles == null)
        //    {
        //        return NotFound();
        //    }

        //    var article = await _context.Articles
        //        .Include(a => a.Travel)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (article == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(article);
        //}

        //// POST: Articles/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Articles == null)
        //    {
        //        return Problem("Entity set 'AppDbContext.Articles'  is null.");
        //    }
        //    var article = await _context.Articles.FindAsync(id);
        //    if (article != null)
        //    {
        //        _context.Articles.Remove(article);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ArticleExists(int id)
        //{
        //  return (_context.Articles?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
