using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using Newtonsoft.Json;

namespace JcSoft.Framework.OAuth.WeChat
{
    public class WeChatAuthorize : BaseOAuth, IOAuthInstance
    {
        string _appId = ConfigurationManager.AppSettings["OAuth_Weixin_AppId"];
        string _appSecret = ConfigurationManager.AppSettings["OAuth_Weixin_AppSecret"];

        public WeChatAuthorizeOption Option { get; }

        public string Name { get; set; }
        public WeChatAuthorize()
        {
            string appId = _appId;
            string secretKey = _appSecret;
            Option = new WeChatAuthorizeOption(appId, secretKey);
            Name = "WeChat";
            AppId = appId;
            //Option.RedirectUrl = "/account/unionlogin";
            Option.RedirectUrl = RedirectUrl;
        }

        public WeChatAuthorize(string appId, string secretKey)
        {
            Option = new WeChatAuthorizeOption(appId, secretKey);
            Name = "WeChat";
            _appId = appId;
        }

        public override void Login(HttpContext context)
        {
            string parms = "?appid=" + _appId
     + "&redirect_uri=" + Uri.EscapeDataString(Option.RedirectUrl) + "&response_type=code&scope=snsapi_login"
     + "&state=" + Name + "#wechat_redirect";

            var url = Option.AuthorizationEndpoint + parms;
            context.Response.Redirect(url);
        }

        public override dynamic Callback(HttpContext context, out string openId)
        {
            string code = context.Request["code"];
            string parms = "?grant_type=authorization_code&"
  + "appid=" + Option.AppId + "&secret=" + Option.SecretKey + "&code=" + code + "&grant_type=authorization_code";

            string url = Option.TokenEndpoint + parms;

            string str = GetRequest(url);

            dynamic token = JsonConvert.DeserializeObject(str);
            openId = token["openid"] ?? "";
            return token;
        }

        public string AuthorizeUrl()
        {
            string parms = "?appid=" + _appId
     + "&redirect_uri=" + Uri.EscapeDataString(Option.RedirectUrl) + "&response_type=code&scope=snsapi_login"
     + "&state=" + Name + "#wechat_redirect";

            var url = Option.AuthorizationEndpoint + parms;
            return url;
        }


        /// <summary>
        /// 使用Access Token来获取用户的OpenID
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        private UserInfoModel GetUserInfo(dynamic token, string openId)
        {
            string parms = "?access_token=" + token["access_token"] + "&openid=" + openId;

            string url = Option.UserInformationEndpoint + parms;
            string str = GetRequest(url);

            var user = JsonConvert.DeserializeObject<UserInfoModel>(str);

            return user;
        }

        public UserInfoModel GetUserInfo(string accessToken, string openId)
        {
            string parms = "?access_token=" + accessToken + "&openid=" + openId + "&lang=zh-CN";

            string url = Option.UserInformationEndpoint + parms;
            string str = GetRequest(url);

            UserInfoModel user = JsonConvert.DeserializeObject<UserInfoModel>(str);

            return user;
        }
    }
}
