using System.Configuration;

namespace Inexa.Atlantis.Re.Commons.Infras.Configuration
{
    public class WebConfigApplicationSettings : IApplicationSettings
    {
        public string LoggerName
        {
            get
            {
                return ConfigurationManager.AppSettings["LoggerName"];
            }
        }

        public string SiteUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["SiteUrl"];
            }
        }

        public string ListName
        {
            get
            {
                return ConfigurationManager.AppSettings["ListName"];
            }
        }

        public string UserName
        {
            get
            {
                return ConfigurationManager.AppSettings["UserName"];
            }
        }

        public string Password
        {
            get
            {
                return ConfigurationManager.AppSettings["Password"];
            }
        }

        public bool IsRelativeUrl
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["Password"]);
            }
        }

        public string FormatageDocument
        {
            get
            {
                return ConfigurationManager.AppSettings["FormatageDocument"];
            }
        }

        public string Extension
        {
            get
            {
                return ConfigurationManager.AppSettings["Extension"];
            }
        }



        public string ExpEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["ExpEmail"];
            }
        }

        public string ExpFullName
        {
            get
            {
                return ConfigurationManager.AppSettings["ExpFullName"];
            }
        }

        public string ToMail
        {
            get
            {
                return ConfigurationManager.AppSettings["ToMail"];
            }
        }

        public string ToFullName
        {
            get
            {
                return ConfigurationManager.AppSettings["ToFullName"];
            }
        }

        public string BccMail
        {
            get
            {
                return ConfigurationManager.AppSettings["BccMail"];
            }
        }

        public string BccFullName
        {
            get
            {
                return ConfigurationManager.AppSettings["BccFullName"];
            }
        }

        public bool? SendEmailOrNo
        {
            get
            {
                var rep = ConfigurationManager.AppSettings["SendEmailOrNo"];
                return rep != null ?  rep.Equals("true") : (bool?)null;
            }
        }





        public string DomainName
        {
            get
            {
                return ConfigurationManager.AppSettings["DomainName"];
            }
        }



        public string LienServiceDevise
        {
            get
            {
                return ConfigurationManager.AppSettings["LienServiceDevise"];
            }
        }
        public string CodeServiceDevise
        {
            get
            {
                return ConfigurationManager.AppSettings["CodeServiceDevise"];
            }
        }
        public string TypeDonneeServiceDevise
        {
            get
            {
                return ConfigurationManager.AppSettings["TypeDonneeServiceDevise"];
            }

        }
        public string CleServiceDevise
        {
            get
            {
                return ConfigurationManager.AppSettings["CleServiceDevise"];
            }

        }

       
    }
}
