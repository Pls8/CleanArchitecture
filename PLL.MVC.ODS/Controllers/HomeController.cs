using BLL.ODS.Repositories;
using DAL.ODS.Models.Products;
using Microsoft.AspNetCore.Mvc;
using PLL.MVC.ODS.Models;
using SLL.ODS.Interfaces;
using System.Diagnostics;

namespace PLL.MVC.ODS.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;


    public HomeController(
        IProductService productService,
        ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetActiveProductsAsync();
        var categories = await _categoryService.GetActiveCategoriesAsync();

        ViewBag.Categories = categories;
        return View(products);
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
