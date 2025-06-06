namespace apbd_lab_12.Repositories.Interfaces;

public interface IClientRepository
{
    Task<bool> HasClientTripsAsync(int idClient);
    Task<bool> DeleteClientAsync(int idClient);
}