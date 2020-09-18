using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITest.BO
{
    public class Photo
    {
        public string id { get; set; }
        public string owner { get; set; }
        public string secret { get; set; }
        public string server { get; set; }
        public int farm { get; set; }
        public string title { get; set; }
        public bool ispublic { get; set; }
        public bool isfriend { get; set; }
        public bool isfamily { get; set; }
    }

    public class Photos
    {
        public int page { get; set; }
        public int pages { get; set; }
        public int perpage { get; set; }
        public int total { get; set; }
        public List<Photo> photo { get; set; }

        public DataTable ToUt_SavePhoto()
        {
            var table = new DataTable();
            table.Columns.Add("id", typeof(string));
            table.Columns.Add("owner", typeof(string));
            table.Columns.Add("secret", typeof(string));
            table.Columns.Add("server", typeof(string));
            table.Columns.Add("farm", typeof(int));
            table.Columns.Add("title", typeof(string));
            table.Columns.Add("ispublic", typeof(bool));
            table.Columns.Add("isfriend", typeof(bool));
            table.Columns.Add("isfamily", typeof(bool));

            if (photo != null)
            {
                foreach (var photo in photo)
                {
                    table.Rows.Add(photo.id, photo.owner, photo.secret, photo.farm, photo.title, photo.ispublic, photo.isfriend, photo.isfamily);
                }
            }

            return table;
        }
    }

    public class RootPhotos
    {
        public Photos photos { get; set; }
        public string stat { get; set; }
    }


    
}
