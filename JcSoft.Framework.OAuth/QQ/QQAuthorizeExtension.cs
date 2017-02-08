namespace JcSoft.Framework.OAuth.QQ
{
    public static class QQAuthorizeExtension
    {
        public static void UseQQAuthorize(this OAuthProvider provider)
        {
            provider.Register(new QQAuthorize());
        }

    }
}
