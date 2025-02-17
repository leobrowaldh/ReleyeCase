using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReleyeCase.Mvc.Models;
using Repositories;

namespace ReleyeCase.Mvc.Controllers;

public class HomeController(ICustomerRepository _customerRepo, ILogger<HomeController> _logger) : Controller
{
    public async Task<IActionResult> Index()
    {
        HomeIndexViewModel model = new()
        {
            Customers = await _customerRepo.GetCustomers()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomer(HomeIndexViewModel model)
    {
        if (ModelState.IsValid)
        {
            var createdCustomer = await _customerRepo.CreateCustomer(model.NewCustomer);
            _logger.LogInformation($"Customer {createdCustomer.CustomerId} created");
        }

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
