using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SPCLCTAPI.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _context;

        public UserService(IHttpContextAccessor httpContext)
        {
            _context = httpContext;
        }

        public ClaimsPrincipal User
        {
            get
            {
                return _context.HttpContext.User;
            }
        }

        public string CurrentUserId
        {
            get
            {
                return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        }
    }
}
