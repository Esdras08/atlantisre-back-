using System.Runtime.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;
using YAXLib;

namespace Inexa.Atlantis.Re.Commons.Infras.Domains
{
    /// <summary>
    /// Response base
    /// </summary>
    [DataContract]
    public class ResponseBase
    {
        /// <summary>
        /// Permet de signifier que la fonction executée à générer une erreur
        /// </summary>
        [XmlIgnore]
        [DataMember]
        public bool HasError { get; set; }

        /// <summary>
        /// Message d'erreur lorsque HasError est True
        /// </summary>
        [XmlIgnore]
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Contient le nombre total d'enregistrement dans la base de données pour une demande effectuée
        /// </summary>
        [XmlIgnore]
        [DataMember]
        public int Count { get; set; }

        /// <summary>
        /// Contient l'index debut d'une sequence
        /// </summary>
        [XmlIgnore]
        [DataMember]
        public int IndexDebut { get; set; }

        /// <summary>
        /// Contient l'index fin d'une sequence
        /// </summary>
        [XmlIgnore]
        [DataMember]
        public int IndexFin { get; set; }

        /// <summary>
        /// Indique que l'utilisateur est authentifié
        /// </summary>
        [XmlIgnore]
        [DataMember]
        public bool IsAuthentify { get; set; }

        /// <summary>
        /// Indique que l'utilisateur est connecté
        /// </summary>
        [XmlIgnore]
        [DataMember]
        public bool IsConnected { get; set; }

        public ResponseBase()
        {
            HasError = false;
            IsAuthentify = false;
        }
    }

    public static class ResponseBaseExtension
    {
        public static string ToJsonString(this ResponseBase response)
        {
            return JsonConvert.SerializeObject(response);
        }

        public static string ToXmlString(this ResponseBase response)
        {
            var serializer = new YAXSerializer(response.GetType());
            return serializer.Serialize(response);
        }
    }
}
