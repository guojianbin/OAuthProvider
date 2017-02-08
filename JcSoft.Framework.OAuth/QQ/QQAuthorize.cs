using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using Newtonsoft.Json;

namespace JcSoft.Framework.OAuth.QQ
{
    public class QQAuthorize : BaseOAuth, IOAuthInstance
    {
        private string appId = ConfigurationManager.AppSettings["OAuth_QQ_AppId"];
        private string appKey = ConfigurationManager.AppSettings["OAuth_QQ_AppKey"];

        QQAuthorizeOption Option { get; }

        public string Name { get; set; }
        public QQAuthorize()
        {
            Option = new QQAuthorizeOption(appId, appKey);
            Name = "QQ";
            Option.RedirectUrl = "/account/unionlogin";
        }

        public override void Login(HttpContext context)
        {
            string parms = "?appid=" + Option.AppId
     + "&redirect_uri=" + Uri.EscapeDataString(Option.RedirectUrl) + "&response_type=code&scope=snsapi_login"
     + "&state=" + Name + "#wechat_redirect";

            var url = Option.AuthorizationEndpoint + parms;
            context.Response.Redirect(url);
        }

        public QQAuthorize(string appId, string secretKey)
        {
            Option = new QQAuthorizeOption(appId, secretKey);
            Name = "QQ";
        }

        public string AuthorizeUrl()
        {
            string parms = "?appid=" + Option.AppId
     + "&redirect_uri=" + Uri.EscapeDataString(Option.RedirectUrl) + "&response_type=code&scope=snsapi_login"
     + "&state=" + Name + "#wechat_redirect";

            return Option.AuthorizationEndpoint + parms;
        }


        public override dynamic Callback(HttpContext context, out string openId)
        {
            string code = context.Request["code"];
            string parms = "?grant_type=authorization_code&"
              + "client_id=" + Option.AppId + "&redirect_uri=" + Uri.EscapeDataString(Option.RedirectUrl)
              + "&client_secret=" + Option.SecretKey + "&code=" + code;

            string url = Option.TokenEndpoint + parms;

            string str = GetRequest(url);

            dynamic token = JsonConvert.DeserializeObject<dynamic>(str);
            openId = GetOpenID(token["access_token"]);
            return token;
        }


        /// <summary>
        /// 使用Access Token来获取用户的OpenID
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public string GetOpenID(string accessToken)
        {
            string parms = "?access_token=" + accessToken;

            string url = Option.UserInformationEndpoint + parms;
            string str = GetRequest(url);

            NameValueCollection user = ParseJson(str);

            return user["openid"];
        }
    }
}
