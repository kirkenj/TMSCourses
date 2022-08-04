using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT14.Models
{
    public class Executive : Manager
    {
        public override Posts Post => Posts.Executive;

        private protected static void CopyAndFillGaps(Employee source, Executive destination)
        {
            if (destination is not Executive)
            {
                CopyAndFillGaps(destination, source);
            }
        }
    }
}
