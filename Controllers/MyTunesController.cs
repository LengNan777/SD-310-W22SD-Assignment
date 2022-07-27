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
            return View(vm);
        }

        public IActionResult Collection(int UserId, string RedeemInformation)
        {
            ViewBag.RedeemInformation = RedeemInformation;
            ViewBag.UserName = _db.Users.First(s => s.Id == UserId).Name;
            ViewBag.UserId = UserId;
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
            List<Song> topSellingSongs = _db.Songs.OrderByDescending(s => s.Sales).Take(3).ToList();
            return View(topSellingSongs);
        }

        public IActionResult BuyPage(int UserId,string Information)
        {
            ViewBag.UserId = UserId;
            ViewBag.Balance = _db.Users.First(u => u.Id == UserId).Balance;
            ViewBag.purchaseDetail = Information;
            
            List<Song> ownedSongs = _db.Collections.Include(c => c.User).Where(c => c.UserId == UserId).Select(c=>c.Song).ToList();
            List<Song> allSongs = _db.Songs.Include(s=>s.Artist).ToList();
            List<Song> result = allSongs.Where(s => ownedSongs.All(s2 => s2.Id != s.Id)).ToList();
            return View(result);
        }

        [HttpPost]
        public IActionResult Buy(int SongId,int UserId)
        {
            User u = _db.Users.First(u => u.Id == UserId);
            Song s = _db.Songs.First(s => s.Id == SongId);
            string Information;

            if (u.Balance < s.Price)
            {
                Information = "Sorry! You do not have enough money to buy this song.";
            }
            else
            {
                Information = $"Congratulations! You buy {s.Name} successfully!";
                u.Balance -= s.Price;
                _db.Orders.Add(new Order { UserId = u.Id, SongId = s.Id, Date = DateTime.Now });
                _db.Collections.Add(new Collection { UserId = u.Id, SongId = s.Id });
                _db.SaveChanges();
                
            }
            return RedirectToAction("BuyPage",new { UserId = u.Id, Information = Information });
        }

        [HttpPost]
        public IActionResult Redeem(int UserId,int SongId)
        {
            string RedeemInformation;

            User u = _db.Users.First(u => u.Id == UserId);
            Song s = _db.Songs.First(s => s.Id == SongId);
            Collection c = _db.Collections.Include(c => c.Song).First(c => c.UserId == UserId && c.SongId == SongId);
            Order o = _db.Orders.First(o => o.UserId == UserId && o.SongId == SongId);
            u.Balance += s.Price;
            _db.Collections.Remove(c);
            _db.Orders.Remove(o);
            s.Rating = null;
            _db.SaveChanges();
            RedeemInformation = $"Congratulations! You redeem the {c.Song.Name} successfully!";
                       

            return RedirectToAction("Collection", new { UserId = c.UserId, RedeemInformation = RedeemInformation });
        }

        [HttpGet]
        public IActionResult Rate(int SongId)
        {
            Song s = _db.Songs.First(s => s.Id == SongId);
            return View(s);
        }

        [HttpPost]
        public IActionResult Rate(int SongId,int rate)
        {
            Song s = _db.Songs.First(s => s.Id == SongId);
            s.Rating = rate;
            _db.SaveChanges();
            return View(s);
        }

        public IActionResult Query(string month)
        {
            try
            {
                ViewBag.t = month;
                List<Order> o = _db.Orders.Include(o => o.Song).Where(o => (o.Date).ToString().Substring(0,7) == month).ToList();
                return View(o);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error");
            }
            
        }
    }
}
