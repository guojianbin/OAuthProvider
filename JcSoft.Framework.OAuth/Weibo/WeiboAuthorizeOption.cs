namespace JcSoft.Framework.OAuth.Weibo
{
    public class WeiboAuthorizeOption : OAuthOption
    {
        public const string GET_AUTH_CODE_URL = "https://api.weibo.com/oauth2/authorize";
        public const string GET_ACCESS_TOKEN_URL = "https://api.weibo.com/oauth2/access_token";
        public const string GET_UID_URL = "https://api.weibo.com/2/account/get_uid.json";


        public WeiboAuthorizeOption(string appid, string key)
        {
            AppId = appid;
            SecretKey = key;
            TokenEndpoint = GET_ACCESS_TOKEN_URL;
            AuthorizationEndpoint = GET_AUTH_CODE_URL;
            UserInformationEndpoint = GET_UID_URL;
        }
        
        public string Scope { get; set; }

        public string State { get; set; }
    }
}
