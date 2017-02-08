# OAuthProvider
用于mvc项目简单实现第三方登陆。基础代码源于http://www.aspku.com/kaifa/net/127890.html

* 开启登陆

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            new OAuthProvider().UseWeChatAuthorize().UseQQAuthorize().UseWeiboAuthorize();
        }

* 登陆控制器

    public class AccountController : Controller
    {
        public ActionResult OLogin(string type)
        {
            var client = new OAuthProvider().GetInstance(type);
            client.Login(System.Web.HttpContext.Current);
            return new EmptyResult();
        }

        public ActionResult UnionLogin()
        {
            string state = Request["state"];
            var client = new OAuthProvider().GetInstance(state);
            string openId = "";
            var token = client.Callback(System.Web.HttpContext.Current, out openId);

            //... bisiness logic

            return View();
        }
    }
