namespace la_mia_pizzeria_static.Models.Repository
{
    public class InMemoryPizzaRepository : IDbPizzeriaRepository
    {
        public static List<Pizza> Pizzas = new List<Pizza>();

        public List<Pizza> All()
        {
            return Pizzas;
        }

        public void Create(Pizza pizza, List<int> selectedIngredients)
        {
            pizza.PizzaId = Pizzas.Count;
            pizza.Category = new Category() { Id = 1, Title = "Fake Category" };

            pizza.Ingredients = new List<Ingredient>();

            foreach (int ingredientId in selectedIngredients)
            {
                pizza.Ingredients.Add(new Ingredient() { Id = ingredientId, Name = "Fake ingredient" + ingredientId });
            }

            Pizzas.Add(pizza);
        }

        public void Delete(Pizza pizza)
        {
            Pizzas.Remove(pizza);
        }

        public Pizza GetById(int id)
        {
            return Pizzas.Where(pizza => pizza.PizzaId == id).FirstOrDefault();
        }

        public void Update(Pizza pizza, Pizza formData, List<int> selectedIngredients)
        {
            pizza = formData;
            pizza.Category = new Category() { Id = 1, Title = "Fake Category" };

            pizza.Ingredients = new List<Ingredient>();

            foreach (int ingredientId in selectedIngredients)
            {
                pizza.Ingredients.Add(new Ingredient() { Id = ingredientId, Name = "Fake ingredient" + ingredientId });
            }

        }
    }
}
