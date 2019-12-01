using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Web.Models
{
    public class PagedFilmSearchModel : FilmSearchModel
    {
        public static readonly int PAGE_SIZE = 1000;  

        public int Page { get; set; }
    }
}
