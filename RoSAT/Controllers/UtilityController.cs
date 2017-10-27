using SpellingCorrector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoSAT.Controllers
{
    public class UtilityController : Controller
    {
        [HttpGet]
        public ActionResult GetWordSuggestions(string word)
        {
            SpellingSuggestions obj = SpellingSuggestions.Instance;
            return Json(obj.GetSuggestionsForWord(word), JsonRequestBehavior.AllowGet);
        }
    }
}