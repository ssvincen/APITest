using APITest.BI;
using APITest.BO;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace APITest.Controllers
{
    [Authorize]
    [Route("FlickrImages")]
    public class FlickrImagesController : ApiController
    {
        private readonly IFlickrImagesDataAccess flickrImagesData;
        public FlickrImagesController(IFlickrImagesDataAccess flickrImagesDataAccess)
        {
            flickrImagesData = flickrImagesDataAccess;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("SearchSavedImageBySearchName")]
        public async Task<IEnumerable<DisplayImage>> GetSavedImagesAsync(string name)
        {
            return await flickrImagesData.GetFlickrSavedImagesBySearchNameAsync(name, null);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllSearchCriteria")]
        public async Task<IEnumerable<Location>> GetLocationsAsync()
        {
            return await flickrImagesData.GetLocations();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetImageBySearchName")]
        public async Task<IEnumerable<DisplayImage>> GetPhotoBySearchNameAsync(string name)
        {
            return await flickrImagesData.GetFlickrImagesBySearchNameAsync(name, null);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetImageByLatLong")]
        public async Task<IEnumerable<DisplayImage>> GetPhotoBySearchNameAsync(float latitude, float longitude)
        {
            return await flickrImagesData.GetFlickrImagesByLatLong(latitude, longitude, null);
        }


        [HttpGet]
        [Route("GetImageBySearchNameForRegisteredUser")]
        public async Task<IEnumerable<DisplayImage>> GetPhotoBySearchNameForRegisteredUserAsync(string name)
        {
            var userId = User.Identity.GetUserId();
            return await flickrImagesData.GetFlickrImagesBySearchNameAsync(name, userId);
        }

        [HttpDelete]
        [Route("DeleteImage")]
        public async Task<IHttpActionResult> DeleteImage([FromBody] int id)
        {

            var userId = User.Identity.GetUserId();
            var photo = await flickrImagesData.GetPhotoById(id);
            if (photo == null)
            {
                return BadRequest("Photo Not Found!");
            }
            bool isDeleted = await flickrImagesData.DeleteFlickrPhotoAsync(userId, id);
            return Json(new { isDeleted = isDeleted });
        }

        [HttpDelete]
        [Route("DeleteAllImagesBySearchName")]
        public async Task<IHttpActionResult> DeleteImageBySearchName(string searchName)
        {
            var userId = User.Identity.GetUserId();
            bool isDeleted = await flickrImagesData.DeleteFlickrPhotosBySearchNameAsync(userId, searchName);
            return Json(new { isDeleted = isDeleted });
        }


    }
}
