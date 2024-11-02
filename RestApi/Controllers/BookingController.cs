using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi.Data;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly APIContext _context;
        public BookingController(APIContext context)
        {
            _context = context;
        }


        [HttpPost]
        public JsonResult CreateEdit(Booking booking)
        {
            if (booking.Id == 0)
            {
                _context.Bookings.Add(booking);

            }
            else
            {
                var bookingInDB = _context.Bookings.FirstOrDefault(x => x.Id == booking.Id);

                if (bookingInDB == null)
                {
                    return new JsonResult(NotFound());
                }

                bookingInDB = booking;
            }
            _context.SaveChanges();
            return new JsonResult(Ok(booking));
        }

        [HttpGet]
        public JsonResult GetOneBook(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(x => x.Id == id);

            if (booking == null)
                return new JsonResult(NotFound());


            return new JsonResult(Ok(booking));
        }

        [HttpPut]
        public JsonResult createBooking(Booking booking)
        {
            var bookInDb = _context.Bookings.FirstOrDefault(x => x.Id == booking.Id);

            if (bookInDb == null)
            {
                _context.Bookings.Add(booking);
                return new JsonResult(Ok(booking));
            }
            else
            {
                return new JsonResult(NoContent());
            }

        }

        [HttpDelete]
        public JsonResult deleteBook(int id)
        {
            var bookInDb = _context.Bookings.FirstOrDefault(x => x.Id == id);

            if (bookInDb == null)
            {
                return new JsonResult(NotFound());
            }
            else
            {
                _context.Bookings.Remove(bookInDb);
                return new JsonResult(Ok(bookInDb));
            }
        }
    }
}
