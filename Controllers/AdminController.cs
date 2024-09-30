using Microsoft.AspNetCore.Mvc;

namespace Mozzafiato.Controllers
{
    public class AdminController : Controller
    {
        public string Index() 
        {
            return $"Testando o método Index do AdminController: {DateTime.Now}";
        }
        public string Demo()
        {
            return $"Testando o método Demo do AdminController: {DateTime.Now}";
        }
    }
}
