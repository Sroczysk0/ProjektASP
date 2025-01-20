using Microsoft.AspNetCore.Mvc;
using DataModels = Projekt01.Data;
using ViewModels = Projekt01.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Projekt01.Controllers
{
   public class GameController : Controller
{
    private readonly DataModels.AppDbContext _context;

    public GameController(DataModels.AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index(int page = 1, int pageSize = 10)
    {
        var totalGames = _context.game.Count();

        var games = _context.game
            .Join(_context.genre, g => g.genre_id, gr => gr.id, (g, gr) => new
            {
                Game = g,
                GenreName = gr.genre_name
            })
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var gamePublishers = _context.game_publisher
            .Join(_context.publisher, gp => gp.publisher_id, p => p.id, (gp, p) => new
            {
                GameId = gp.game_id,
                PublisherName = p.publisher_name
            })
            .ToList();

        var result = games.Select(g => new ViewModels.GameViewModel
        {
            game_id = g.Game.id,
            game_name = g.Game.game_name,
            genre_name = g.GenreName,
            publisher_name = string.Join(", ", gamePublishers
                .Where(pub => pub.GameId == g.Game.id)
                .Select(pub => pub.PublisherName))
        }).ToList();

        var model = new ViewModels.PaginatedGamesViewModel
        {
            Games = result,
            CurrentPage = page,
            PageSize = pageSize,
            TotalGames = totalGames
        };

        return View(model);
    }

    [Authorize]
    [HttpGet]
    public IActionResult Create()
    {
        var genres = _context.genre.ToList();
        var publishers = _context.publisher.ToList();

        var model = new ViewModels.CreateGameViewModel
        {
            Genres = genres.Select(g => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = g.genre_name,
                Value = g.id.ToString()
            }).ToList(),
            Publishers = publishers.Select(p => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = p.publisher_name,
                Value = p.id.ToString()
            }).ToList()
        };

        return View(model);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ViewModels.CreateGameViewModel model)
    {
        if (ModelState.IsValid)
        {
            var game = new ViewModels.game()
            {
                game_name = model.GameName,
                genre_id = model.SelectedGenreId
            };

            _context.game.Add(game);
            _context.SaveChanges(); 

            foreach (var publisherId in model.SelectedPublisherIds)
            {
                var gamePublisher = new ViewModels.game_publisher()
                {
                    game_id = game.id,
                    publisher_id = publisherId
                };

                _context.game_publisher.Add(gamePublisher);
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        model.Genres = _context.genre.Select(g => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        {
            Text = g.genre_name,
            Value = g.id.ToString()
        }).ToList();

        model.Publishers = _context.publisher.Select(p => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        {
            Text = p.publisher_name,
            Value = p.id.ToString()
        }).ToList();

        return View(model);
    }

    [HttpGet]
    public IActionResult Details(int gameId)
    {
        var game = _context.game
            .Where(g => g.id == gameId)
            .Select(g => new
            {
                GameId = g.id,
                GameName = g.game_name
            })
            .FirstOrDefault();

        if (game == null)
        {
            return NotFound();
        }

        var platforms = _context.game_publisher
            .Where(gp => gp.game_id == gameId)
            .Join(_context.game_platform, gp => gp.id, gpl => gpl.game_publisher_id, (gp, gpl) => new
            {
                gpl.id,
                gpl.platform_id,
                gpl.release_year
            })
            .Join(_context.platform, gpl => gpl.platform_id, plt => plt.id, (gpl, plt) => new
            {
                GamePlatformId = gpl.id,
                PlatformName = plt.platform_name,
                ReleaseYear = gpl.release_year
            })
            .ToList();

        var platformDetails = platforms.Select(platform => new ViewModels.PlatformDetailsViewModel
        {
            PlatformName = platform.PlatformName,
            ReleaseYear = platform.ReleaseYear,
            Regions = _context.region_sales
                .Where(rs => rs.game_platform_id == platform.GamePlatformId)
                .Join(_context.region, rs => rs.region_id, r => r.id, (rs, r) => new ViewModels.RegionSalesViewModel
                {
                    RegionName = r.region_name,
                    NumSales = rs.num_sales
                })
                .ToList()
        }).ToList();

        var model = new ViewModels.GameDetailsViewModel
        {
            GameId = game.GameId,
            GameName = game.GameName,
            PlatformDetails = platformDetails
        };

        return View(model);
    }
}

}
