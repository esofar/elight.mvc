using System.Configuration;
using System.Web;
using System.Xml;

namespace Elight.Infrastructure
{
    public class Configs
    {
        /// <summary>
        /// 配置文件相对路径。
        /// </summary>
        private const string CONGIG_PATH = "/Configs/AppSetting.config";

        /// <summary>
        /// 根据Key取Value值。
        /// </summary>
        /// <param name="key">键</param>
        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString().Trim();
        }
        /// <summary>
        /// 根据Key修改Value。
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetValue(string key, string value)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath(CONGIG_PATH));
            XmlNode xNode;
            xNode = doc.SelectSingleNode("//appSettings");

            XmlElement modifyElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + key + "']");
            if (modifyElem != null)
            {
                modifyElem.SetAttribute("value", value);
            }
            else
            {
                XmlElement addElem = doc.CreateElement("add");
                addElem.SetAttribute("key", key);
                addElem.SetAttribute("value", value);
                xNode.AppendChild(addElem);
            }
            doc.Save(HttpContext.Current.Server.MapPath(CONGIG_PATH));
        }
    }
}
