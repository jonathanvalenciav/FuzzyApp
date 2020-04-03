using ComplexApp.Models;
using ComplexApp.Services;
using System.Web.Mvc;

namespace ComplexApp.Controllers
{
    public class SequencesController : Controller
    {
        // GET: Sequences
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sequences()
        {

            var globalSequence = (Sequences)System.Web.HttpContext.Current.Session["Sequences"];
            var globalSequenceReverse = (Sequences)System.Web.HttpContext.Current.Session["SequencesReverse"];

            System.Web.HttpContext.Current.Session["Sequences"] = globalSequence != null ? System.Web.HttpContext.Current.Session["Sequences"] : new Sequences();
            System.Web.HttpContext.Current.Session["SequencesReverse"] = globalSequenceReverse != null ? System.Web.HttpContext.Current.Session["SequencesReverse"] : new Sequences();

            return View();
        }

        public ActionResult addNewElementToSequence(string newElement)
        {
            var sequencesServices = new SequencesServices();

            if (sequencesServices.validateInput(newElement)) {
                sequencesServices.addElementToSequence(sequencesServices.getValueFromInput(newElement));
            }

            return View("Sequences");
        }

        public ActionResult removeElement(int index)
        {
            var sequencesServices = new SequencesServices();
            sequencesServices.removeElementFromSequence(index);

            return View("Sequences");
        }
    }
}