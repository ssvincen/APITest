using APITest.BO;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;

namespace APITest.BI
{
    public class FlickrImagesDataAccess : IFlickrImagesDataAccess
    {
        private readonly IConnectionManager connectionManager;
        private readonly string flickrUrl = "https://www.flickr.com/services/rest/?method=flickr.photos.search&api_key=e866ae2a6c47a207711af409cc7f5613&";
        public FlickrImagesDataAccess(IConnectionManager _connectionManager)
        {
            connectionManager = _connectionManager;
        }

        public async Task<IEnumerable<DisplayImage>> GetFlickrImagesBySearchNameAsync(string name, string userId)
        {
            var displayImage = new List<DisplayImage>();
            using (HttpClient client = new HttpClient())
            {
                string jsonstring = await client.GetStringAsync(flickrUrl + $"tags={name}&format=json&nojsoncallback=1");
                var rootPhotos = JsonConvert.DeserializeObject<RootPhotos>(jsonstring);
                if (rootPhotos.stat == "ok")
                {
                    foreach (var item in rootPhotos.photos.photo)
                    {
                        await SavePhotosAsync(item, name, 0, 0, userId);
                    }                    
                    return displayImages(rootPhotos.photos.photo);                    
                }
                return displayImage;
            }
        }

      

        public async Task<IEnumerable<DisplayImage>> GetFlickrImagesByLatLong(float latitude, float longitude, string userId)
        {
            var displayImage = new List<DisplayImage>();
            using (HttpClient client = new HttpClient())
            {
                //string jsonstring = await client.GetStringAsync(flickrUrl + $"lat={latitude}&lon={longitude}&format=json&nojsoncallback=1");
                string jsonstring = await client.GetStringAsync(flickrUrl + $"lat={29.8345}&lon={30.8384}&format=json&nojsoncallback=1");
                var rootPhotos = JsonConvert.DeserializeObject<RootPhotos>(jsonstring);
                if (rootPhotos.stat == "ok")
                {
                    foreach (var item in rootPhotos.photos.photo)
                    {
                        await SavePhotosAsync(item, null, latitude, longitude, userId);
                    }
                    return displayImages(rootPhotos.photos.photo);                    
                }
                return displayImage;
            }
        }

        public async Task<IEnumerable<DisplayImage>> GetFlickrSavedImagesBySearchNameAsync(string name, string userId)
        {
            var param = new DynamicParameters();
            param.Add("@SearchName", dbType: DbType.String, value: name);
            using (var db = connectionManager.DefaultConnection())
            {
                var photo = (List<Photo>)await db.QueryAsync<Photo>("dbo.sprSearchFromSavedImages", commandType: CommandType.StoredProcedure, param: param);
                return displayImages(photo);
            }
        }

        public async Task<IEnumerable<Location>> GetLocations()
        {
            using (var db = connectionManager.DefaultConnection())
            {
                return await db.QueryAsync<Location>("dbo.sprLocation", commandType: CommandType.StoredProcedure);
            }
        }

















        private List<DisplayImage> displayImages(IEnumerable<Photo> photos)
        {
            List<DisplayImage> displayImage = new List<DisplayImage>();
            foreach (var item in photos)
            {
                Uri image = new Uri($"https://farm{item.farm}.staticflickr.com/{item.server}/{item.id}_{item.secret}.jpg");
                displayImage.Add(new DisplayImage { id = item.id, title = item.title, Image = image });
            }
            return displayImage;            
        }

        private async Task<int> SavePhotosAsync(Photo photo, string name, float Latitude, float Longitude, string userId)
        {
            var param = new DynamicParameters();
            param.Add("@id", dbType: DbType.String, value: photo.id);
            param.Add("@owner", dbType: DbType.String, value: photo.owner);
            param.Add("@secret", dbType: DbType.String, value: photo.secret);
            param.Add("@server", dbType: DbType.String, value: photo.server);
            param.Add("@farm", dbType: DbType.String, value: photo.farm);
            param.Add("@title", dbType: DbType.String, value: photo.title);
            param.Add("@ispublic", dbType: DbType.String, value: photo.ispublic);
            param.Add("@isfriend", dbType: DbType.String, value: photo.isfriend);
            param.Add("@isfamily", dbType: DbType.String, value: photo.isfamily); 
            param.Add("@SearchName", dbType: DbType.String, value: name);
            param.Add("@UserId", dbType: DbType.String, value: userId);
            param.Add("@Latitude", dbType: DbType.String, value: Latitude);
            param.Add("@Longitude", dbType: DbType.String, value: Longitude);

            using (var db = connectionManager.DefaultConnection())
            {
                return await db.QueryFirstOrDefaultAsync<int>("dbo.sprSaveFlickrPhoto", commandType: CommandType.StoredProcedure, param: param);
            }
        }

        private async Task<int> SaveAllPhotosAsync(Photos photo)
        {
            int PhotoId = 0;
            var param = new DynamicParameters();
            param.Add("@photos", value: photo.ToUt_SavePhoto().AsTableValuedParameter("dbo.udtFlickrPhoto"));
            using (var db = connectionManager.DefaultConnection())
            {
                await db.QueryFirstOrDefaultAsync<int>("dbo.sprSaveFlickrPhoto", commandType: CommandType.StoredProcedure, param: param);
            }
            return PhotoId;
        }

        
    }
}
