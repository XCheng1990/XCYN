﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using XCYN.Web.Model;

namespace XCYN.Web.Pages.redis
{
    /// <summary>
    /// UserHandler 的摘要说明
    /// </summary>
    public class UserHandler : IHttpHandler, IRequiresSessionState
    {
        

        public void ProcessRequest(HttpContext context)
        {
            string UserName = context.Request["UserName"];
            if (!string.IsNullOrEmpty(UserName))
            {
                //记录登陆状态
                XUser user = new XUser()
                {
                    UserName = UserName,
                    ID = 1,
                    Age = 12
                };
                context.Session.Add("UserInfo", user);
                context.Response.Redirect("Index.aspx");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}