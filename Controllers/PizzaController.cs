using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Forms;
using la_mia_pizzeria_static.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        PizzeriaDbContext db;

        IDbPizzeriaRepository pizzeriaRepository;


        public PizzaController() : base ()
        {
            db = new PizzeriaDbContext();

            int dbError = new Random().Next(1, 11);

            if (dbError > 5)
            {
                pizzeriaRepository = new InMemoryPizzaRepository();
            } else
            {
                pizzeriaRepository= new DbPizzeriaRepository();
            }
        } 

        public IActionResult Index()
        {

            List<Pizza> listaPizze = pizzeriaRepository.All();


            return View(listaPizze);
        }

        public IActionResult Detail(int id)
        {
            PizzeriaDbContext db = new PizzeriaDbContext();

            Pizza pizza = pizzeriaRepository.GetById(id);
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

                formData.Ingredients = new List<SelectListItem>();

                List<Ingredient> ingredientList = db.Ingredients.ToList();

                foreach (Ingredient ingredient in ingredientList)
                {
                    formData.Ingredients.Add(new SelectListItem(ingredient.Name, ingredient.Id.ToString()));
                }

                return View(formData);
            }

            

            pizzeriaRepository.Create(formData.Pizza, formData.SelectedIngredients);

            return RedirectToAction("Index");
        }


        public IActionResult Update(int id)
        {
            
            Pizza pizza = pizzeriaRepository.GetById(id);

            if (pizza == null)
            {
                return NotFound();
            }

            PizzaForm formData = new PizzaForm();
            formData.Pizza = pizza;
            formData.Categories = db.Categories.ToList();
            formData.Ingredients = new List<SelectListItem>();

            List<Ingredient> ingredientList = db.Ingredients.ToList();

            foreach (Ingredient ingredient in ingredientList)
            {
                formData.Ingredients.Add(new SelectListItem(
                    ingredient.Name,
                    ingredient.Id.ToString(),
                    pizza.Ingredients.Any(i => i.Id == ingredient.Id)
                    ));
            }

            return View(formData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaForm formData)
        {

            if (!ModelState.IsValid)
            {
                formData.Pizza.PizzaId = id;
                formData.Categories = db.Categories.ToList();
                formData.Ingredients = new List<SelectListItem>();

                List<Ingredient> ingredientList = db.Ingredients.ToList();

                foreach (Ingredient ingredient in ingredientList)
                {
                    formData.Ingredients.Add(new SelectListItem(ingredient.Name, ingredient.Id.ToString()));
                }

                return View(formData);
            }

            Pizza pizzaitem = pizzeriaRepository.GetById(id);

            if (pizzaitem == null)
            {
                return NotFound();
            }

            pizzeriaRepository.Update(pizzaitem, formData.Pizza, formData.SelectedIngredients);

            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza pizza = pizzeriaRepository.GetById(id);

            if (pizza == null)
            {
                return NotFound();
            }

            pizzeriaRepository.Delete(pizza);


            return RedirectToAction("Index");
        }
    }
}

