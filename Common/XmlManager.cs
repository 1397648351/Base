/************************************************
 * 项目名称 ：Common   
 * 项目描述 ：
 * 文件名称 ：XmlManager.cs
 * 版 本 号 ：v1.0.0.0  
 * 说    明 ：
 * 作    者 ：WUZE 
 * 创建时间 ：2018/6/11 22:15:13 
 * 更新时间 ：2018/6/11 22:15:13 
*************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Common
{
    public static class XmlManager
    {
        /// <summary>
        /// 取指定的结点
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="nodePath"></param>
        /// <returns>取指定的结点</returns>
        public static XmlNode Find(string xmlPath, string nodePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            //取指定的单个结点
            XmlNode node = xmlDoc.SelectSingleNode(nodePath);
            return node;
        }

        /// <summary>
        /// 取指定的结点的集合
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="nodePath"></param>
        /// <returns>取指定的结点的集合</returns>
        public static XmlNodeList SelectByPath(string xmlPath, string nodePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            //取指定的结点的集合
            XmlNodeList nodes = xmlDoc.SelectNodes(nodePath);
            return nodes;
        }

        /// <summary>
        /// 取指定的结点的集合
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="nodePath"></param>
        /// <returns>取指定的结点的集合</returns>
        public static XmlNodeList SelectByTagName(string xmlPath, string nodePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            //取到所有的xml结点
            XmlNodeList nodes = xmlDoc.GetElementsByTagName("*");
            return nodes;
        }

        /// <summary>
        /// 取指定节点属性值
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="nodePath"></param>
        /// <param name="attrName"></param>
        public static string SelectAttribute(string xmlPath, string nodePath, string attrName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlElement element = (XmlElement)xmlDoc.SelectSingleNode(nodePath);
            string attrValue = element.GetAttribute(attrName);
            return attrValue;
        }

        /// <summary>
        /// 设置节点属性
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="nodePath"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void CreateAttribute(string xmlPath, string nodePath, string name, string value)
        {
            //属性
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlElement node = (XmlElement)xmlDoc.SelectSingleNode(nodePath);
            node.SetAttribute(name, value);
            xmlDoc.Save(xmlPath);
        }
    }
}
