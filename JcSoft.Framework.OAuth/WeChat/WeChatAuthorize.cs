using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using Newtonsoft.Json;

namespace JcSoft.Framework.OAuth.WeChat
{
    public class WeChatAuthorize : BaseOAuth, IOAuthInstance
    {
        string AppId = ConfigurationManager.AppSettings["OAuth_Weixin_AppId"];
        string AppSecret = ConfigurationManager.AppSettings["OAuth_Weixin_AppSecret"];

        WeChatAuthorizeOption Option { get; }

        public string Name { get; set; }
        public WeChatAuthorize()
        {
            string appId = AppId;
            string secretKey = AppSecret;
            Option = new WeChatAuthorizeOption(appId, secretKey);
            Name = "WeChat";
            Option.RedirectUrl = "/account/unionlogin";
        }

        public WeChatAuthorize(string appId, string secretKey)
        {
            Option = new WeChatAuthorizeOption(appId, secretKey);
            Name = "WeChat";
        }

        public override void Login(HttpContext context)
        {
            string parms = "?appid=" + AppId
     + "&redirect_uri=" + Uri.EscapeDataString(Option.RedirectUrl) + "&response_type=code&scope=snsapi_login"
     + "&state=" + Name + "#wechat_redirect";

            var url = Option.AuthorizationEndpoint + parms;
            context.Response.Redirect(url);
        }

        public override dynamic Callback(HttpContext context,out string openId)
        {
            string code = context.Request["code"];
            string parms = "?grant_type=authorization_code&"
  + "client_id=" + Option.AppId + "&redirect_uri=" + Uri.EscapeDataString(Option.RedirectUrl)
  + "&client_secret=" + Option.SecretKey + "&code=" + code;

            string url = Option.TokenEndpoint + parms;

            string str = GetRequest(url);

            dynamic token = JsonConvert.DeserializeObject<dynamic>(str);
            openId = token["access_token"];
            return token;
        }

        public string AuthorizeUrl()
        {
            string parms = "?appid=" + AppId
     + "&redirect_uri=" + Uri.EscapeDataString(Option.RedirectUrl) + "&response_type=code&scope=snsapi_login"
     + "&state=" + Name + "#wechat_redirect";

            var url = Option.AuthorizationEndpoint + parms;
            return url;
        }
        
    }
}
