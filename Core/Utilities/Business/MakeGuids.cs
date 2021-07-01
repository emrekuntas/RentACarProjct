using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class MakeGuids
    {
        public static string MakeGuid()
        {
            Guid obj = Guid.NewGuid();
            return obj.ToString();
        }

    }
}
