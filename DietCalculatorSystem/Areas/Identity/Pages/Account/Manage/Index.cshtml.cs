using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DietCalculatorSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using static DietCalculatorSystem.Data.DataConstants.User;

namespace DietCalculatorSystem.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [StringLength(MaxFullNameLength, ErrorMessage = "{0} must be at least {2} and at max {1} characters long.", MinimumLength = MinFullNameLength)]
            [Display(Name = "Kullanıcı Adı")]
            public string NewUsername { get; set; }

            [Phone]
            [Display(Name = "Telefon Numarası")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            var userName = await userManager.GetUserNameAsync(user);
            var phoneNumber = await userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                NewUsername = userName,
                PhoneNumber = phoneNumber,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }


            var username = await userManager.GetUserNameAsync(user);
            var phoneNumber = await userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber || Input.NewUsername != username)
            {
                var setPhoneResult = await userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                var setUsernameResult = await userManager.SetUserNameAsync(user, Input.NewUsername);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Telefon numarası ayarlanırken beklenmedik bir hata oluştu.";
                    return RedirectToPage();
                }
                else if (!setUsernameResult.Succeeded)
                {
                    StatusMessage = "Kullanıcı adı ayarlanırken beklenmedik bir hata oluştu.";
                    return RedirectToPage();
                }
            }

            await signInManager.RefreshSignInAsync(user);
            StatusMessage = "Profiliniz güncellendi";
            return RedirectToPage();
        }
    }
}
