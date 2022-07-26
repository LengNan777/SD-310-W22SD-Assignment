using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment.Models.ViewModel
{
    public class UserSelectViewModel
    {
        public User SelectedUser { get; set; }
        public List<SelectListItem> UserSelectItems { get; set; }

        public UserSelectViewModel(ICollection<User> Users)
        {
            UserSelectItems = new List<SelectListItem>();
            foreach (User u in Users)
            {
                UserSelectItems.Add(new SelectListItem(u.Name.ToString(), u.Id.ToString()));
                //new SelectListItem() { Text = a.Name, Value = a.Id.ToString() }
                //+c.Id.ToString()
            }
        }
    }
}
