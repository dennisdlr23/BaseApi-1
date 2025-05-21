using System;
using GestionDocumental.WebApi.Features.Common.Entities;
using GestionDocumental.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace GestionDocumental.WebApi.Features.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly CommonService _commonService;
        public CommonController(CommonService commonService)
        {
            _commonService = commonService;
        }      
     

    }
}
