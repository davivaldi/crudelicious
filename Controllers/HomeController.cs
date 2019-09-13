using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CruDelicious.Models;
using Microsoft.EntityFrameworkCore;


namespace CruDelicious.Controllers
{
    public class HomeController : Controller
    {
         private CruDeliciousContext dbContext;
     
        public HomeController(CruDeliciousContext context)
        {
            dbContext = context;
        }


        public IActionResult Index()
        {
            List<Dishes> AllDishes = dbContext.Dishes.OrderByDescending(des =>des.CreatedAt).ToList();
            foreach (Dishes item in AllDishes)
            {
                System.Console.WriteLine(item.Name);
            }
            return View(AllDishes);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create(Dishes newDish)
        {
            Console.WriteLine(newDish);
            Console.WriteLine(newDish.Chef);
            Console.WriteLine(newDish.Name);
            Console.WriteLine(newDish.Calories);
            Console.WriteLine(newDish.Tastiness);
            Console.WriteLine(newDish.Description);

            if(ModelState.IsValid)
            {
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("New");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Dish(int id)
        {
            Dishes singleDish = dbContext.Dishes.SingleOrDefault(x => x.DishId == id);
            return View(singleDish);
        }

         [HttpGet("edit/{tempId}")]
        public IActionResult EditDish(int tempId)
        {
            System.Console.WriteLine("*****************************************************************************************************************************Im starting to edit here");
            Dishes singleDish = dbContext.Dishes.SingleOrDefault(x => x.DishId == tempId);
            return View(singleDish);

        }

        [HttpPost]
        [Route("edited")]
        public IActionResult Edit(Dishes editDish)
        {

            System.Console.WriteLine("***********************************************************************************************************************************************In the edited funtion");
            if(ModelState.IsValid)
            {
                dbContext.Dishes.Update(editDish);

                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditDish");
            }
        }

        [HttpGet("delete/{tempId}")]
        public IActionResult Delete(int tempId)
        {
            Dishes deleteDish = dbContext.Dishes.SingleOrDefault(x => x.DishId == tempId);
            dbContext.Dishes.Remove(deleteDish);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
