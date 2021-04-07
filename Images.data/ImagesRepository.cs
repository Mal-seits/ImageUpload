using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Images.data
{
    public class ImagesRepository
    {
        private readonly string _connectionString;

        public ImagesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddImage (string title, string fileName)
        {
            using var context = new ImagesDbContext(_connectionString);
            context.Images.Add(new Image
            {
                Title = title,
                Date = DateTime.Now,
                Likes = 0,
                FileName = fileName
            });
            context.SaveChanges();
        }
        public List<Image> GetAllImages()
        {
            using var context = new ImagesDbContext(_connectionString);
            return context.Images.OrderByDescending(i => i.Date).ToList();
        }
        public Image GetById(int id)
        {
            using var context = new ImagesDbContext(_connectionString);
            return context.Images.FirstOrDefault(i => i.Id == id);
        }
        public void AddLike(int id)
        {

            using var context = new ImagesDbContext(_connectionString);
            var image = context.Images.FirstOrDefault(i => i.Id == id);
            image.Likes++;
            context.SaveChanges();
        }
       
    }
}
