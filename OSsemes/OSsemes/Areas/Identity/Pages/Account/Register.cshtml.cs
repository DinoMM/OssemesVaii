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

                ModelState.AddModelError(String.Empty, "Pou�it� email u� existuje!");       //ked vyjde ina chyba tak bude stale vypisovat toto
                
            }
            return Page();
        }

        public class InputModel
        {
            [Required(ErrorMessage = "Email mus� by� vyplnen�")]
            [EmailAddress]
            [MaxLength(256, ErrorMessage = "Maxim�lny po�et znakov v emaily je 256")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Heslo mus� by� vyplnen�")]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Heslo mus� obsahova� minim�lne 1 ve�k� znak, 1 mal� znak, 1 ��slicu")]
            [DataType(DataType.Password)]
            [MinLength(6, ErrorMessage = "Heslo mus� ma� d�ku minim�lne 6 znakov")]
            [MaxLength(64, ErrorMessage = "Heslo mus� ma� d�ku maxim�lne 6 znakov")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Opakovn� heslo mus� by� vyplnen� rovnako ako heslo")]
            [DataType(DataType.Password)]
            [MinLength(6, ErrorMessage = "Heslo mus� ma� d�ku minim�lne 6 znakov")]
            [MaxLength(64, ErrorMessage = "Heslo mus� ma� d�ku maxim�lne 64 znakov")]
            [Compare("Password", ErrorMessage = "Hesl� sa musia rovna�")]
            public string ConfPassword { get; set; }

            [Required(ErrorMessage = "Meno mus� by� vyplnen�")]
            [DataType(DataType.Text)]
            [RegularExpression(@"^[a-zA-Z��蝞��������ȍ���������]+$", ErrorMessage = "Meno mo�e obsahova� len val�dne znaky")]
            [MinLength(3, ErrorMessage = "Meno mus� ma� d�ku minim�lne 3 znakov")]
            [MaxLength(64, ErrorMessage = "Meno mus� ma� d�ku maxim�lne 64 znakov")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Priezvisko mus� by� vyplnen�")]
            [DataType(DataType.Text)]
            [RegularExpression(@"^[a-zA-Z��蝞��������ȍ���������]+$", ErrorMessage = "Priezvisko mo�e obsahova� len val�dne znaky")]
            [MinLength(3, ErrorMessage = "Priezvisko mus� ma� d�ku minim�lne 3 znakov")]
            [MaxLength(64, ErrorMessage = "Priezvisko mus� ma� d�ku maxim�lne 64 znakov")]
            public string Surname { get; set; }

            [Required(ErrorMessage = "Podmienky pou��vania musia byt schv�len�!")]
            [Range(typeof(bool), "true", "true", ErrorMessage = "Podmienky pou��vania musia byt schv�len�!")]
            public bool TermsCond { get; set; } = false;

        }
    }
}
