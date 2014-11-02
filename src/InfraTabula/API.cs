using System;
using System.Collections.Generic;

namespace InfraTabula
{
    public abstract class API
    {
        public abstract IEnumerable<Item> GetItems();

        public abstract object AddTags(object id, string tags);

        public abstract object RemoveTags(object id, string tags);

    }
}
