using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    // Create
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        // _context.Products.Add(product);
        // await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    // Paginate
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int pageNumber = 1, int pageSize = 10)
    {
        // var products = await Products
        //     .Skip((pageNumber - 1) * pageSize)
        //     .Take(pageSize)
        //     .ToListAsync();

        string query = "";


        var products = new List<Product>(0);
        return products;
    }

    // Read
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        // var product = await _context.Products.FindAsync(id);
        var product = new Product();

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    // Update
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        // _context.Entry(product).State = EntityState.Modified;
        // await _context.SaveChangesAsync();

        return NoContent();
    }

    // Delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        // var product = await _context.Products.FindAsync(id);
        var product = new Product();

        if (product == null)
        {
            return NotFound();
        }

        // _context.Products.Remove(product);
        // await _context.SaveChangesAsync();

        return NoContent();
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}