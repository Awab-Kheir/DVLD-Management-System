using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Global_Classes                             //  DVLD.Classes  he
{
    public class clsFormat
    {
        public static string DateToShort(DateTime Dt1)
        {
            return Dt1.ToString("dd/MMM/yyyy");         //   فبصير بينكتب كأسم مو كرقم MMM حط II
        }
    }
}
