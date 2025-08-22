using Microsoft.AspNetCore.Mvc;
using MovieRental.Movie;
using MovieRental.Rental;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {

        private readonly IRentalFeatures _features;

        public RentalController(IRentalFeatures features)
        {
            _features = features;
        }


        [HttpPost]
        public IActionResult Post([FromBody] Rental.Rental rental)
        {
	        return Ok(_features.Save(rental));
        }

        [HttpGet]
        public IActionResult Get(string customerName)
        {
            var rentals = _features.GetRentalsByCustomerName(customerName);
	        return Ok(rentals);
        }

	}
}
