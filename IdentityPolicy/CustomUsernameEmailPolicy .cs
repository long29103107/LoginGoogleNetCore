using Microsoft.AspNetCore.Identity;

namespace LoginSocialNetCore.IdentityPolicy
{
    public class CustomUsernameEmailPolicy : UserValidator<AppUser>
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            IdentityResult result = await base.ValidateAsync(manager, user);
            List<IdentityError> errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

            if (user.UserName == "google")
            {
                errors.Add(new IdentityError
                {
                    Description = "Google cannot be used as a user name"
                });
            }

            return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }
    }
}
