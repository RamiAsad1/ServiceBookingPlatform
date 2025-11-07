using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBookingPlatformApi.Entities.Bookings;
using ServiceBookingPlatformApi.Repositories;

namespace ServiceBookingPlatformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public BookingsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var Bookings = await _unitOfWork.Repository<Booking>().GetAllAsync();
            return Ok(Bookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var Booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(id);
            if (Booking == null)
            {
                return NotFound();
            }
            return Ok(Booking);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] Booking Booking)
        {
            await _unitOfWork.Repository<Booking>().AddAsync(Booking);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetBookingById), new { id = Booking.Id }, Booking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking Booking)
        {
            if (id != Booking.Id)
            {
                return BadRequest();
            }
            _unitOfWork.Repository<Booking>().Update(Booking);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var Booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(id);
            if (Booking == null)
            {
                return NotFound();
            }
            _unitOfWork.Repository<Booking>().Delete(Booking);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
