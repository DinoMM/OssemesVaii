using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using OSsemes.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;


namespace OSsemes.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        private readonly SignInManager<IdentityUserOwn> _signInManager;


        public LoginModel(SignInManager<IdentityUserOwn> signInManager)
        {
            _signInManager = signInManager;
        }
        
        public void OnGet()
        {
            ReturnUrl = Url.Content("~/");
        }
       
        public async Task<IActionResult> OnPostAsync()
        {
            ReturnUrl = Url.Content("~/");
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, false);

                if (result.Succeeded)
                {
                    return LocalRedirect(ReturnUrl);
                }
                ModelState.AddModelError(String.Empty, "ZlÈ prihlasovacie ˙daje");
            }
            return Page();
        }

        public class InputModel
        {
            [Required(ErrorMessage = "Email musÌ byù vyplnen˝")]
            [EmailAddress]
            [MaxLength(320)]
            public string Email { get; set; }

          

            [Required(ErrorMessage = "Heslo musÌ byù vyplnenÈ")]
            [DataType(DataType.Password, ErrorMessage = "ZadanÈ ˙daje s˙ chybnÈ") ]
            [MaxLength(64)]
            public string Password { get; set; }

        }
    }
}
