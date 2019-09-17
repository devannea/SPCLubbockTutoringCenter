using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SPCLCTAPI.Core.Services
{
    public interface IUserService
    {
        ClaimsPrincipal User { get; }
        string CurrentUserId { get; }
    }
}
