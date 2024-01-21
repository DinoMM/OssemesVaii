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
        public string SuccRegistration { get; set; } = "";

        private readonly SignInManager<IdentityUserOwn> _signInManager;

        
        public LoginModel(SignInManager<IdentityUserOwn> signInManager)     //(pomohol som si z internetu tutori·ly/AI)
        {
            _signInManager = signInManager;
        }
        
        public void OnGet()
        {
            ReturnUrl = Url.Content("~/");
            var cookieValue = Request.Cookies["NewRegister"];       //ziskanie cookies o tom ci sa uzivatel uspesne zaregistroval (od AI)
            if (!String.IsNullOrEmpty(cookieValue))
            {
                if (cookieValue == "true")
                {
                    SuccRegistration = "Registr·cia prebehla ˙speöne";
                    Response.Cookies.Delete("NewRegister");     //vymazanie
                }
            }
        }
       
        public async Task<IActionResult> OnPostAsync()      //(pomohol som si z internetu tutori·ly/AI)
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
