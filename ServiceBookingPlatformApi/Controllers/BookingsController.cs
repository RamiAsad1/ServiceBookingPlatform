using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServiceBookingPlatformApi.DTOs.Bookings;
using ServiceBookingPlatformApi.DTOs.Users;
using ServiceBookingPlatformApi.Entities.Bookings;
using ServiceBookingPlatformApi.Entities.Users;
using ServiceBookingPlatformApi.Repositories;

namespace ServiceBookingPlatformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingsController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var Bookings = await _unitOfWork.Repository<Booking>().GetAllAsync();
            var BookingsDto = _mapper.Map<IEnumerable<BookingDto>>(Bookings);

            return Ok(BookingsDto);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var Booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(id);
            if (Booking == null)
            {
                return NotFound();
            }
            var BookingDto = _mapper.Map<BookingDto>(Booking);

            return Ok(BookingDto);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booking = _mapper.Map<Booking>(createDto);
            await _unitOfWork.Repository<Booking>().AddAsync(booking);
            await _unitOfWork.SaveAsync();
            var bookingDto = _mapper.Map<BookingDto>(booking);

            return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, bookingDto);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] UpdateBookingDto updateDto)
        {
            var existingBooking = await _unitOfWork.Repository<Booking>().GetByIdAsync(id);
            if (existingBooking == null)
                return NotFound();

            _mapper.Map(updateDto, existingBooking);

            _unitOfWork.Repository<Booking>().Update(existingBooking);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin,User")]
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
