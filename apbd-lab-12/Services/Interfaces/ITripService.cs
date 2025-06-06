using apbd_lab_12.DTOs;

namespace apbd_lab_12.Services.Interfaces;

public interface ITripService
{
    Task<List<TripDTO>> GetTripsAsync(int page, int pageSize);
    Task<bool> AssignClientToTripAsync(int idTrip, AssignClientDTO dto);
}
