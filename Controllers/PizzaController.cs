using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        PizzeriaDbContext db;

        public PizzaController() : base ()
        {
            db = new PizzeriaDbContext();
        }

        public IActionResult Index()
        {
            
            List<Pizza> listaPizze = db.Pizze.Include(pizza => pizza.Category).ToList();


            return View(listaPizze);
        }

        public IActionResult Detail(int id)
        {
            PizzeriaDbContext db = new PizzeriaDbContext();

            Pizza pizza = db.Pizze.Where(p => p.PizzaId == id).FirstOrDefault();
            return View(pizza);
        }

        public IActionResult Create()
        {
            PizzaForm formData = new PizzaForm();

            formData.Pizza = new Pizza();
            formData.Categories = db.Categories.ToList();
            formData.Ingredients = new List<SelectListItem>();

            List<Ingredient> ingredientList = db.Ingredients.ToList();

            foreach (Ingredient ingredient in ingredientList)
            {
                formData.Ingredients.Add(new SelectListItem(ingredient.Name, ingredient.Id.ToString()));
            }



            return View(formData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaForm formData)
        {
            if (!ModelState.IsValid)
            {
                formData.Categories = db.Categories.ToList();
                return View(formData);
            }

            db.Pizze.Add(formData.Pizza);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Update(int id)
        {
            
            Pizza pizza = db.Pizze.Where(pizza => pizza.PizzaId == id).FirstOrDefault();

            if (pizza == null)
            {
                return NotFound();
            }

            PizzaForm formData = new PizzaForm();
            formData.Pizza = pizza;
            formData.Categories = db.Categories.ToList();

            return View(formData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaForm formData)
        {

            if (!ModelState.IsValid)
            {
                formData.Categories = db.Categories.ToList();
                return View(formData);
            }

            Pizza pizzaitem = db.Pizze.Where(pizza => pizza.PizzaId == id).FirstOrDefault();

            if (pizzaitem == null)
            {
                return NotFound();
            }

            pizzaitem.Name = formData.Pizza.Name;
            pizzaitem.Description = formData.Pizza.Description;
            pizzaitem.Image = formData.Pizza.Image;
            pizzaitem.Prezzo = formData.Pizza.Prezzo;
            pizzaitem.CategoryId = formData.Pizza.CategoryId;

            //update implicito
            //formData.Pizza.PizzaId = id;
            //db.Pizze.Update(formData.Pizza);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza pizza = db.Pizze.Where(post => post.PizzaId == id).FirstOrDefault();

            if (pizza == null)
            {
                return NotFound();
            }

            db.Pizze.Remove(pizza);
            db.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}

