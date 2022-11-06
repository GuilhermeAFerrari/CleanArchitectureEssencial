using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers;

//[Authorize]
public class ProductsController : Controller
{
	private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IWebHostEnvironment _environment;

    public ProductsController(
        IProductService productService,
        ICategoryService categoryService,
        IWebHostEnvironment enviroment)
	{
		_productService = productService;
        _categoryService = categoryService;
        _environment = enviroment;
	}

	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var products = await _productService.GetProductsAsync();
		return View(products);
	}

	[HttpGet]
	public async Task<IActionResult> Create()
	{
        ViewBag.Categories = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
        return View();
	}

	[HttpPost]
	public async Task<IActionResult> Create(ProductDTO product)
	{
		if (ModelState.IsValid)
		{
			await _productService.AddAsync(product);
			return RedirectToAction(nameof(Index));
		}
		return View(product);
	}

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
		if (id is null) return NotFound();

        var productDto = await _productService.GetByIdAsync(id);

        if (productDto is null) return NotFound();

        var categories = await _categoryService.GetCategoriesAsync();

        ViewBag.Categories = new SelectList(categories, "Id", "Name", productDto.CategoryId);

        return View(productDto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductDTO productDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _productService.UpdateAsync(productDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }
        var categories = await _categoryService.GetCategoriesAsync();

        ViewBag.Categories = new SelectList(categories, "Id", "Name", productDto.CategoryId);

        return View(productDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var productDto = await _productService.GetByIdAsync(id);

        if (productDto is null) return NotFound();

        return View(productDto);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.RemoveAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var productDto = await _productService.GetByIdAsync(id);

        if (productDto is null) return NotFound();

        var wwwroot = _environment.WebRootPath;
        var image = Path.Combine(wwwroot, "images\\" + productDto.Image);
        var exists = System.IO.File.Exists(image);
        ViewBag.ImageExist = exists;

        return View(productDto);
    }
}
