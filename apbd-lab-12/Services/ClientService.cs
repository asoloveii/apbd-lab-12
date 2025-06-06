using apbd_lab_12.Repositories.Interfaces;
using apbd_lab_12.Services.Interfaces;

namespace apbd_lab_12.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<bool> DeleteClientAsync(int idClient)
    {
        return await _clientRepository.DeleteClientAsync(idClient);
    }
}
