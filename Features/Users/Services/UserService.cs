using System;
using System.Collections.Generic;
using System.Linq;
using DGestionDocumental.Features.Auth.Dto;
using GestionDocumental.WebApi.Features.Users.Entities;
using GestionDocumental.WebApi.Features.Common.Entities;
using GestionDocumental.WebApi.Infraestructure;
using Microsoft.Extensions.Configuration;
using GestionDocumental.WebApi.Helpers;



namespace GestionDocumental.WebApi.Features.Users
{
    public class UserService
    {
        private readonly GestionDocumentalDbContext _GestionDocumentalDbContext;
        private readonly IConfiguration _configuration;
   


        public UserService(GestionDocumentalDbContext GestionDocumentalDbContext, IConfiguration configuration)
        {
            _GestionDocumentalDbContext = GestionDocumentalDbContext;
            _configuration = configuration;
          
  
        }

        public List<UserDto> Get()
        {
            var users = _GestionDocumentalDbContext.User.ToList();
            var themes = _GestionDocumentalDbContext.Theme.ToList();
            var roles = _GestionDocumentalDbContext.Role.ToList();

            var result = (from u in users
                          join r in roles on u.RoleId equals r.RoleId into userRole
                          from r in userRole.DefaultIfEmpty()
                          join t in themes on u.ThemeId equals t.ThemeId into themeUser
                          from t in themeUser.DefaultIfEmpty()
                          select new UserDto
                          {
                              Active = u.Active,
                              Email = u.Email,
                              Name = u.Name,
                              Password = null,
                              RoleId = u.RoleId,
                              ThemeId = u.ThemeId,
                              UserId = u.UserId,
                              UserName = u.UserName,
                              Role = r?.Description ??"ROL NO ASIGNADO",
                              Theme = t?.Description ?? "TEMA NO ASIGNADO"
                          }
                          ).ToList();
            return result;
        }

        public List<UserDto> Add(User user)
        {
            user.IsValid();
            if (string.IsNullOrEmpty(user.Password)) throw new Exception("Debe ingresar una contraseña");
            if (user.Password.Length <8) throw new Exception("Debe ingresar una contraseña que contenga al menos 8 caracteres");
            user.Active = true;
            user.Password = Helper.EncryptPassword(user.Password.Trim(), _configuration);
            user.UserName = user.UserName.Trim().ToLower();
            _GestionDocumentalDbContext.User.Add(user);
            _GestionDocumentalDbContext.SaveChanges();
            return Get();
        }


        public List<UserDto> Edit(User user)
        {
            user.IsValid();
            if (!string.IsNullOrEmpty(user.Password))
            {
                if (user.Password.Length < 8) throw new Exception("Debe ingresar una contraseña que contenga al menos 8 caracteres");
                user.Password = Helper.EncryptPassword(user.Password.Trim(), _configuration);
            }
            var currentUser = _GestionDocumentalDbContext.User.Where(x => x.UserId == user.UserId).FirstOrDefault();
            currentUser.Name = user.Name;
            currentUser.Email = user.Email;
            currentUser.RoleId = user.RoleId;
            currentUser.ThemeId = user.ThemeId;
            currentUser.Active = user.Active;

            _GestionDocumentalDbContext.User.Update(currentUser);
            currentUser.Password = user.Password;
            _GestionDocumentalDbContext.SaveChanges();
            return Get();
        }

        public List<Theme> GetThemes()
        {
            var themes = _GestionDocumentalDbContext.Theme.ToList();
            return themes;
        }
    }
}
