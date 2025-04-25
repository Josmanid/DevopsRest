using DevopsRest.Models;
using DevopsRest.RepositoryList;
using Microsoft.AspNetCore.Mvc;

namespace DevopsRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EasterController : ControllerBase
    {
        private readonly EggsRepository _eggsRepository;

        public EasterController(EggsRepository eggsRepositoryList) {
            _eggsRepository = eggsRepositoryList;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Egg>> GetEggs(
            [FromHeader] int? inputAmount,
            [FromQuery] int? inputLowPrice,
            [FromQuery] int? inputHighPrice,
            [FromQuery] string? inputName,
            [FromQuery] string? inputOrderBy) {
            List<Egg> result = _eggsRepository.GetAllEggs(
                amount: inputAmount,
                LowPrice: inputLowPrice,
                HighPrice: inputHighPrice,
                name: inputName,
                orderBy: inputOrderBy);

            if (result != null)
            {
                Response.Headers.Add("totalamount", result.Count.ToString());
                return Ok(result);
            }
            return NotFound();
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET api/<EasterController>/5
        /*[HttpGet("{id}")]:
        This tells the controller to look in the URL path for an id, like:
         * http://localhost:29156/api/xclass/1
         */
        [HttpGet("{id}")]
        public ActionResult<Egg> Get(int id) {
            Egg result = _eggsRepository.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        // POST api/<EasterController>
        [HttpPost]
        public ActionResult<Egg> Post([FromBody] Egg incomingEgg) {
            try
            {
              
                Egg result = _eggsRepository.AddEgg(incomingEgg);
                return Created("/" + result.Id, result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest("Der er ikke noget i dit objekt");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest("Dit objekt er for småt eller for stort");
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Dit objekt er ikke gyldigt");
            }

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // PUT api/<EasterController>/5
        [HttpPut("{id}")]
        public ActionResult<Egg> Put(int id, [FromBody] Egg value) {
            try
            {
                
                Egg result = _eggsRepository.UpdateEgg(id, value);
                if (result != null)
                {
                    return Ok(result);
                }
                else { return NotFound(); }

            }
            catch (ArgumentNullException ex)
            {
                return BadRequest("Der er ikke noget i dit objekt");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest("Dit objekt er for småt eller for stort");
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Dit objekt er ikke gyldigt");
            }


        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // DELETE api/<EasterController>/5
        [HttpDelete("{id}")]
        public ActionResult<Egg> Delete(int id) {
            Egg result = _eggsRepository.RemoveEgg(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
    
}

