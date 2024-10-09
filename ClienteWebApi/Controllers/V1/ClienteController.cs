using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cliente.Web.Api.Controllers.V1;

[Authorize]
[Route("Api/V{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class ClienteController : ControllerBase
{


}
