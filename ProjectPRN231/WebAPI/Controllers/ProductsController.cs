using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using WebAPI.DataAccess;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public PRN231_DBContext _context;
        public ProductsController(PRN231_DBContext _context)
        {
            this._context = _context;
        }
        [HttpGet("hot")]
        public async Task<IActionResult> GetHotProducts()
        {
            List<Product> GetTopDiscountProducts = new List<Product>();
            var GetTopDiscountOrderDetails = await _context.OrderDetails.OrderByDescending(x => x.Discount).Take(4).ToListAsync();
            foreach (var order in GetTopDiscountOrderDetails)
            {
                Product p = GetProductById(order.ProductId);
                GetTopDiscountProducts.Add(GetProductById(order.ProductId));
            }
            return Ok(GetTopDiscountProducts);
        }

        public Product GetProductById(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
            return product;
        }
        [HttpGet]
        public async Task<IActionResult> GetBestSaleProducts()
        {

            List<Product> GetBestSaleProducts = new List<Product>();
            return Ok(GetBestSaleProducts);
        }

        [HttpGet("new")]
        public async Task<IActionResult> GetNewestProducts()
        {
            List<Product> list = await _context.Products.OrderByDescending(x => x.ProductId).Take(4).ToListAsync();
            return Ok(list);
        }
    }
}
