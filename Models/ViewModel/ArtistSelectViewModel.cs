using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment.Models.ViewModel
{
    public class ArtistSelectViewModel
    {
        public Artist SelectedArtist { get; set; }
        public List<SelectListItem> ArtistSelectItems { get; set; }

        public ArtistSelectViewModel(ICollection<Artist> artists)
        {
            ArtistSelectItems = new List<SelectListItem>();
            foreach (Artist a in artists)
            {
                ArtistSelectItems.Add(new SelectListItem(a.Name, a.Id.ToString())) ;
                //new SelectListItem() { Text = a.Name, Value = a.Id.ToString() }
            }
        }
    }
}
