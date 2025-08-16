using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotBudget.Data.Services
{
    public class ProfileService : IProfileService
    {
        public readonly ApplicationDbContext context;
        public ProfileService(ApplicationDbContext dbContext) {
            context = dbContext;
        }
        public Profile GetProfileById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Profile ID cannot be empty", nameof(id));
            }

            var profile = context.Profiles
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(p => p.Id == id);

            return profile ?? throw new KeyNotFoundException($"Profile with ID {id} not found");
        }
        public Profile AddProfile(Profile profile)
        {
            if (profile == null)
                throw new ArgumentNullException(nameof(profile));

            try
            {
                context.Profiles.Add(profile);
                context.SaveChanges(); 
                return profile;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Exception in saving yo profile cuh: ", ex);
                return profile;
            }
        }
        public void DeleteProfile(string id) {
            if (string.IsNullOrWhiteSpace(id)) {
                throw new ArgumentException("Profile ID cannot be empty", nameof(id));

                var profile = context.Profiles.Find(id);
                if (profile == null) {
                    throw new KeyNotFoundException($"Profile with ID {id} not found!");
                }
                context.Profiles.Remove(profile);
                context.SaveChanges();
            }
        }

        public Profile UpdateProfile(Profile updatedProfile) {
            context.Profiles.Update(updatedProfile);
            context.SaveChanges();
            return updatedProfile;
        }
    }
}
