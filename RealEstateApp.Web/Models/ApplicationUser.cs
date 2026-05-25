using Microsoft.AspNetCore.Identity;

namespace RealEstateApp.Web.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    public Agency? Agency { get; set; }
}
