namespace JcSoft.Framework.OAuth.QQ
{
    public class QQAuthorizeOption :OAuthOption
    {
        public const string GET_AUTH_CODE_URL = "https://graph.qq.com/oauth2.0/authorize";
        public const string GET_ACCESS_TOKEN_URL = "https://graph.qq.com/oauth2.0/token";
        public const string GET_OPENID_URL = "https://graph.qq.com/oauth2.0/me";

        public QQAuthorizeOption(string appid, string key)
        {
            AppId = appid;
            SecretKey = key;

            TokenEndpoint = GET_ACCESS_TOKEN_URL;
            AuthorizationEndpoint = GET_AUTH_CODE_URL;
            UserInformationEndpoint = GET_OPENID_URL;
        }
        
        public string Scope { get; set; }

        public string State { get; set; }
    }
}
