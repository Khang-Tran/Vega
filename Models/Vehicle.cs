using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        public bool IsRegister { get; set; }
        public Model Model { get; set; }
        public int ModelId { get; set; }
        public DateTime LastUpdate { get; set; }
        public ICollection<VehicleFeature> Features { get; set; }


        public Vehicle()
        {
            Features = new Collection<VehicleFeature>();
        }
    }


}
