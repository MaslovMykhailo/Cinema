using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Web.Models
{
    public class FilmPrice
    {
        public Guid FilmId { get; set; }

        public float Price { get; set; }
    }
}
