using apbd_lab_12.DTOs;
using apbd_lab_12.Repositories.Interfaces;
using apbd_lab_12.Services.Interfaces;

namespace apbd_lab_12.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;

    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task<List<TripDTO>> GetTripsAsync(int page, int pageSize)
    {
        return await _tripRepository.GetTripsAsync(page, pageSize);
    }

    public async Task<bool> AssignClientToTripAsync(int idTrip, AssignClientDTO dto)
    {
        return await _tripRepository.AssignClientToTripAsync(idTrip, dto);
    }
}
