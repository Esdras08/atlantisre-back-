using Newtonsoft.Json;
using System;
using System.Diagnostics;


namespace Inexa.Atlantis.Re.Commons.Infras.Helpers
{
    public class JsonSerializerHelper
    {
        public static T Deserialize<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
                return default(T);

            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("JsonSerializerHelper->Deserialize->{0}", ex.Message));
                return default(T);
            }
        }

        public static string Serialize(object obj)
        {
            if (obj == null)
                return string.Empty;

            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("JsonSerializerHelper->Serialize->{0}", ex.Message));
                return string.Empty;
            }
        }
    }
}
