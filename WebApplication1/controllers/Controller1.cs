using System.Net;
using System.Net.Mime;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.controllers;

[ApiController]
[Route("[controller]/[action]")]
public class Controller1 : Controller
{
    [HttpPost("POST")]
    public IActionResult POST(IFormFile file)
    {
        Stream test = System.IO.File.Open("test", FileMode.OpenOrCreate);
        test.Close();
        MemoryStream memoryStream = new MemoryStream();
        file.CopyToAsync(memoryStream);
        byte[] array = memoryStream.ToArray();

        using (var contet = new ItbaseContext())
        {
            Image image = new Image()
            {
                Identifier = Guid.NewGuid().ToString("N"),
                Imagebase64 = array
            };
            contet.Images.Add(image);
            contet.SaveChanges();
        }

        return Ok("Изображение сохранено");
    }

    [HttpGet("getImage")]
    public IActionResult? getImage(int id)
    {
        using (var context = new ItbaseContext())
        {
            var imagebase64 = context.Images.FirstOrDefault(x => x.Id == id)?.Imagebase64;
            if (imagebase64 != null)
            {
                byte[] imgData = imagebase64;
                return File(imgData, "image/png");
            }
            else
            {
                return Ok("Изображение не найдено");
            }
        }
    }
}