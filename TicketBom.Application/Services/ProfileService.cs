using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBom.Application.ViewModels;
using TicketBom.Domain.Entities.AccountAggregate;

namespace TicketBom.Application.Services
{
    public class ProfileService : IProfileService
    {

        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public Task<List<ProfileViewModel>> GetAll()
        {
            var users = _profileRepository.GetAll().Result;

            return Task.FromResult(users.Select(x => new ProfileViewModel
            {
                Id = x.Id,
                Description = x.Description
            }).ToList());
        }

   
    }

    public interface IProfileService
    {
        Task<List<ProfileViewModel>> GetAll();
    }
}
