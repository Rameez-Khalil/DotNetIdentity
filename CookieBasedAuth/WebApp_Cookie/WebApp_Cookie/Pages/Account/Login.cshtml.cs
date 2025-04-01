using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApp_Cookie.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty] //this binds the front and back of this page.
        public Credential Credential { get; set; } = new Credential();

        public void OnGet()
        {
        }

        public void OnPost()
        {

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
