
using System.Collections.Generic;

namespace AdrianP.Models
{
    public class ApiResponseList
    {
        public List<string> Data { get; set; }
    }

    public class ApiResponseNotify
    {
        public List<UserNotify> Data { get; set; }
    }
}
