using System;
using System.Web;

namespace EducationPortal.Models
{
    public static class Authorization
    {
        static public bool IsAuthorized
        {
            get
            {
                return IsAdmin || IsUser;
            }
            set
            {
                IsAdmin = false;
                IsUser = false;
                Login = null;
            }
        }

        static public string Login
        {
            get
            {
                return HttpContext.Current.Session["Login"]?.ToString();
            }
            set
            {
                HttpContext.Current.Session["Login"] = value;
            }
        }

        static public bool IsUser
        {
            get
            {
                if (HttpContext.Current.Session["isUser"] == null)
                {
                    HttpContext.Current.Session["isUser"] = false;
                }

                return Convert.ToBoolean(HttpContext.Current.Session["isUser"]);
            }
            set
            {
                HttpContext.Current.Session["isUser"] = value;
            }
        }

        static public bool IsAdmin
        {
            get
            {
                if (HttpContext.Current.Session["isAdmin"] == null)
                {
                    HttpContext.Current.Session["isAdmin"] = false;
                }

                return Convert.ToBoolean(HttpContext.Current.Session["isAdmin"]);
            }
            set
            {
                HttpContext.Current.Session["isAdmin"] = value;
            }
        }
    }
}