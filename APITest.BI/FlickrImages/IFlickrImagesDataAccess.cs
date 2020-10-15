using APITest.BO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APITest.BI
{
    public interface IFlickrImagesDataAccess
    {
        Task<IEnumerable<DisplayImage>> GetFlickrImagesBySearchNameAsync(string name, string userId);
        Task<IEnumerable<DisplayImage>> GetFlickrSavedImagesBySearchNameAsync(string name, string userId);
        Task<IEnumerable<DisplayImage>> GetFlickrImagesByLatLong(float latitude, float longitude, string userId);
        Task<IEnumerable<Location>> GetLocations();
        Task<bool> DeleteFlickrPhotoAsync(string userId, int id);
        Task<Photo> GetPhotoById(int id);
        Task<bool> DeleteFlickrPhotosBySearchNameAsync(string userId, string searchName);

    }
}
