namespace JcSoft.Framework.OAuth.WeChat
{
    public class WeChatAuthorizeOption :OAuthOption
    {
        public const string GET_AUTH_CODE_URL = "https://open.weixin.qq.com/connect/qrconnect";
        public const string GET_ACCESS_TOKEN_URL = "https://api.weixin.qq.com/sns/oauth2/access_token";
        public const string GET_USERINFO_URL = "https://api.weixin.qq.com/sns/userinfo";

        public WeChatAuthorizeOption(string appid, string key)
        {
            AppId = appid;
            SecretKey = key;

            TokenEndpoint = GET_ACCESS_TOKEN_URL;
            AuthorizationEndpoint = GET_AUTH_CODE_URL;
            UserInformationEndpoint = GET_USERINFO_URL;
        }
        
        public string Scope { get; set; }

        public string State { get; set; }
    }
}
