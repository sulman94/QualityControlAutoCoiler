using Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectX.Models;
using Services.Interfaces;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProjectX.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAutoCoiler _autoCoiler;
        private readonly IMachines _machines;
        private readonly ISizeCategory _sizeCategory;
        private readonly IAdmin _admin;
        private readonly IColors _colors;
        private readonly ILoggerManager _logger;
        
        public HomeController(IAutoCoiler autoCoiler, ILoggerManager logger, IMachines machines, ISizeCategory sizeCategory, IColors colors, IAdmin admin)
        {
            _autoCoiler = autoCoiler;
            _logger = logger;
            _admin = admin;
            _machines = machines;
            _colors = colors;
            _sizeCategory = sizeCategory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Unauthorize()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task SetDropdowns()
        {
            var machines = await _machines.MachineDropdownCall();
            var machineslist = new SelectList(machines.Data, "Id", "Value");

            var sizes = await _sizeCategory.SizeCategoryDropdownCall();
            var sizeslist = new SelectList(sizes.Data, "Id", "Value");

            var colors = await _colors.ColorDropdownCall();
            var colorslist = new SelectList(colors.Data, "Id", "Value");

            var operators = await _admin.UsersDropdownCall();
            var operatorslist = new SelectList(operators.Data, "Id", "Value");


            ViewBag.allMachines = machineslist;
            ViewBag.allColors = colorslist;
            ViewBag.allSizes = sizeslist;
            ViewBag.allOperators = operatorslist;
        }
    }
}
