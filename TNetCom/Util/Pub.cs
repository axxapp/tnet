using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;
using TCom.Model.ToKey;

namespace TCom.Util
{
    public static class Pub
    {
        static AccessToken am;
        static JsAccessToKey jsam;
        static long t = DateTime.Now.Ticks / 10000;
        volatile static int un = 0;
        private volatile static object lk = new object();
        //id 生成器,CAS版本
        public static string ID()
        {
            //lock (lk)
            {
                long _t = DateTime.Now.Ticks / 10000;
                if (t == _t)
                {
                    //Interlocked.CompareExchange(ref t, _t, _t);
                    Interlocked.Increment(ref un);
                }
                else
                {
                    Interlocked.Exchange(ref t, _t);
                    Interlocked.Exchange(ref un, 0);
                }

                return t + "" + Thread.CurrentThread.ManagedThreadId + "" + un;
            }
        }
        /// <summary>
        /// 微信公众号Token
        /// </summary>
        public static string token
        {
            get
            {
                return ConfigurationManager.AppSettings["r_token"];
            }
        }

        /// <summary>
        /// 微信公众号appid
        /// </summary>
        public static string appid
        {
            get
            {
                return ConfigurationManager.AppSettings["r_appid"];
            }
        }

        /// <summary>
        /// 商家号r_mch_id
        /// </summary>
        public static string mchid
        {
            get
            {
                return ConfigurationManager.AppSettings["r_mch_id"];
            }
        }





        /// <summary>
        /// 微信公众号secret
        /// </summary>
        public static string secret
        {
            get
            {
                return ConfigurationManager.AppSettings["r_secret"];
            }
        }


        /// <summary>
        /// 微信公众号secret
        /// </summary>
        public static string key
        {
            get
            {
                return ConfigurationManager.AppSettings["r_key"];
            }
        }

        public static string weixin_notify_url
        {
            get
            {
                return ConfigurationManager.AppSettings["r_notify_url_weixin"];
            }
        }



        /// <summary>
        /// 从本地获取accessToken
        /// </summary>
        public static string accessToken
        {
            get
            {
                string k = "";
                if (am == null)
                {
                    am = getAccessToken();
                }
                else
                {
                    DateTime t = am.expires.AddMinutes(-5);
                    if (DateTime.Now >= t)
                    {
                        am = getAccessToken();

                    }
                }
                if (am != null)
                {
                    k = am.access_token;
                }
                return k;
            }
        }

        public static AccessToken accessTokenObj
        {
            get
            {

                if (am == null)
                {
                    am = getAccessToken();
                }
                else
                {
                    DateTime t = am.expires.AddMinutes(-5);
                    if (DateTime.Now >= t)
                    {
                        am = getAccessToken();

                    }
                }
                return am;
            }
        }
        public static bool delAccessToken()
        {
            am = null;
            return true;
        }

        /// <summary>
        /// 从本地获取accessToken
        /// </summary>
        public static AccessToken getShareAccessToken()
        {
            AccessToken m = null;
            try
            {
                string s = Get("http://127.0.0.1/TNet/Service/Tokey/WeiXin");
                if (!string.IsNullOrWhiteSpace(s))
                {
                    JavaScriptSerializer Serializer = new JavaScriptSerializer();
                    m = Serializer.Deserialize<AccessToken>(s);
                    if (m != null && !string.IsNullOrWhiteSpace(m.access_token))
                    {
                        m.expires_in = 60 * 30;
                        m.expires = DateTime.Now.AddSeconds(m.expires_in);
                    }

                }
            }
            catch (Exception)
            {
            }
            return m;
        }




        /// <summary>
        /// 从微信服务器获取accessToken，并保存在本地xml中
        /// </summary>
        /// <returns></returns>
        public static AccessToken getAccessToken()
        {
            lock (lk)
            {



                AccessToken m = null;
                if (HttpContext.Current == null)
                {
                    m = getShareAccessToken();
                    return m;
                }
                else
                {
                    string ph = HttpContext.Current.Server.MapPath("~/App_Data/ToKey.j");
                    if (!File.Exists(ph))
                    {
                        File.Create(ph);
                    }
                    string str = "";
                    JavaScriptSerializer s = new JavaScriptSerializer();
                    using (StreamReader sr = new StreamReader(ph))
                    {
                        str = sr.ReadToEnd();
                        if (!string.IsNullOrWhiteSpace(str))
                        {
                            m = s.Deserialize<AccessToken>(str);
                            if (m != null && m.expires.AddMinutes(-5) > DateTime.Now)
                            {
                                return m;
                            }
                        }
                    }
                    string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + secret;
                    try
                    {
                        str = Get(url);
                        if (!string.IsNullOrWhiteSpace(str))
                        {
                            m = s.Deserialize<AccessToken>(str);
                            m.expires = DateTime.Now.AddSeconds(m.expires_in);
                            if (m != null)
                            {
                                try
                                {
                                    str = s.Serialize(m);
                                    if (!string.IsNullOrWhiteSpace(str))
                                    {
                                        using (StreamWriter sw = new StreamWriter(ph))
                                        {
                                            sw.Write(str);
                                        }
                                    }
                                }
                                catch (Exception)
                                {

                                }

                            }

                        }

                    }
                    catch (Exception)
                    {

                    }
                    return m;
                }
            }
        }






        public static string wxJsConfig
        {
            get
            {
                string ns = getRound();
                string ts = DateTime.Now.Ticks / 10000 + "";
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                int j = 0;
                if ((j = url.IndexOf('#')) != -1)
                {
                    url = url.Substring(0, j);
                }
                SortedDictionary<string, string> dict = new SortedDictionary<string, string>();
                dict["noncestr"] = ns;
                dict["jsapi_ticket"] = jsAccessToken;
                dict["timestamp"] = ts;
                dict["url"] = url;
                string sign = "";
                foreach (var k in dict.Keys)
                {
                    if (sign != "")
                    {
                        sign += "&";
                    }
                    sign += k + "=" + dict[k];
                }
                sign = Sha1(sign);
                string v = "var WX_JS_CONFIG_OBJ={" +
                        "debug: false," +
                        "appId: '" + appid + "'," + // 必填，公众号的唯一标识
                        "timestamp: " + ts + "," + // 必填，生成签名的时间戳
                        "nonceStr: '" + ns + "'," + // 必填，生成签名的随机串
                        "signature: '" + sign + "'," +// 必填，签名，见附录1
                        "jsApiList: [" +
                                       "'checkJsApi',                   " +
                                       "'onMenuShareTimeline',          " +
                                       "'onMenuShareAppMessage',        " +
                                       "'onMenuShareQQ',                " +
                                       "'onMenuShareWeibo',             " +
                                       "'onMenuShareQZone',             " +
                                       "'hideMenuItems',                " +
                                       "'showMenuItems',                " +
                                       "'hideAllNonBaseMenuItem',       " +
                                       "'showAllNonBaseMenuItem',       " +
                                       "'translateVoice',               " +
                                       "'startRecord',                  " +
                                       "'stopRecord',                   " +
                                       "'onVoiceRecordEnd',             " +
                                       "'playVoice',                    " +
                                       "'onVoicePlayEnd',               " +
                                       "'pauseVoice',                   " +
                                       "'stopVoice',                    " +
                                       "'uploadVoice',                  " +
                                       "'downloadVoice',                " +
                                       "'chooseImage',                  " +
                                       "'previewImage',                 " +
                                       "'uploadImage',                  " +
                                       "'downloadImage',                " +
                                       "'getNetworkType',               " +
                                       "'openLocation',                 " +
                                       "'getLocation',                  " +
                                       "'hideOptionMenu',               " +
                                       "'showOptionMenu',               " +
                                       "'closeWindow',                  " +
                                       "'scanQRCode',                   " +
                                       "'chooseWXPay',                  " +
                                       "'openProductSpecificView',      " +
                                       "'addCard',                      " +
                                       "'chooseCard',                   " +
                                       "'openCard'                      " +
                           "]" + // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
                     "};";
                return v;
            }

        }

        private static string getRound()
        {
            StringBuilder sb = new StringBuilder(100);
            Random rd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 10; i++)
            {
                sb.Append(rd.Next(10000000));
            }
            return sb.ToString();
        }

        public static string jsAccessToken
        {
            get
            {
                string k = "";
                if (jsam == null)
                {
                    jsam = getJsAccessToken();
                }
                else
                {
                    DateTime t = am.expires.AddMinutes(-5);
                    if (DateTime.Now >= t)
                    {
                        jsam = getJsAccessToken();

                    }
                }
                if (jsam != null)
                {
                    k = jsam.ticket;
                }
                return k;
            }
        }

        private static JsAccessToKey getJsAccessToken()
        {
            JsAccessToKey m = null;
            string url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + accessToken + "&type=jsapi";
            try
            {
                string reqStr = Get(url);
                if (!string.IsNullOrWhiteSpace(reqStr))
                {
                    JavaScriptSerializer Serializer = new JavaScriptSerializer();
                    m = Serializer.Deserialize<JsAccessToKey>(reqStr);
                    m.expires = DateTime.Now.AddSeconds(m.expires_in);
                }
            }
            catch (Exception)
            {
            }
            return m;
        }


        ///// <summary>
        ///// 本地路径转换成URL相对路径
        ///// </summary>
        ///// <param name="imagesurl1"></param>
        ///// <returns></returns>
        //public static string urlconvertor(string imagesurl1)
        //{
        //    string tmpRootDir = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
        //    string imagesurl2 = imagesurl1.Replace(tmpRootDir, ".."); //转换成相对路径
        //    imagesurl2 = imagesurl2.Replace(@"\", @"/");
        //    //imagesurl2 = imagesurl2.Replace(@"Aspx_Uc/", @"");
        //    return imagesurl2;
        //}

        /// <summary>
		/// Base64解密
		/// </summary>
		/// <param name="str">加密字符串</param>
		/// <returns>原字符串</returns>
		public static string DeBase64(string str)
        {
            //调用FromBase64String()返回解密后的byte数组
            byte[] temps = Convert.FromBase64String(str);
            //把byte数组转化为string类型
            string tempd = Encoding.Default.GetString(temps);
            //输出解密结果
            return tempd;
        }



        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <returns>原字符串</returns>
        public static byte[] DeBase64ToBytes(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                //调用FromBase64String()返回解密后的byte数组
                byte[] temps = Convert.FromBase64String(str);
                //把byte数组转化为string类型
                //string tempd = Encoding.Default.GetString(temps);
                //输出解密结果
                return temps;
            }
            return null;
        }



        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Post(string url, string data, string contentType = "application/x-www-form-urlencoded;charset=UTF-8")
        {
            try
            {
                Encoding encoding = Encoding.UTF8;
                byte[] bdata = encoding.GetBytes(data);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.AllowAutoRedirect = true;
                request.ContentType = contentType;
                request.ContentLength = bdata.Length;
                CookieContainer cookie = new CookieContainer();
                request.CookieContainer = cookie;
                using (Stream myRequestStream = request.GetRequestStream())
                {
                    myRequestStream.Write(bdata, 0, bdata.Length);
                    myRequestStream.Close();
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    response.Cookies = cookie.GetCookies(response.ResponseUri);
                    using (Stream myResponseStream = response.GetResponseStream())
                    {
                        using (StreamReader myStreamReader = new StreamReader(myResponseStream, encoding))
                        {
                            string retString = myStreamReader.ReadToEnd();
                            myStreamReader.Close();
                            myResponseStream.Close();
                            return retString;
                        }
                    }
                }


            }
            catch (Exception)
            {
            }
            return "";
        }


        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Get(string url, string contentType = "text/html;charset=UTF-8")
        {
            try
            {
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowAutoRedirect = true;
                request.Method = "GET";
                request.ContentType = contentType;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream myResponseStream = response.GetResponseStream())
                    {
                        using (StreamReader myStreamReader = new StreamReader(myResponseStream, encoding))
                        {
                            string retString = myStreamReader.ReadToEnd();
                            myStreamReader.Close();
                            myResponseStream.Close();
                            return retString;
                        }
                    }
                }

            }
            catch (Exception)
            {
            }
            return "";
        }



        public static string Sha1(string str)
        {
            byte[] data = UTF8Encoding.Default.GetBytes(str);
            SHA1 sha = new SHA1CryptoServiceProvider();
            // This is one implementation of the abstract class SHA1.
            data = sha.ComputeHash(data);
            string result = BitConverter.ToString(data);
            result = result.Replace("-", "");
            return result;
        }


        public static void e(string m)
        {
            lock (lk)
            {
                FileStream f = File.Open(@"C:\1.txt", FileMode.OpenOrCreate | FileMode.Append);

                byte[] data = System.Text.Encoding.UTF8.GetBytes(m + "\r\n\r\n");
                f.Write(data, 0, data.Length);
                f.Flush();
                f.Close();

            }
        }

    }
}