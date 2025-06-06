using apbd_lab_12.DTOs;
using apbd_lab_12.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace apbd_lab_12.Controllers
{
    [ApiController]
    [Route("api/trips")]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        // GET /api/trips?page=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<List<TripDTO>>> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page and PageSize must be greater than 0.");

            var trips = await _tripService.GetTripsAsync(page, pageSize);
            return Ok(trips);
        }

        // POST /api/trips/{idTrip}/clients
        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] AssignClientDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _tripService.AssignClientToTripAsync(idTrip, dto);

            if (!success)
                return BadRequest("Assignment failed. Either the trip does not exist, it's in the past, or the client already exists/is assigned.");

            return Ok("Client assigned to trip successfully.");
        }
    }
}