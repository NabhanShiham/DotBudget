using Microsoft.AspNetCore.Mvc;

namespace DotBudget.Data.Services
{
    public interface IProfileService
    {
        public abstract Profile GetProfileById(String id);
        public abstract Profile AddProfile(Profile profile);
        public abstract void DeleteProfile(String id);
    }
}
