using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.Models
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }

        public bool isSupported(string fileName)
        {
            return AcceptedFileTypes.All(s => s != Path.GetExtension(fileName).ToLower());

        }

    }
}
