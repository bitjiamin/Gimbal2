using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Gimbal
{
    public class Operxml
    {

        string basepath = System.IO.Directory.GetCurrentDirectory();
        /// 创建节点  
        /// </summary>  
        /// <param name="xmldoc"></param>  xml文档
        /// <param name="parentnode"></param>父节点  
        /// <param name="name"></param>  节点名
        /// <param name="value"></param>  节点值
        /// 
        public void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }

        public void CreateXmlFile(string IP,string Port)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建根节点
            XmlNode root = xmlDoc.CreateElement("config");
            xmlDoc.AppendChild(root);

            XmlNode node1 = xmlDoc.CreateNode(XmlNodeType.Element, "Tcp", null);
            CreateNode(xmlDoc, root, "IP", IP);
            CreateNode(xmlDoc, root, "Port", Port);
            root.AppendChild(node1);

            try
            {
                xmlDoc.Save(basepath+@"\TCPconfig.xml");
            }
            catch (Exception e)
            {
                //显示错误信息
                Console.WriteLine(e.Message);
            }
            //Console.ReadLine();
        }

            public string ReadXmlFile(string parent, string key)
           {
            string strData=null;
            XmlDocument doc = new XmlDocument();
            //加载Xml文件
            doc.Load(basepath+@"\TCPconfig.xml");
            //获取节点
            //XmlNode xmlNode1 = doc.SelectSingleNode("/config");
            XmlNode xmlNode1 = doc.SelectSingleNode("/config");
            //获取IP属性值
           // string strData = xmlNode1[key].InnerText;
            var nodes = xmlNode1.FirstChild;
            while(nodes != null )
            {
                if (nodes.Name == parent)
                {
                   
                    var childnode = nodes.FirstChild;
                    if (childnode.Name == key)
                    {
                        strData = childnode.InnerText;
                    }
                    else if (childnode.NextSibling.Name == key) {
                        strData = childnode.NextSibling.InnerText;
                    }
                }
                nodes =nodes.NextSibling;
            }
          
           
            return strData;
           }

        

    }
}
