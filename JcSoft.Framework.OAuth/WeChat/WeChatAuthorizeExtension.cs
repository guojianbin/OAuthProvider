namespace JcSoft.Framework.OAuth.WeChat
{
    public static class WeChatAuthorizeExtension
    {
        public static OAuthProvider UseWeChatAuthorize(this OAuthProvider provider)
        {
            provider.Register(new WeChatAuthorize());
            return provider;
        }

    }
}
