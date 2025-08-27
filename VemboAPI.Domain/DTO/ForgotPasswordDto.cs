using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VemboAPI.Domain.DTO
{
    public class ForgotPasswordDto
    {
        public string? Email { get; set; }
        public string? ClientUri { get; set; }
    }
}
