# OAuthProvider
用于mvc项目简单实现第三方登陆。基础代码源于http://www.aspku.com/kaifa/net/127890.html

* 配置
```xml
    <!--新浪微博登录相关配置-->
    <add key="OAuth_Sina_AppKey" value="123456789" />
    <add key="OAuth_Sina_AppSecret" value="25f9e794323b453885f5181f1b624d0b" />
    <!--QQ登录相关配置-->
    <add key="OAuth_QQ_AppId" value="1105900017" />
    <add key="OAuth_QQ_AppKey" value="y7s4uLbkzbUttVdr" />
    <!--微信登录相关配置-->
    <add key="OAuth_Weixin_AppId" value="wx906c9aa9d539a1d0" />
    <add key="OAuth_Weixin_AppSecret" value="25f9e794323b453885f5181f1b624d0b" />	
    <!--OAuth RedirectUrl-->
    <add key="OAuth_RedirectUrl" value="http://www.app.com/account/unionlogin"/>
```

* 开启登陆
```c#
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            new OAuthProvider().UseWeChatAuthorize().UseQQAuthorize().UseWeiboAuthorize();
        }
```

* 登陆控制器
```c#
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
```