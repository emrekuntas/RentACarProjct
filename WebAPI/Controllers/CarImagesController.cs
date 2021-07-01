using Business.Abstract;
using Core.Utilities.Helpers;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        private readonly ICarImageService _carImageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileHelper _file;

        public CarImagesController(ICarImageService carImageService, IWebHostEnvironment webHostEnvironment, IFileHelper file)
        {
            _carImageService = carImageService;
            _webHostEnvironment = webHostEnvironment;
            _file = file;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _carImageService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
        [HttpPost("Add")]
        public IActionResult Add([FromForm] CarImages carImage,IFormFile file)
        {
            var imageResult = _file.Upload(file, _webHostEnvironment.WebRootPath + "\\uploads\\");
            if (imageResult.IsFaulted || imageResult.IsCanceled) return BadRequest(imageResult.Result.Message);
            carImage.ImagePath = imageResult.Result.Message;

            var result = _carImageService.Add(carImage);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("Update")]
        public IActionResult Update(CarImages carImage)
        {
            var result = _carImageService.Update(carImage);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
        [HttpPost("Delete")]
        public IActionResult Delete(CarImages carImage)
        {
            var result = _carImageService.Delete(carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
