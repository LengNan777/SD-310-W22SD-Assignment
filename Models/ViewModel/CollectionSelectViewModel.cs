using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment.Models.ViewModel
{
    public class CollectionSelectViewModel
    {
        public Collection SelectedCollection { get; set; }
        public List<SelectListItem> CollectionSelectItems { get; set; }

        public CollectionSelectViewModel(ICollection<Collection> collections)
        {
            CollectionSelectItems = new List<SelectListItem>();
            foreach (Collection c in collections)
            {
                CollectionSelectItems.Add(new SelectListItem(c.User.Name.ToString(), c.Id.ToString()));
                //new SelectListItem() { Text = a.Name, Value = a.Id.ToString() }
                //+c.Id.ToString()
            }
        }
    }
}
