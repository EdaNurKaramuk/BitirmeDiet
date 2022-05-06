using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static DietCalculatorSystem.WebConstants.AdminConstants;

namespace DietCalculatorSystem.Areas.Admin.Controllers
{
    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public abstract class AdminController : Controller
    {
    }
}
