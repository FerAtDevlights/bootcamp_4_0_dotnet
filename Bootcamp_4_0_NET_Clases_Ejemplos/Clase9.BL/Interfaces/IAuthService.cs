using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clase9.BL.Interfaces
{
    public interface IAuthService
    {
        string HashPassword(string plainPassword);
        bool VerifyPassword(string hashedPassword, string plainPassword);
        string GenerateJwtToken(int userId, string username, IList<string> roles);
    }
}
