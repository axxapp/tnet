using System;
using System.Xml;
using TCom.WX;

namespace TCom.Util
{
    public class MsgHelper
    {
        /// <summary>
        /// 处理消息并回应
        /// </summary>
        /// <param name="postStr"></param>
        /// <returns></returns>
        public string responseMsg(string postStr)
        {
            string responseContent = string.Empty;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(new System.IO.MemoryStream(System.Text.Encoding.GetEncoding("utf-8").GetBytes(postStr)));
            XmlNode MsgType = xmldoc.SelectSingleNode("/xml/MsgType");
            if (MsgType != null)
            {
                switch (MsgType.InnerText)
                {
                    case "event":
                        responseContent = eventHandle(xmldoc);//事件处理
                        break;
                    case "text":
                        // responseContent = textHandle(xmldoc);//接受文本消息处理
                        break;
                    default:
                        break;
                }
            }
            return responseContent;
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="xmldoc"></param>
        /// <returns></returns>
        private string eventHandle(XmlDocument xmldoc)
        {
            string responseContent = string.Empty;
            XmlNode Event = xmldoc.SelectSingleNode("/xml/Event");
            XmlNode EventKey = xmldoc.SelectSingleNode("/xml/EventKey");
            XmlNode ToUserName = xmldoc.SelectSingleNode("/xml/ToUserName");
            XmlNode FromUserName = xmldoc.SelectSingleNode("/xml/FromUserName");
            if (Event != null)
            {
                WXEvent m = new WXEvent();
                m.FromUserName = FromUserName.InnerText;
                m.ToUserName = ToUserName.InnerText;
                m.Event = Event.InnerText;
                m.EventKey = EventKey.InnerText;

                if (Event.InnerText.Equals("subscribe"))
                {
                    
                    responseContent = doSubscribe(m);
                }
                else if (Event.InnerText.Equals("CLICK"))
                {
                    responseContent = echoNews(m);
                }
            }
            return responseContent;
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private string doSubscribe(WXEvent m)
        {
            string responseContent = string.Empty;
            responseContent = ReplyType.Text(m.FromUserName, m.ToUserName , "谢谢关注我们");
            return responseContent;
        }


        private string echoText(WXEvent m)
        {
            string responseContent = string.Empty;
            responseContent = ReplyType.Text(m.FromUserName, m.ToUserName, "你好");
            return responseContent;
        }


        private string echoImg(WXEvent m)
        {
            string responseContent = string.Empty;
            responseContent = ReplyType.Img(m.FromUserName, m.ToUserName, "mcuDkPvm-HII7zTEJlc6t_O8zUG-KaZXrFZuUCldHVo");
            return responseContent;
        }


        private string echoNews(WXEvent m)
        {
            string responseContent = string.Empty;
            responseContent = ReplyType.NewsItem("你好", "北京", Pub.baseUrl + "Images/Home/2.jpg", Pub.baseUrl);
            for (int i = 0; i < 5; i++)
            {
                responseContent += ReplyType.NewsItem("201"+i+"年", "2016年", Pub.baseUrl + "Images/Com/user.png", Pub.baseUrl);


            }

            responseContent = ReplyType.News(m.FromUserName, m.ToUserName,6, responseContent);
            return responseContent;
        }
    }
}