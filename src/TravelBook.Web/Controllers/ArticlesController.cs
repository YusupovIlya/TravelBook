using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelBook.Core.ProjectAggregate;
using TravelBook.Web.ViewModels.ArticleViewModels;

namespace TravelBook.Web.Controllers
{
    public class ArticlesController : BaseController
    {
        private readonly ILogger<ArticlesController> _logger;
        private readonly ITravelRepository _travelRepository;
        public ArticlesController(UserManager<IdentityUser> userManager,
                                  ILogger<ArticlesController> logger,
                                  IMapper mapper,
                                  IMediator mediator,
                                  ITravelRepository travelRepository)
                                  : base(userManager, mapper, mediator)
        {
            _logger = logger;
            _travelRepository = travelRepository;
        }

        [HttpGet]
        public async Task<IActionResult> FullArticle([FromQuery] int articleId)
        {
            return await ControllerAction<Article, NullReferenceException>(

                    articleId,

                    async (id) => await _travelRepository.GetArticleById(id),

                    (Article article) =>
                    {
                        var articleModel = _mapper.Map<ArticleViewModel>(article);
                        return View(articleModel);
                    });
        }

        [HttpGet]
        public async Task<IActionResult> TravelArticles([FromQuery] int travelId)
        {
            ViewBag.TravelId = travelId;
            return await ControllerAction<Article, ArgumentNullException>(

                    travelId,

                    async (id) => await _travelRepository.GetArticlesByTravelId(id),

                    (Article[] articles) =>
                    {
                        ViewBag.IsEmpty = false;
                        var articlesModel = _mapper.Map<IEnumerable<ArticleViewModel>>(articles);
                        return View("~/Views/Articles/ListArticles.cshtml", articlesModel);
                    },

                    () =>
                    {
                        ViewBag.IsEmpty = true;
                        return View("~/Views/Articles/ListArticles.cshtml");
                    });
        }

        [HttpGet]
        public IActionResult Create([FromQuery] int travelId)
        {
            ViewBag.TravelId = travelId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] NewArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                Article newArticle = _mapper.Map<Article>(model);
                await _travelRepository.AddArticle(newArticle);
                return RedirectToAction(nameof(ArticlesController.TravelArticles), new {travelId=model.TravelId});
            }
            ViewBag.TravelId = model.TravelId;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromQuery] int articleId)
        {
            return await ControllerAction<Article, NullReferenceException>(

                    articleId,

                    async (id) => await _travelRepository.GetArticleById(id),

                    (Article article) =>
                    {
                        var articleModel = _mapper.Map<EditArticleViewModel>(article);
                        return View(articleModel);
                    });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] EditArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                Article newArticle = _mapper.Map<Article>(model);
                await _travelRepository.EditArticle(newArticle);
                return RedirectToAction(nameof(ArticlesController.TravelArticles), new { travelId = model.TravelId });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromQuery] int articleId)
        {
            return await ControllerAction<Article, NullReferenceException>(

                    articleId,

                    async (id) => await _travelRepository.GetArticleById(id),

                    async (Article article) =>
                    {
                        await _travelRepository.RemoveArticle(article);
                        return RedirectToAction(nameof(ArticlesController.TravelArticles), new { travelId = article.TravelId });
                    });
        }
    }
}
