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

        //public IActionResult Top()
        //{
        //    List<Song> topSellingSongs = _db.Songs.OrderBy(s => s.Sales).Take(3).ToList();
        //    IGrouping<int,Song> resultset = (IGrouping<int, Song>)_db.Songs.GroupBy(s => s.ArtistId).Select().ToList();

        //    //List<Artist> topSellingArtists = _db.Artists.FromSqlRaw("SELECT TOP 3 SUM(Sales) FROM Song GROUP BY ArtistId Order BY SUM(Sales)").ToList();
        //    return View(resultset);
        //}

        public IActionResult BuyPage(int UserId,string Information)
        {
            ViewBag.UserId = UserId;
            ViewBag.Balance = _db.Users.First(u => u.Id == UserId).Balance;
            ViewBag.purchaseDetail = Information;
            //purchase = null;
            
            List<Song> ownedSongs = _db.Collections.Include(c => c.User).Where(c => c.UserId == UserId).Select(c=>c.Song).ToList();
            List<Song> allSongs = _db.Songs.Include(s=>s.Artist).ToList();
            List<Song> result = allSongs.Where(s => ownedSongs.All(s2 => s2.Id != s.Id)).ToList();
            return View(result);
        }

        [HttpPost]
        public IActionResult Buy(int SongId,int UserId)
        {
            //ViewBag.tt = "tt";
            User u = _db.Users.First(u => u.Id == UserId);
            Song s = _db.Songs.First(s => s.Id == SongId);
            string Information;

            //else if (ViewData["purchase"] == true)
            //{
            //    ViewBag.purchaseDetail = $"Congratulations! You buy song successfully!";

            //}
            //else if (purchase == false)
            //{
            //    ViewBag.purchaseDetail = "Sorry! You do not have enough money to buy this song.";
            //}


            if (u.Balance < s.Price)
            {
                Information = "Sorry! You do not have enough money to buy this song.";
                
                //ViewData["purchase"] = false;
            }
            else
            {
                Information = $"Congratulations! You buy {s.Name} successfully!";
                //ViewData["purchase"] = true;
                u.Balance -= s.Price;
                _db.Orders.Add(new Order { UserId = u.Id, SongId = s.Id, Date = new DateTime() });
                _db.Collections.Add(new Collection { UserId = u.Id, SongId = s.Id });
                _db.SaveChanges();
                
            }
            return RedirectToAction("BuyPage",new { UserId = u.Id, Information = Information });
        }

        [HttpPost]
        public IActionResult Redeem(int UserId,int SongId)
        {
            string RedeemInformation;

            Collection c = _db.Collections.Include(c => c.Song).First(c => c.UserId == UserId && c.SongId == SongId);
            Order o = _db.Orders.First(o => o.UserId == UserId && o.SongId == SongId);
            if (false)
            {
                RedeemInformation = "Sorry. We can not redeem the purchase because it was a 30 days before order.";
            }
            else
            {
                _db.Collections.Remove(c);
                _db.Orders.Remove(o);
                _db.SaveChanges();
                RedeemInformation = $"Congratulations! You redeem the {c.Song.Name} successfully!";
            }            

            return RedirectToAction("Collection", new { UserId = c.UserId, RedeemInformation = RedeemInformation });
        }
    }
}
