using ContosoPizza.Data;
using Microsoft.EntityFrameworkCore;
using ContosoPizza.Models;

namespace ContosoPizza.Services;

public class PizzaService
{
    private readonly PizzaContext _context;
    public PizzaService(PizzaContext context){
        _context = context;
    }

    public IEnumerable<Pizza> GetAll()
    {
        return _context.Pizzas
            .Include(p=>p.Toppings)
            .Include(p=>p.Sauce)
            .AsNoTracking()
            .ToList();
        //throw new NotImplementedException();
    }

    public Pizza? GetById(int id)
    {
        return _context.Pizzas
            .Include(p => p.Toppings)
            .Include(p => p.Sauce)
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);
        //throw new NotImplementedException();
    }

    public Pizza? Create(Pizza newPizza)
    {
        _context.Pizzas.Add(newPizza);
        _context.SaveChanges();

        return newPizza;
        //throw new NotImplementedException();
    }

    public void AddTopping(int PizzaId, int ToppingId)
    {        
        var pizzaToUpdate = _context.Pizzas.Find(PizzaId);
        var toppingToAdd = _context.Toppings.Find(ToppingId);

        if(pizzaToUpdate is null || toppingToAdd is null)
            throw new InvalidOperationException("Pizza or topping does not exist");

        if(pizzaToUpdate.Toppings is null)
            pizzaToUpdate.Toppings = new List<Topping>();

        pizzaToUpdate.Toppings.Add(toppingToAdd);

        _context.SaveChanges();
        //throw new NotImplementedException();
    }

    public void UpdateSauce(int PizzaId, int SauceId)
    {
        var pizzaToUpdate = _context.Pizzas.Find(PizzaId);
        var sauceToUpdate = _context.Sauces.Find(SauceId);

        if(pizzaToUpdate is null || sauceToUpdate is null)
            throw new InvalidOperationException("Pizza or sauce does not except");

        pizzaToUpdate.Sauce = sauceToUpdate;
        _context.SaveChanges();
        //throw new NotImplementedException();
    }

    public void DeleteById(int id){
        var pizzaToDelete = _context.Pizzas.Find(id);
        if(pizzaToDelete is not null){
            _context.Pizzas.Remove(pizzaToDelete);
            _context.SaveChanges();
        }
        //throw new NotImplementedException();
    }
}