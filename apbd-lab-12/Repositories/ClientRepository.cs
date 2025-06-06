using apbd_lab_12.Data;
using apbd_lab_12.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace apbd_lab_12.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> HasClientTripsAsync(int idClient)
    {
        return await _context.ClientTrips.AnyAsync(ct => ct.IdClient == idClient);
    }

    public async Task<bool> DeleteClientAsync(int idClient)
    {
        var client = await _context.Clients.FindAsync(idClient);
        if (client == null) return false;

        if (await HasClientTripsAsync(idClient))
            return false;

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return true;
    }
}