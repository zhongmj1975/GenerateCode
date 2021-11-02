using System;
using System.Collections.Generic;
using System.Text;

namespace FSELink.Entities
{
    public class GenerateException:Exception
    {
        public List<RequestOrder> ErrorOrders;
    }
}
