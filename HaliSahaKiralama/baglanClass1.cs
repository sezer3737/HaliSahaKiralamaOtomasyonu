using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace HaliSahaKiralama
{
    class baglanClass1
    { public static string sqlconnection = ConfigurationManager.ConnectionStrings["HaliSahaConnection"].ConnectionString;
    }
}
