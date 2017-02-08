namespace JcSoft.Framework.OAuth.Weibo
{
    public static class WeiboAuthorizeExtension
    {
        public static OAuthProvider UseWeChatAuthorize(this OAuthProvider provider)
        {
            provider.Register(new WeiboAuthorize());
            return provider;
        }

    }
}
