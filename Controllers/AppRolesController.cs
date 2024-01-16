using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AarogyaSaathi.Dto;
using AarogyaSaathi.Data;

namespace AarogyaSaathi.Controllers
{
   [Authorize(Roles ="Admin")]
    public class AppRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _appDbContext;

        public AppRolesController(RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            _roleManager = roleManager;
            _appDbContext=dbContext;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleStore roleStore)
        {
            var alreadyAdded = await _roleManager.RoleExistsAsync(roleStore.Role);

            if (!alreadyAdded)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleStore.Role));
                return RedirectToAction("List");
            }
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult List()
        {
            var roles = _roleManager.Roles; // Get all roles
            var rolesDto = new List<RoleStore>();

            foreach (var role in roles)
            {
                rolesDto.Add(new RoleStore
                {
                    Role = role.Name

                });
            }

            return View(rolesDto);
        }

        [HttpGet]
        public IActionResult GetPatients() {
            var patients = _appDbContext.PatientData;
            return View(patients);
        }

    }
}
