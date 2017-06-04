using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Xml.Serialization;
using TrialTask_Bees.Interfaces;
using TrialTask_Bees.Logging;

namespace TrialTask_Bees.DataSaving
{
    public class XmlFileDataSaverController : IDataSaverController
    {
        private static readonly string DEFAULT_PATH = HttpRuntime.AppDomainAppPath + "/"+ ConfigurationManager.AppSettings["defaultSaveXMLFilePath"];
        private static readonly string EXTENSION = ConfigurationManager.AppSettings["defaultSaveXMLFileExtension"];
        private static readonly string FORMAT_BRACKETS = ConfigurationManager.AppSettings["defaultSaveXMLFileFormatBracets"];

        public string Path
        {
            get
            {
                if(string.IsNullOrEmpty(_path))
                {
                    _path = DEFAULT_PATH;
                }
                
                return _path;
            }
            set
            {
                _path = value;

                if (!_path.Contains(EXTENSION)) _path += EXTENSION;
                if (!_path.Contains(FORMAT_BRACKETS)) _path = _path.Replace(EXTENSION, FORMAT_BRACKETS + EXTENSION);
            }
        }

        public void Save<T>(T obj)
        {
            if (obj != null)
            {
                int counter = 1;

                while (File.Exists(string.Format(Path, counter)))
                {
                    counter++;
                }

                try
                {
                    using (FileStream fSteam = new FileStream(string.Format(Path, counter),
                            FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        XmlSerializer xmlSerializer =
                            new XmlSerializer(typeof(T));
                        xmlSerializer.Serialize(fSteam, obj);
                    }
                }
                catch(Exception ex)
                {
                    Logger.Log.Error(ex.Message, ex);
                }
            }
        }

        private string _path;
    }
}