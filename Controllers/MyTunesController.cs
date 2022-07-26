using Assignment.Models;
using Assignment.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Controllers
{
    public class MyTunesController : Controller
    {
        private AssignmentContext _db { get; set; }
        public MyTunesController(AssignmentContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            return View(_db.Songs.Include(s => s.Artist).ToList());          
        }

        public IActionResult Artist()
        {
            ArtistSelectViewModel vm = new ArtistSelectViewModel(_db.Artists.ToList());
            return View(vm);
        }

        public IActionResult Song(int ArtistId)
        {
            ViewBag.ArtistName = _db.Artists.First(a => a.Id == ArtistId).Name;
            return View(_db.Songs.Where(s => s.ArtistId == ArtistId).ToList());
        }

        [HttpPost]
        public IActionResult Like(int Id)
        {
            Song likedSong = _db.Songs.First(s => s.Id == Id);
            likedSong.Likes++;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult User()
        {
            UserSelectViewModel vm = new UserSelectViewModel(_db.Users.ToList());
            //return View(_db.Collections.Include(c => c.User).ToList());

            return View(vm);
        }

        public IActionResult Collection(int UserId)
        {
            ViewBag.UserName = _db.Users.First(s => s.Id == UserId).Name;
            return View(_db.Collections.Where(c => c.UserId == UserId).Include(c => c.User).Include(c => c.Song).ThenInclude(s => s.Artist).OrderBy(a => a.Song.Artist.Name).ToList());
        }

        public IActionResult SelectUser(int UserId)
        {
            return RedirectToAction("Collection");
        }

        public IActionResult BackToArtist()
        {
            return RedirectToAction("Artist");
        }

        public IActionResult BackToUser()
        {
            return RedirectToAction("User");
        }

        public IActionResult Top()
        {
            List<Song> topSellingSongs = _db.Songs.OrderBy(s => s.Sales).Take(3).ToList();
            IGrouping<int,Song> resultset = (IGrouping<int, Song>)_db.Songs.GroupBy(s => s.ArtistId).Select().ToList();

            //List<Artist> topSellingArtists = _db.Artists.FromSqlRaw("SELECT TOP 3 SUM(Sales) FROM Song GROUP BY ArtistId Order BY SUM(Sales)").ToList();
            return View(resultset);
        }
    }
}
