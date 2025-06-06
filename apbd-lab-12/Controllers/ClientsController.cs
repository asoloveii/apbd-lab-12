using apbd_lab_12.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace apbd_lab_12.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClient(int idClient)
        {
            var deleted = await _clientService.DeleteClientAsync(idClient);

            if (!deleted)
            {
                return BadRequest("Client cannot be deleted because they are assigned to one or more trips.");
            }

            return NoContent(); // 204
        }
    }
}