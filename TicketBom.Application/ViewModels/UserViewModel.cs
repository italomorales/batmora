using System.Collections.Generic;

namespace TicketBom.Application.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<ProfileViewModel> Profiles { get; set; }
    }
}
