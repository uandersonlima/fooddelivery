using System;

namespace fooddelivery.Models.Helpers
{
    public class AppView
    {
        public string Search { get; set; }
        public int? NumberPag { get; set; }
        public int? RecordPerPage { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public bool CheckSearch()
        {
            if (!string.IsNullOrEmpty(Search))
                return true;
            return false;
        }
        public bool CheckDate()
        {
            if (Start.HasValue && End.HasValue)
                return true;
            return false;
        }
        public bool CheckPagination()
        {
            if (NumberPag.HasValue && RecordPerPage.HasValue)
                return true;
            return false;
        }
    }
}
