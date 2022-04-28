﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace sentence.generator.api.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected string UserID => FindClaim(ClaimTypes.NameIdentifier);
        private string FindClaim(string claimName)
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(claimName);

            if (claim == null)
            {
                return null;
            }
            return claim.Value;
        }
    }
}
