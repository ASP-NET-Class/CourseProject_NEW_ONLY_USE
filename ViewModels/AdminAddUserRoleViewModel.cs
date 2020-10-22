using CourseProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.ViewModels
{
    public class AdminAddUserRoleViewModel
    {
        public ApplicationUser User { get; set; }
        public string Role { get; set; }
        public SelectList RoleList { get; set; }
    }
}
