using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace WebApp_Cookie.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty] //this binds the front and back of this page.
        public Credential Credential { get; set; } = new Credential();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page(); 

            //verify the credentials.
            if(Credential.Name=="admin" && Credential.Password == "password")
            {
                //create the security context.
                //generate the claims and add them to the identity.
                //add the primary identity to the principal object.

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@example.com")
                }; 

                var identity = new ClaimsIdentity(claims, "MyCookieAuthentication");

                var claimsPrincipal = new ClaimsPrincipal(identity);

                //encrypt and serialize
               await HttpContext.SignInAsync("MyCookieAuthentication", claimsPrincipal); //this talks to the IAuthenticatonService

               return Redirect("/Index"); 

            }

            return Page();
        }
    }


    public class Credential
    {
        [Required]
        [Display(Description ="User Name")]
        public string Name { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty; 
    }
}
