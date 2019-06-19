using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Inexa.Atlantis.Re.Commons.Infras.Helpers
{
    public class SerializeDeSerializeHelper<T>
    {
        public SerializeDeSerializeHelper()
        {
        }

        private static void ToFile(string path, T o)
        {
            TextWriter textWriter;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (textWriter = new StreamWriter(path))
                {
                    serializer.Serialize(textWriter, o);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static MemoryStream ToMemory(T o)
        {
            try
            {
                var memoryStream = new MemoryStream();
                var serializer = new XmlSerializer(typeof(T));

                serializer.Serialize(memoryStream, o);
                return memoryStream;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static string ToXml(T o)
        {
            var returnVal = string.Empty;

            using (var memoryStream = ToMemory(o))
            {
                memoryStream.Position = 0;
                using (var sr = new StreamReader(memoryStream))
                {
                    returnVal = sr.ReadToEnd();
                }
            }
            return returnVal;
        }




        private static T FromFile(string path)
        {
            T o;
            try
            {
                var serializer = new XmlSerializer(typeof(T));

                using (TextReader textReader = new StreamReader(path))
                {
                    o = (T)serializer.Deserialize(textReader);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return o;
        }

        public static T FromXml(string xmlString)
        {
            T o;
            try
            {
                var stringReader = new StringReader(xmlString);
                var textReader = new XmlTextReader(stringReader);
                var serializer = new XmlSerializer(typeof(T));

                o = (T)serializer.Deserialize(textReader);
                stringReader.Close();
                textReader.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            return o;
        }
    }
}
