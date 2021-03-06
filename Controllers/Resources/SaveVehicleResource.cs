﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vega.Models;

namespace Vega.Controllers.Resources
{
    public class SaveVehicleResource
    {
        public int Id { get; set; }
        public ContactResource Contact { get; set; }
        public bool IsRegister { get; set; }
        public int ModelId { get; set; }
        public ICollection<int> Features { get; set; }

        public SaveVehicleResource()
        {
            Features=new Collection<int>();
        }
    }
}
