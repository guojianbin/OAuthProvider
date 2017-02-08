using System.Collections;
using System.Web;

namespace JcSoft.Framework.OAuth
{
    public class OAuthProvider
    {
        private static object provider;
      
        static Hashtable Providers = new Hashtable();

        public void Register<TOAuthInstance>(TOAuthInstance instance) where TOAuthInstance:IOAuthInstance
        {
            Providers.Add(instance.Name, instance);
        }

        public TOAuthInstance GetInstance<TOAuthInstance>(string name) where TOAuthInstance : IOAuthInstance
        {
            return (TOAuthInstance) Providers[name];
        }
        public IOAuthInstance GetInstance(string name)
        {
            return Providers[name] as IOAuthInstance;
        }
    }

    public interface IOAuthInstance
    {
        string Name { get; set; }

        string AuthorizeUrl();

        dynamic Callback(HttpContext context,out string openId);

        void Login(HttpContext context);
    }

    public abstract class OAuthOption
    {
        public string AppId { get; set; }

        public string SecretKey { get; set; }

        public string Name { get; set; }

        public string RedirectUrl { get; set; }

        public string TokenEndpoint { get; set; }

        public string AuthorizationEndpoint { get; set; }

        public string UserInformationEndpoint { get; set; }
    }
}
