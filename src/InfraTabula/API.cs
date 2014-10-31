using System;
using System.Collections.Generic;

namespace InfraTabula
{
    public abstract class API
    {
        public abstract IEnumerable<Item> GetItems();

    }
}
