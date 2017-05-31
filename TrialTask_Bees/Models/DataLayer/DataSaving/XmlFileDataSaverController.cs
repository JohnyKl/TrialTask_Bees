using System;
using System.IO;
using System.Xml.Serialization;
using TrialTask_Bees.Interfaces;
using TrialTask_Bees.Logging;

namespace TrialTask_Bees.DataSaving
{
    public class XmlFileDataSaverController : IDataSaverController
    {
        private static readonly string DEFAULT_PATH = "saved{0}.xml";
        private static readonly string EXTENSION = ".xml";
        private static readonly string FORMAT_BRACKETS = "{0}";

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