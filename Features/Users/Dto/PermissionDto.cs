using System;
using GestionDocumental.WebApi.Features.Users.Entities;

namespace GestionDocumental.WebApi.Features.Users.Dto
{
    public class PermissionDto: Permission
    {
        public int RoleId { get; set; }
    }
}
