namespace apbd_lab_12.Services.Interfaces;

public interface IClientService
{
    Task<bool> DeleteClientAsync(int idClient);
}
