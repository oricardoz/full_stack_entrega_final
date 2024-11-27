
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiFullStack.Controllers
{
    [Authorize(Roles = "admin,cliente")]
    public class ProdutosController : ControllerBase
    {
        // ...existing code...
    }
}