using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iMed.Models
{
    public class ProfilePicture
    {
        public string Id { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}
