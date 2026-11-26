
using Microsoft.AspNetCore.Identity;

namespace SNDClinico.Web.Data;

public class SndIdentityUser : IdentityUser
{
    public string? NomeCompleto { get; set; }
}
