using System;
using System.Configuration;
using System.Web;
using Newtonsoft.Json;

namespace JcSoft.Framework.OAuth.Weibo
{
    public class WeiboAuthorize : BaseOAuth, IOAuthInstance
    {
        string _appId = ConfigurationManager.AppSettings["OAuth_Weixin_AppId"];
        string _appSecret = ConfigurationManager.AppSettings["OAuth_Weixin_AppSecret"];

        WeiboAuthorizeOption Option { get; }

        public string Name { get; set; }
        public WeiboAuthorize()
        {
            Option = new WeiboAuthorizeOption(_appId, _appSecret);
            Name = "Weibo";
            Option.RedirectUrl = "/account/unionlogin";
        }

        public WeiboAuthorize(string appId, string secretKey)
        {
            Option = new WeiboAuthorizeOption(appId, secretKey);
            Name = "Weibo";
        }

        public override void Login(HttpContext context)
        {
            string parms = "?client_id=" + Option.AppId + "&redirect_uri=" + Uri.EscapeDataString(Option.RedirectUrl)
        + "&state=" + Name;

            var url = Option.AuthorizationEndpoint + parms;
            context.Response.Redirect(url);
        }

        public override dynamic Callback(HttpContext context,out string openId)
        {
            string code = context.Request["code"];
            string parms = "client_id=" + Option.AppId + "&client_secret=" + Option.SecretKey
      + "&grant_type=authorization_code&code=" + code + "&redirect_uri=" + Uri.EscapeDataString(Option.RedirectUrl);
            
            string url = Option.TokenEndpoint + parms;

            string str = GetRequest(url);

            dynamic token = JsonConvert.DeserializeObject<dynamic>(str);
            openId = token["uid"];
            return token;
        }

        public string AuthorizeUrl()
        {
            string parms = "?client_id=" + Option.AppId + "&redirect_uri=" + Uri.EscapeDataString(Option.RedirectUrl)
        + "&state=" + Name;

            var url = Option.AuthorizationEndpoint + parms;
            return url;
        }
        
    }
}
