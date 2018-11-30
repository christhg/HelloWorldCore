using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
using Microsoft.AspNetCore.Mvc;//引入命名空间 Microsoft.AspNetCore.Mvc，并修改 HomeController 继承自 Controller。
using HelloWord.Models;

namespace HelloWord.Controllers
{
    public class HomeController: Controller
    {
        public HomeController()
        {
        }

        //public string Index()
        //{
        //    return "你好，世界! 此消息来自 HomeController...";
        //}

        //01.使用ContentResult 是实现了 ActionResult 接口的不同结果类型之一
        public ContentResult Index()
        {

            //Content() 方法会产生一个 ContentResult
            return Content("你好，世界! 此消息来自使用了 Action Result 的 Home 控制器");
        }

        //02.使用ObjectReult
        public ObjectResult Index2()
        {
            var employee = new Employee { ID = 1, Name = "语飞" };
            //返回一个 ObjectResult 时，MVC 框架将访问这个对象。并将这个对象做一些转换，
            //然后作为 HTTP 响应返回给客户端
            return new ObjectResult(employee);
        }

        //03.使用View
        public ViewResult Index3()
        {
            var employee = new Employee { ID = 1, Name = "语飞" };
            return View(employee);//將employee當作object參數傳入到View
        }

        //04
        public ViewResult Homepage()
        {
            var model = new HomePageViewModel();
            using(var context = new HelloWorldDBContext())
            {
                SQLEmployeeData sqldata = new SQLEmployeeData(context);
                model.Employees = sqldata.GetAll();
            }
            return View(model);
        }
    }
    
    public class SQLEmployeeData
    {
        private HelloWorldDBContext _context { get; set; }

        public SQLEmployeeData(HelloWorldDBContext context)
        {
            _context = context;
        }

        public void Add(Employee emp)
        {
            _context.Add(emp);
            _context.SaveChanges();
        }

        public Employee Get(int ID)
        {
            return _context.Employees.FirstOrDefault(e => e.ID == ID);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList<Employee>();
        }
    }

    public class HomePageViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
    }
}
