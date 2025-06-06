using apbd_lab_12.Data;
using apbd_lab_12.DTOs;
using apbd_lab_12.Models;
using apbd_lab_12.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace apbd_lab_12.Repositories;

public class TripRepository : ITripRepository
{
    private readonly AppDbContext _context;

    public TripRepository(AppDbContext context)
    {
        _context = context;
    }

    
    public async Task<List<TripDTO>> GetTripsAsync(int page, int pageSize)
    {
        return await _context.Trips
            .Include(t => t.TripCountries).ThenInclude(tc => tc.Country)
            .Include(t => t.ClientTrips).ThenInclude(ct => ct.Client)
            .OrderByDescending(t => t.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TripDTO
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.TripCountries.Select(tc => tc.Country.Name).ToList(),
                Clients = t.ClientTrips.Select(ct => new ClientDTO
                {
                    FirstName = ct.Client.FirstName,
                    LastName = ct.Client.LastName
                }).ToList()
            })
            .ToListAsync();
    }
    
    public async Task<bool> AssignClientToTripAsync(int idTrip, AssignClientDTO dto)
    {
        var trip = await _context.Trips.FindAsync(idTrip);
        if (trip == null || trip.DateFrom < DateTime.Now)
            return false;

        var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == dto.Pesel);
        if (existingClient != null)
            return false;

        var existingAssignment = await _context.ClientTrips.AnyAsync(ct => ct.Client.Pesel == dto.Pesel && ct.IdTrip == idTrip);
        if (existingAssignment)
            return false;

        var newClient = new Client
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Telephone = dto.Telephone,
            Pesel = dto.Pesel
        };

        _context.Clients.Add(newClient);
        await _context.SaveChangesAsync();

        var newClientTrip = new ClientTrip
        {
            IdClient = newClient.IdClient,
            IdTrip = idTrip,
            RegisteredAt = DateTime.UtcNow,
            PaymentDate = dto.PaymentDate
        };

        _context.ClientTrips.Add(newClientTrip);
        await _context.SaveChangesAsync();
        return true;
    }
}