using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Draw
{
    public class Config
    {
        public static string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
