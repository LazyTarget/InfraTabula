using System;
using System.Collections.Generic;

namespace InfraTabula
{
    public class Item
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public IEnumerable<string> Tags { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
