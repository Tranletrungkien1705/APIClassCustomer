using APIClassCustomer.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using APIClassCustomer.Data;

namespace APIClassCustomer.Controllers
{
    [Route("api")]
    [ApiController]
    [EnableCors("MyRequestMVCAPI")]
    public class CustomerController : Controller
    {
            private readonly DbConnection db;
            public CustomerController(DbConnection db)
            {
                this.db = db;
            }

            // GET: api/<ClassController>
            [HttpGet]
            [Route("/[controller]/[action]")]
            public IEnumerable<Customer> Get()
            {
                IEnumerable<Customer> categories = db.Customer.ToArray();
                return categories;
            }

            // GET api/<ClassController>/5
            [HttpGet]
            [Route("/[controller]/[action]/{id}")]
            public Customer Get(int id)
            {
                Customer categories = db.Customer.Where(x => x.Id == id).First();
                return categories;
            }

            [HttpPost]
            [Route("/[controller]/[action]")]
            public IActionResult CreatNew(Customer class1)
            {
                bool flag = true;

                if (this.isDupplicated(class1.FullName))
                {
                    ModelState.AddModelError("CustomerDupplicated", "Customer is Dupplicated");
                    flag = false;
                }
                if (!ModelState.IsValid)
                {
                    flag = false;
                    return Ok(ModelState);
                }
                if (flag)
                {
                    List<Class> classes= db.Class.ToList();
                    if(class1.Password == class1.ConfirmPassword ||
                        classes.Where(x=>x.Id==class1.ClassId).First() != null
                    )
                    {
                        db.Customer.Add(class1);
                        db.SaveChanges();

                    }
                }
                return Ok(class1);
            }
            [HttpPut]
            [Route("/[controller]/[action]/{c}")]
            public List<Customer> DeleteCustomer(int c)
            {
                Customer customer = db.Customer.Where(x => x.Id == c).First();
                db.Customer.Remove(customer);
                db.SaveChanges();
                return db.Customer.ToList();
            }
            [HttpGet]
            [Route("/[controller]/[action]/{c}")]
            public IEnumerable<Customer> SearchByName(string c)
            {
                string[] x = c.Split(' ');
                List<Customer> category = new List<Customer>();
                List<Customer> cate = new List<Customer>();
                foreach (string s in x)
                {

                    category = db.Customer.Where(x => x.FullName.ToLower().Contains(s.ToLower()))
                        .ToList();
                    category.ForEach(x => cate.Add(x));
                }
                return cate;
            }
            [HttpGet]
            [Route("/[controller]/[action]/{keyword}")]
            public IEnumerable<Customer> SearchById(int c)
            {
                List<Customer> category = new List<Customer>();
                category = db.Customer.Where(x => x.Id.Equals(c))
                    .ToList();
                return category;
            }
            [HttpPut]
            [Route("/[controller]/[action]/{keyword}")]
            public List<Customer> UpdateById(Customer cate, int Id)
            {
                if (db.Customer.Where(x => x.FullName == cate.FullName).Count() == 0)
                {
                    Customer category = db.Customer.Where(x => x.Id == Id).First();
                    category.FullName = cate.FullName;
                    db.SaveChanges();

                }
                return db.Customer.ToList();

            }
            [HttpGet]
            [Route("/[controller]/[action]")]
            public bool isDupplicated(string? catName)
            {
                bool flag = true;
                Customer category1 = db.Customer.Where(category => category.FullName.ToLower().Equals(catName.ToLower())).FirstOrDefault();
                if (category1 is null) flag = false;
                return flag;
            }
        }
}
