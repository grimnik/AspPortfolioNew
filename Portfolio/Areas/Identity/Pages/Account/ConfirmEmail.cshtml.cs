using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Portfolio.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _IdentityUserManager;

        public ConfirmEmailModel(UserManager<IdentityUser> IdentityUserManager)
        {
            _IdentityUserManager = IdentityUserManager;
        }

        public async Task<IActionResult> OnGetAsync(string IdentityUserId, string code)
        {
            if (IdentityUserId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var IdentityUser = await _IdentityUserManager.FindByIdAsync(IdentityUserId);
            if (IdentityUser == null)
            {
                return NotFound($"Unable to load IdentityUser with ID '{IdentityUserId}'.");
            }

            var result = await _IdentityUserManager.ConfirmEmailAsync(IdentityUser, code);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for IdentityUser with ID '{IdentityUserId}':");
            }

            return Page();
        }
    }
}
