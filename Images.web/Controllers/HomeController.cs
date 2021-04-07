using Images.web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Images.data;
namespace Images.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        public HomeController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

   
        public IActionResult Index()
        {
           
            return View();
        }
        public IActionResult GetAllImages()
        {
            var connectionString = _configuration.GetConnectionString("ConnectionString");
            var repository = new ImagesRepository(connectionString);
            var images = repository.GetAllImages();
            return Json(images);
        }
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(string title, IFormFile imageFile)
        {
            var fileName = CreateImageAndStore(imageFile);
            var connectionString = _configuration.GetConnectionString("ConnectionString");
            var repository = new ImagesRepository(connectionString);
            repository.AddImage(title, fileName);
            return RedirectToAction("Index");
        }
        
        public IActionResult ViewImage(int id)
        {
            var cookieValue = Request.Cookies[$"{id}"];
            bool hasBeenHere = cookieValue != null;
            SingleImageViewModel vm = new SingleImageViewModel();
            if (hasBeenHere)
            {
                vm.HasBeenHere = "disabled";
            }
            else
            {
                vm.HasBeenHere = "";
            }
            if (!hasBeenHere)
            {
                Response.Cookies.Append($"{id}", $"{id}");
            }
            var connectionString = _configuration.GetConnectionString("ConnectionString");
            var repository = new ImagesRepository(connectionString);
            var image = repository.GetById(id);
            vm.Image = image;
            return View(vm);
        }
        [HttpPost]
        public IActionResult AddLike(int id)
        {
            var connectionString = _configuration.GetConnectionString("ConnectionString");
            var repository = new ImagesRepository(connectionString);
            repository.AddLike(id);
            return Json(id);
        }
        public IActionResult GetLikes(int id)
        {
            var connectionString = _configuration.GetConnectionString("ConnectionString");
            var repository = new ImagesRepository(connectionString);
            var image = repository.GetById(id);
            return Json(image.Likes);
        }
        private string CreateImageAndStore(IFormFile imageFile)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            string fullPath = Path.Combine(_environment.WebRootPath, "Uploads", fileName);
            using (FileStream stream = new FileStream(fullPath, FileMode.CreateNew))
            {
                imageFile.CopyTo(stream);
            }
            return fileName; 
        }
    }
}
