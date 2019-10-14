namespace Cinema.BusinessLogic.Searching
{
    public class TicketSearchModel  : ISearchModel
    {
        public int PlaceNumber { get; set; }
        public SortBy SortBy { get; set; }
        public PriceFilter PriceFilter { get; set; }
        public DateFilter DateFilter { get; set; }
        public string FilmNamePart { get; set; }


        public TicketSearchModel()
        {
            FilmNamePart = "";
            PlaceNumber = 0;
            DateFilter = 0;
            PriceFilter = new PriceFilter();
        }
    }

    public enum SortBy
    {
        Name,
        PriceAscending,
        PriceDescending,
        CreationDate
    }

    public class PriceFilter
    {
        public decimal From { get; set; }
        public decimal To { get; set; }

        public PriceFilter()
        {
            From = 0;
            To = 0;
        }
    }

    public enum DateFilter
    {
        LastWeek = 1,
        LastMonth,
        LastYear,
        LastTwoYears,
        LastThreeYears
    }

}
