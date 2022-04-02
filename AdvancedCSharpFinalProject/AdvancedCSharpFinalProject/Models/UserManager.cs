﻿using AdvancedCSharpFinalProject.Data;
using Microsoft.AspNetCore.Identity;

namespace AdvancedCSharpFinalProject.Models
{
    public class UserManager // we don't need this Class in our database
    {
        public ApplicationDbContext _db { get; set; }
        public UserManager<ApplicationUser> _userManager { get; set; }
        public RoleManager<IdentityRole> _roleManager { get; set; }
        public ApplicationUser User { get; set; }
        public List<string> GetAllRolesOfUser(string userId)
        {
            User = _db.Users.FirstOrDefault(user => user.Id == userId); 
            List<string> allRoleIdsOfUser = _db.UserRoles.Where(userRole => userRole.UserId == userId).Select(userRole => userRole.RoleId).ToList();
            List<string> roleNames = _db.Roles.Where(roles => allRoleIdsOfUser.Contains(roles.Id)).Select(roles => roles.Name).ToList();
            return roleNames;
        }
        public async Task<string> AssignRoleToUser(string userId, string roleForUser, double budgetOrSalary) //does not return anything
        {
            string message= "";
            ApplicationUser user = await _userManager.FindByIdAsync(userId); // pull the user out of the database

            if (await _roleManager.RoleExistsAsync(roleForUser)) // check if role is in the database
            {
                if (roleForUser == "Project Manager")
                {
                    ProjectManager projectManager = new ProjectManager(user);
                    _db.Users.Add(projectManager);
                    if (projectManager.DailySalary != 0)
                    {
                        await _userManager.AddToRoleAsync(projectManager, "Developer");
                    }
                    await _userManager.DeleteAsync(user);
                    projectManager.Budget = budgetOrSalary;
                    await _userManager.AddToRoleAsync(projectManager, roleForUser);
                    message = $"{projectManager.UserName} is added to the role {roleForUser}";
                    return message;
                }
                if (roleForUser == "Developer")
                {
                    Developer developer = new Developer(user);
                    _db.Users.Add(developer);
                    if (developer.Budget != 0)
                    {
                        await _userManager.AddToRoleAsync(developer, "Project Manager");
                    }
                    await _userManager.DeleteAsync(user);
                    developer.DailySalary = budgetOrSalary;
                    await _userManager.AddToRoleAsync(developer, roleForUser);
                    message = $"{developer.UserName} is added to the role {roleForUser}";
                    return message;
                }
                return message;
            }
            else
            {
                message = $"No role found";
                return message;
            }

        }
        public async Task<string> CheckIfAUserIsInARole(string userId, string roleToCheck)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            User = user;
            string message;
            if (await _roleManager.RoleExistsAsync(roleToCheck))
            {
                if (await _userManager.IsInRoleAsync(user, roleToCheck))
                {
                    message = $"{user.UserName} is in the {roleToCheck} role.";
                    return message;
                } else
                {
                    message = $"{user.UserName} is NOT in the {roleToCheck} role.";
                    return message;
                }
            }
            else
            {
                message = $"{roleToCheck} role Not Found";
                return message;
            }
        }
        public UserManager()
        {

        }
        public UserManager(ApplicationDbContext Db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = Db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
    }
}

//In OOP, you can't cast an instance of a parent class into a child class.
//You can only cast a child instance into a parent that it inherits from.