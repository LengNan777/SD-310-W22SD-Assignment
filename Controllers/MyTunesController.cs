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

        [HttpPost]
        public IActionResult Like(int Id)
        {
            Song likedSong = _db.Songs.First(s => s.Id == Id);
            likedSong.Likes++;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Collection()
        {
            CollectionSelectViewModel vm = new CollectionSelectViewModel(_db.Collections.Include(c=>c.User).ToList());
            //return View(_db.Collections.Include(c => c.User).ToList());

            return View(vm);
        }
    }
}
