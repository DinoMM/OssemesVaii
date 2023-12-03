using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OSsemes.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace OSsemes.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        private readonly SignInManager<IdentityUserOwn> _signInManager;
        private readonly UserManager<IdentityUserOwn> _userManager;
        
        public RegisterModel(SignInManager<IdentityUserOwn> signInManager, UserManager<IdentityUserOwn> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void OnGet()
        {
            ReturnUrl = Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ReturnUrl = Url.Content("/Identity/Account/Login");
            if (ModelState.IsValid)
            {
                var identity = new IdentityUserOwn { UserName = Input.Email, Name = Input.Name, Surname = Input.Surname, Email = Input.Email };
                var result = await _userManager.CreateAsync(identity, Input.Password);      //posle post s identitou usera a spolu s zahashovanym heslom
                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(identity, isPersistent: false);        // nastavi cookies
                    await _userManager.AddToRoleAsync(identity, "Guest");                 //nastavi default rolu
                    return LocalRedirect(ReturnUrl);
                }

                ModelState.AddModelError(String.Empty, "Pouitı email u existuje!");       //ked vyjde ina chyba tak bude stale vypisovat toto
                
            }
            return Page();
        }

        public class InputModel
        {
            [Required(ErrorMessage = "Email musí by vyplnenı")]
            [EmailAddress]
            [MaxLength(256, ErrorMessage = "Maximálny poèet znakov v emaily je 256")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Heslo musí by vyplnené")]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Heslo musí obsahova minimálne 1 ve¾kı znak, 1 malı znak, 1 èíslicu")]
            [DataType(DataType.Password)]
            [MinLength(6, ErrorMessage = "Heslo musí ma dåku minimálne 6 znakov")]
            [MaxLength(64, ErrorMessage = "Heslo musí ma dåku maximálne 6 znakov")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Opakovné heslo musí by vyplnené rovnako ako heslo")]
            [DataType(DataType.Password)]
            [MinLength(6, ErrorMessage = "Heslo musí ma dåku minimálne 6 znakov")]
            [MaxLength(64, ErrorMessage = "Heslo musí ma dåku maximálne 64 znakov")]
            [Compare("Password", ErrorMessage = "Heslá sa musia rovna")]
            public string ConfPassword { get; set; }

            [Required(ErrorMessage = "Meno musí by vyplnené")]
            [DataType(DataType.Text)]
            [RegularExpression(@"^[a-zA-Z¾šèıáíéúôòó¼ŠÈİÁÍÉÚÔÒÓ]+$", ErrorMessage = "Meno moe obsahova len valídne znaky")]
            [MinLength(3, ErrorMessage = "Meno musí ma dåku minimálne 3 znakov")]
            [MaxLength(64, ErrorMessage = "Meno musí ma dåku maximálne 64 znakov")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Priezvisko musí by vyplnené")]
            [DataType(DataType.Text)]
            [RegularExpression(@"^[a-zA-Z¾šèıáíéúôòó¼ŠÈİÁÍÉÚÔÒÓ]+$", ErrorMessage = "Priezvisko moe obsahova len valídne znaky")]
            [MinLength(3, ErrorMessage = "Priezvisko musí ma dåku minimálne 3 znakov")]
            [MaxLength(64, ErrorMessage = "Priezvisko musí ma dåku maximálne 64 znakov")]
            public string Surname { get; set; }

            [Required(ErrorMessage = "Podmienky pouívania musia byt schválené!")]
            [Range(typeof(bool), "true", "true", ErrorMessage = "Podmienky pouívania musia byt schválené!")]
            public bool TermsCond { get; set; } = false;

        }
    }
}
