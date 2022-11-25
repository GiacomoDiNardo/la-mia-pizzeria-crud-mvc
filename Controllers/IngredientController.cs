using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace la_mia_pizzeria_static.Controllers
{
    public class IngredientController : Controller
    {
        PizzeriaDbContext db;

        public IngredientController() : base()
        {
            db = new PizzeriaDbContext();
        }
        public IActionResult Index()
        {
            List<Ingredient> listaIngredienti = db.Ingredients.ToList();
            

            return View(listaIngredienti);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            db.Ingredients.Add(ingredient);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //public IActionResult Update(int id)
        //{
        //    Ingredient ingredient = db.Ingredients.Find(id);

        //    if (ingredient == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ingredient);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Update(Ingredient ingredient)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }


        //    db.Ingredients.Add(ingredient);
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}
    }
}
