using System;

namespace TCom.WX
{
    public class ReplyType
    {
        /// <summary>
        /// 普通文本消息
        /// </summary>
        public static string Text(string ToUserName, string FromUserName, string Content)
        {
            return string.Format(@"<xml>
                     <ToUserName><![CDATA[{0}]]></ToUserName>
                     <FromUserName><![CDATA[{1}]]></FromUserName>
                     <CreateTime>{2}</CreateTime>
                     <MsgType><![CDATA[text]]></MsgType>
                     <Content><![CDATA[{3}]]></Content>
                     </xml>", ToUserName, FromUserName, DateTime.Now.Ticks, Content);
        }



        public static string Img(string ToUserName, string FromUserName, string MediaId)
        {
            return string.Format(@"<xml>
                <ToUserName><![CDATA[{0}]]></ToUserName>
                <FromUserName><![CDATA[{1}]]></FromUserName>
                <CreateTime>{2}</CreateTime>
                <MsgType><![CDATA[image]]></MsgType>
                <Image>
                <MediaId><![CDATA[{3}]]></MediaId>
                </Image>
                </xml>", ToUserName, FromUserName, DateTime.Now.Ticks, MediaId);

        }



        /// <summary>
        /// 图文消息项
        /// </summary>
        public static string NewsItem(string Title, string Description, string PicUrl, string Url)
        {
            return string.Format(@"<item>
                            <Title><![CDATA[{0}]]></Title> 
                            <Description><![CDATA[{1}]]></Description>
                            <PicUrl><![CDATA[{2}]]></PicUrl>
                            <Url><![CDATA[{3}]]></Url>
                            </item>", Title, Description, PicUrl, Url);

        }


        /// <summary>
        /// 图文消息主体
        /// </summary>
        public static string News(string ToUserName, string FromUserName, int ArticleCount, string Articles)
        {
            return string.Format(@"<xml>
                            <ToUserName><![CDATA[{0}]]></ToUserName>
                            <FromUserName><![CDATA[{1}]]></FromUserName>
                            <CreateTime>{2}</CreateTime>
                            <MsgType><![CDATA[news]]></MsgType>
                            <ArticleCount>{3}</ArticleCount>
                            <Articles>
                            {4}
                            </Articles>
                            </xml>", ToUserName, FromUserName, DateTime.Now.Ticks, ArticleCount, Articles);

        }

        /// <summary>
        /// 视频消息
        /// </summary>
        public static string Video(string ToUserName, string FromUserName, string MediaId, string Description)
        {
            return string.Format(@"<xml>
                        <ToUserName><![CDATA[{0}]]></ToUserName>
                        <FromUserName><![CDATA[{1}]]></FromUserName>
                        <CreateTime>{2}</CreateTime>
                        <MsgType><![CDATA[video]]></MsgType>
                        <Video>
                        <MediaId><![CDATA[{3}]]></MediaId>
                        <Title><![CDATA[{4}]]></Title>
                        <Description><![CDATA[{5}]]></Description>
                        </Video> 
                        </xml>", ToUserName, FromUserName, DateTime.Now.Ticks, MediaId, Description);

        }

        /// <summary>
        /// 音乐消息
        /// </summary>
        public static string Music(string ToUserName, string FromUserName, string Title, string Description, string MusicUrl, string HQMusicUrl, string ThumbMediaId)
        {
            return string.Format(@"<xml>
                        <ToUserName><![CDATA[{0}]]></ToUserName>
                        <FromUserName><![CDATA[{1}]]></FromUserName>
                        <CreateTime>{2}</CreateTime>
                        <MsgType><![CDATA[music]]></MsgType>
                        <Music>
                        <Title><![CDATA[{3}]]></Title>
                        <Description><![CDATA[{4}]]></Description>
                        <MusicUrl><![CDATA[{5}]]></MusicUrl>
                        <HQMusicUrl><![CDATA[{6}]]></HQMusicUrl>
                        <ThumbMediaId><![CDATA[{7}]]></ThumbMediaId>
                        </Music>
                        </xml>", ToUserName, FromUserName, DateTime.Now.Ticks, Title, Description, MusicUrl, HQMusicUrl, HQMusicUrl, ThumbMediaId);


        }

        /// <summary>
        /// 语音消息
        /// </summary>
        public static string Voice(string ToUserName, string FromUserName, string MediaId)
        {
            return string.Format(@"<xml>
                        <ToUserName><![CDATA[{0}]]></ToUserName>
                        <FromUserName><![CDATA[{1}]]></FromUserName>
                        <CreateTime>{2}</CreateTime>
                        <MsgType><![CDATA[voice]]></MsgType>
                        <Voice>
                        <MediaId><![CDATA[{3}]]></MediaId>
                        </Voice>
                        </xml>", ToUserName, FromUserName, DateTime.Now.Ticks, MediaId);

        }
    }
}