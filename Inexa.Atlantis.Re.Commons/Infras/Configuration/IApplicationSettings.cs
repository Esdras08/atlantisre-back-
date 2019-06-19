namespace Inexa.Atlantis.Re.Commons.Infras.Configuration
{
    public interface IApplicationSettings
    {
        string LoggerName { get; }

        string SiteUrl { get; }

        bool IsRelativeUrl { get; }

        string ListName { get; }

        string UserName { get; }

        string Password { get; }

        string FormatageDocument { get; }

        string Extension { get; }



        string ExpEmail { get; }

        string ExpFullName { get; }

        string ToMail { get; }

        string ToFullName { get; }

        string BccMail { get; }

        string BccFullName { get; }

        bool? SendEmailOrNo { get; }





        string DomainName { get; }


        string LienServiceDevise { get; }
        string CodeServiceDevise { get; }
        string TypeDonneeServiceDevise { get; }
        string CleServiceDevise { get; }
    }
}
