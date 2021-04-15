using System.Collections.Generic;


namespace fooddelivery.Models.Helpers
{
    public class PaginationList<T> : List<T> where T : class
    {
        public Pagination Pagination { get; set; }
    }
}
