using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace CodeMap.Infra
{
    public static class File
    {
        public static List<string> GetAll(string InitialPath, string name)
        {
            return Directory.GetFiles(InitialPath, name, SearchOption.AllDirectories).ToList();
        }

        public static List<string> GetContent(string path)
        {
            List<string> lines = new List<string>();
            foreach (var line in System.IO.File.ReadAllLines(path))
            {
                lines.Add(line.ToLower());
            }
            return lines;
        }

        public static XmlDocument GetConfiguration(string fileName)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(basePath + "/" + fileName);
            return xmlDoc;
        }
    }
}
