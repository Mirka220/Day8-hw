using Day8_hw.Context;
using Day8_hw.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day8_hw.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly CarContext _dbConext;
        public CarController(ILogger<CarController> logger, CarContext car)
        {
            _dbConext = car;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> Get()
        {
            return await _dbConext.Cars.Select(x => CarOut(x)).ToListAsync();
        }

        [HttpGet("Brand")]
        public async Task<ActionResult<Car>> GetFindBrand(Guid Id)
        {
            var FindTemp = await _dbConext.Cars.FindAsync(Id);
            if (FindTemp == null)
            {
                return NotFound();
            }

            return CarOut(FindTemp);
        }

        [HttpPost(Name = "PostCar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(Car car)
        {
            _dbConext.Cars.Add(car);
            await _dbConext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new {
                Brand = car.Brand,
                Color = car.Color,
                YearOfProduction = car.YearOfProduction,
                Price = car.Price,
                BodyType = car.BodyType,
                EngineVolume = car.EngineVolume,
                IsClearedInKazakhstan = car.IsClearedInKazakhstan,
                Comment = car.Comment,
            }, car);
        }

        [HttpDelete("Id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var findDeleteObject = await _dbConext.Cars.FindAsync(Id);
            if (findDeleteObject == null)
            {
                return NotFound();
            }
            _dbConext.Cars.Remove(findDeleteObject);
            await _dbConext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("Id")]
        public async Task<ActionResult> Put(Guid Id, Car car)
        {
            if (Id == car.Id)
            {

                var carObject = _dbConext.Cars.FindAsync(Id);

                try
                {
                    if (await carObject != null)
                    {
                        _dbConext.Entry(carObject).State = EntityState.Modified;
                        await _dbConext.SaveChangesAsync();
                    }
                    else
                    {
                        _dbConext.Cars.Add(car);
                    }

                    await _dbConext.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (Id != car.Id)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }

                }

            }
            else
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPatch("Id")]
        public async Task<ActionResult> Patch(Guid Id, Car car)
        {
            var carID = await _dbConext.Cars.SingleAsync(x => x.Id == Id);

            carID.Id = car.Id;
            carID.Brand = car.Brand;
            carID.Color = car.Color;
            carID.YearOfProduction = car.YearOfProduction;
            carID.Price = car.Price;
            carID.BodyType = car.BodyType;
            carID.EngineVolume = car.EngineVolume;
            carID.IsClearedInKazakhstan = car.IsClearedInKazakhstan;
            carID.Comment = car.Comment;

            await _dbConext.SaveChangesAsync();

            return NoContent();
        }

        private static Car CarOut(Car car) => new Car
        {
            Id = car.Id,
            Brand = car.Brand,
            Color = car.Color,
            YearOfProduction = car.YearOfProduction,
            Price = car.Price,
            BodyType = car.BodyType,
            EngineVolume = car.EngineVolume,
            IsClearedInKazakhstan = car.IsClearedInKazakhstan,
            Comment = car.Comment,
        };
    }
}
