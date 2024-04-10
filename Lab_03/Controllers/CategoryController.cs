﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab_03.Models;
using Lab_03.Repository;

namespace Lab_03.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            var category = await _categoryRepository.GetAllAsync();
            return View(category);
        }
        //public async Task<IActionResult> Add()
        //{
        //    var categories = await _categoryRepository.GetAllAsync();
        //    ViewBag.Categories = new SelectList(categories, "Id", "Name");
        //    return View();
        //}
        //// Xử lý thêm sản phẩm mới
        //[HttpPost]
        //public async Task<IActionResult> Add(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _categoryRepository.AddAsync(category);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
        //    var categories = await _categoryRepository.GetAllAsync();
        //    ViewBag.Categories = new SelectList(categories, "Id", "Name");
        //    return View(category);
        //}
        //public async Task<IActionResult> Update(int id)
        //{
        //    var category = await _categoryRepository.GetByIdAsync(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    var categories = await _categoryRepository.GetAllAsync();
        //    ViewBag.Categories = new SelectList(categories, "Id", "Name", category.Id);
        //    return View(category);
        //}
        //// Xử lý cập nhật sản phẩm
        //[HttpPost]
        //public async Task<IActionResult> Update(int id, Category category)
        //{
        //    if (id != category.Id)
        //    {
        //        return NotFound();
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        await _categoryRepository.UpdateAsync(category);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(category);
        //}
        //// Hiển thị form xác nhận xóa sản phẩm
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var category = await _categoryRepository.GetByIdAsync(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(category);
        //}
        //// Xử lý xóa sản phẩm
        //[HttpPost, ActionName("Delete")]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    await _categoryRepository.DeleteAsync(id);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
