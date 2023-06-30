using APIClassCustomer.Data;
using APIClassCustomer.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIClassCustomer.Controllers
{
    [Route("api")]
    [ApiController]
    [EnableCors("MyRequestMVCAPI")]
    public class ClassController : ControllerBase
    {
        private readonly DbConnection db;
        public ClassController(DbConnection db)
        {
            this.db = db;
        }

        // GET: api/<ClassController>
        [HttpGet]
        [Route("/[controller]/[action]")]
        public IEnumerable<Class> Get()
        {
            IEnumerable<Class> categories = db.Class.ToArray();
            return categories;
        }

        // GET api/<ClassController>/5
        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        public Class Get(int id)
        {
            Class categories = db.Class.Where(x=>x.Id==id).First();
            return categories;
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        public IActionResult CreatNew(Class class1)
        {
            bool flag = true;

            if (this.isDupplicated(class1.Name))
            {
                ModelState.AddModelError("ClassDupplicated", "Class is Dupplicated");
                flag = false;
            }
            if (!ModelState.IsValid)
            {
                flag = false;
                return Ok(ModelState);
            }
            if (flag)
            {
                db.Class.Add(class1);
                db.SaveChanges();
            }
            return Ok(class1);
        }
        [HttpPut]
        [Route("/[controller]/[action]/{c}")]
        public List<Class> DeleteClass(int c)
        {
            Class class1 = db.Class.Where(x => x.Id == c).First();
            db.Class.Remove(class1);
            db.SaveChanges();
            return db.Class.ToList();
        }
        [HttpGet]
        [Route("/[controller]/[action]/{c}")]
        public IEnumerable<Class> SearchByName(string c)
        {
            string[] x = c.Split(' ');
            List<Class> category = new List<Class>();
            List<Class> cate = new List<Class>();
            foreach (string s in x)
            {

                category = db.Class.Where(x => x.Name.ToLower().Contains(s.ToLower()))
                    .ToList();
                category.ForEach(x => cate.Add(x));
            }
            return cate;
        }
        [HttpGet]
        [Route("/[controller]/[action]/{keyword}")]
        public IEnumerable<Class> SearchById(int c)
        {
            List<Class> category = new List<Class>();
            category = db.Class.Where(x => x.Id.Equals(c))
                .ToList();
            return category;
        }
        [HttpPut]
        [Route("/[controller]/[action]/{keyword}")]
        public List<Class> UpdateById(Class cate, int Id)
        {
            if (db.Class.Where(x => x.Name == cate.Name).Count() == 0)
            {
                Class category = db.Class.Where(x => x.Id == Id).First();
                category.Name = cate.Name;
                db.SaveChanges();

            }
            return db.Class.ToList();

        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public bool isDupplicated(string? catName)
        {
            bool flag = true;
            Class category1 = db.Class.Where(category => category.Name.ToLower().Equals(catName.ToLower())).FirstOrDefault();
            if (category1 is null) flag = false;
            return flag;
        }
    }
}
