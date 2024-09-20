using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutosController : ControllerBase
    {
        [HttpGet("getListaAutos")]
        public IActionResult GetAutos()
        {
            ML.Result result = BL.Automovil.GetAll();

            return result.Correct == true ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getAuto/{idAuto}")]
        public IActionResult GetById([FromRoute, Required] int idAuto)
        {
            ML.Result result = BL.Automovil.GetById(idAuto);

            return result.Correct ? Ok(result) : BadRequest(result);
        }

        [HttpPost("agregarAuto")]
        public IActionResult AddAuto([FromBody] ML.Automovil auto)
        {
            ML.Result result = BL.Automovil.AddAuto(auto);

            return result.Correct != true ? BadRequest(result) : Ok(result);
        }

        [HttpPut("updateAuto")]
        public IActionResult UpdateAuto([FromBody] ML.Automovil auto)
        {
            ML.Result result = BL.Automovil.UpdateAuto(auto);

            return result.Correct != true ? BadRequest(result) : Ok(result);
        }

        [HttpDelete("deleteAuto/{idAuto}")]
        public IActionResult DeleteAuto([FromRoute] int idAuto)
        {
            ML.Result result = BL.Automovil.DeleteAuto(idAuto);

            return result.Correct != true ? BadRequest(result) : Ok(result);
        }
    }
}
