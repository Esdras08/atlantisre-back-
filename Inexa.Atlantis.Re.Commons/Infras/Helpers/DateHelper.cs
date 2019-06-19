using System;
using System.IO;
using System.Text;

namespace Inexa.Atlantis.Re.Commons.Infras.Helpers
{
    public static class DateHelper
    {

        public static bool IsDate(string parDate)
        {
            DateTime result;
            return DateTime.TryParse(parDate, out result);
        }
    }
}


