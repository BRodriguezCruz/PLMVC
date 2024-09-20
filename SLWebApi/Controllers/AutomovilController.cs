using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SLWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutomovilController : ControllerBase
    {
        [HttpGet("getAutos")]   
        public IActionResult GetAutos()
        {
            ML.Result result = BL.Automovil.GetAll();

            return result.Correct == true ? Ok(result) : BadRequest(result); 
        }
    }
}
