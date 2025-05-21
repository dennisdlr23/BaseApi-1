using System.Collections.Generic;
using GestionDocumental.WebApi.Features.Users.Entities;
using GestionDocumental.WebApi.Features.Common.Dto;

namespace DGestionDocumental.Features.Auth.Dto
{
    public class UserDto : User
    {
        public string CreatedByName { get; set; }
        public string Theme { get; set; }
        public string Role { get; set; }
        public List<Permission> Permissions { get; set; }
        public List<MenuDto> Menu { get; set; }
        public string Token { get; set; }
    }
}
