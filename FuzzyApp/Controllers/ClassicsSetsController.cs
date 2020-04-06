using FuzzyApp.Commons;
using FuzzyApp.Models;
using FuzzyApp.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ComplexApp.Controllers
{
    public class ClassicsSetsController : Controller
    {
        public static List<Preposition> resultComplete;
        public static string logicExpressionToShow;
        public ActionResult ClassicsSets()
        {
            System.Web.HttpContext.Current.Session["ResultComplete"] = new List<Preposition>();
            resultComplete = new List<Preposition>();

            return View();
        }

        public ActionResult evaluateLogicExpression(string logicExpression)
        {
            resultComplete.Clear();
            logicExpressionToShow = logicExpression;

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