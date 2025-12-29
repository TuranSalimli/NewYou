using NewYou.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYou.Application.Abstraction.Services
{
    public interface ITokenService
    {
        string CreateToken(Account user);
    }
}
