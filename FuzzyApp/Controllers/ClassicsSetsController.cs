using FuzzyApp.Commons;
using FuzzyApp.Models;
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
            System.Web.HttpContext.Current.Session["Result"] = new Preposition('.');

            return View();
        }

        public ActionResult evaluateLogicExpression(string logicExpression)
        {
            if (InputHandlerService.validateExpression(logicExpression))
            {
                InputHandlerService.processInput(logicExpression);
                ViewBag.ShowError = "hide";
            }
            else
            {
                ViewBag.ShowError = "show";
                ViewBag.Message = "Probably you are breaking the expressions rules. Please, check the number of prepositions and the number of uses.";
            }

            return View("ClassicsSets");
        }
    }
}