using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vega.Models;

namespace Vega.Controllers.Resources
{
    public class MakeResource: KeyValuePairResource
    {
        public ICollection<KeyValuePairResource> Models { get; set; }

        public MakeResource()
        {
            // This initialization insures that we wont forget to init this object when create a new Make object
            // avoid null reference exception
            Models = new Collection<KeyValuePairResource>();
        }
    }
}
