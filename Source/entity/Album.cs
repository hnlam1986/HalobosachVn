using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaloBoSach.entity
{
    [Serializable()]
    public class AlbumEnt
    {
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public string FolderPath { get; set; }
        public List<AlbumDetailEnt> ListImage { get; set; }
        public string GetFirstImage
        {
            get
            {
                if (ListImage != null && ListImage.Count > 0)
                {
                    AlbumDetailEnt adetail = ListImage.OrderBy(p => p.OrderImage).FirstOrDefault();
                    if (adetail != null)
                    {
                        return adetail.ImagePath;
                    }
                }
                return "";
            }
        }
    }
    [Serializable()]
    public class AlbumDetailEnt
    {
        public string ImagePath
        {
            get;
            set;
        }
        public int OrderImage { get; set; }
    }
}