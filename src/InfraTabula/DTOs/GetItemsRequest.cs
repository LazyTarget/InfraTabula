using PocketAPI;

namespace InfraTabula
{
    public class GetItemsRequest
    {
        public GetItemsRequest()
        {
            Page = 1;
            PageSize = 10;
        }

        
        public string SearchQuery { get; set; }


        public int Page { get; set; }

        public int PageSize { get; set; }
        
        public string Tag { get; set; }

    }
}
