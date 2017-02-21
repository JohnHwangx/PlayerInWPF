using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace WPFTest
{
    class XmlDataServer
    {
        public List<string> GetTagList()
        {
            var tagList = new List<string>();
            string xmlFileName = Path.Combine(Environment.CurrentDirectory, @"Data\Data.xml");
            XDocument xDoc = XDocument.Load(@"C:\Users\john\Source\Repos\Player4\Player4\Image\SongTags.xml");
            var tags = xDoc.Descendants("RegionTag");
            foreach (var tag in tags)
            {
                foreach (var xElement in tag.Elements())
                {
                    tagList.Add(xElement.Name.ToString());
                    //tagList.Add(xElement.Value);
                }
            }
            MessageBox.Show(tagList.Count.ToString());
            return tagList;
        }
    }
}
