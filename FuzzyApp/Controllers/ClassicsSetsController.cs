using FuzzyApp.Commons;
using FuzzyApp.Services;
using System.Web.Mvc;

namespace ComplexApp.Controllers
{
    public class ClassicsSetsController : Controller
    {
        public ActionResult ClassicsSets()
        {
            var globalPostfix = (string)System.Web.HttpContext.Current.Session["Postfix"];
            System.Web.HttpContext.Current.Session["Postfix"] = Constants.EMPTY_STRING;

            return View();
        }

        public ActionResult evaluateLogicExpression(string logicExpression)
        {
            InputHandlerService.processInput(logicExpression);

            return View("ClassicsSets");
        }
    }
}