using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToolAppStudentsServer.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class JWTControllers : ControllerBase
    {

    }
}
