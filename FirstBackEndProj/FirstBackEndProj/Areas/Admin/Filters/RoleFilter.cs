//using FirstBackEndProj.DAL;
//using FirstBackEndProj.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace FirstBackEndProj.Areas.Admin.Filters
//{
//    public class RoleFilter : ActionFilterAttribute
//    {
//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            string controllerName = filterContext.RouteData.Values["Controller"].ToString();
//            string actionName = filterContext.RouteData.Values["Action"].ToString();
//            var user = filterContext.HttpContext.Session["User"] as User;
//            if (user == null)
//            {
//                filterContext.Result = new RedirectResult("~/Home/Index");
//                return;
//            }

//            var hasAccess = new HasAccess(user, filterContext);


//            if (!hasAccess)
//            {
//                filterContext.Result = new RedirectResult(HttpContext.Current.Request.UrlReferrer.ToString());
//                return;
//            }
//            base.OnActionExecuting(filterContext);
//        }
//        private bool HasAccess(User user, ActionExecutingContext filterContext)
//        {
//            string controller = filterContext.RouteData.Values["Controller"].ToString();
//            string action = filterContext.RouteData.Values["Action"].ToString();

//            using (CoolContext db = new CoolContext())
//            {

//                var controllerFromDb = db.AuthActions.FirstOrDefault(a => a.Name == controller);
//                if (controllerFromDb != null)
//                {

//                    var permission = db.AuthPermissions.FirstOrDefault(p => p.AuthGroupId == user.AuthGroupId && p.AuthActionId == controllerFromDb.Id);
//                    if (permission != null)
//                    {
//                        if (permission.OnlyOwner)
//                        {
//                            filterContext.HttpContext.Session[controller + "-" + action + "-owner"] = true;

//                        }
//                        switch (action)
//                        {
//                            case "Index":
//                                if (permission.CanView)
//                                {
//                                    return true;
//                                }
//                                break;
//                            case "Create":
//                                if (permission.CanCreate)
//                                {
//                                    return true;
//                                }
//                                break;
//                            case "Edit":
//                                if (permission.CanEdit)
//                                {
//                                    return true;
//                                }
//                                break;
//                            case "Delete":
//                                if (permission.CanDelete)
//                                {
//                                    return true;
//                                }
//                                break;
//                            default:
//                                break;
//                        }

//                    }
//                }
//            }
//            return false;
//        }
//    }
//}
