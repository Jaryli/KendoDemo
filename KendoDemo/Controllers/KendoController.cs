using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace KendoDemo.Controllers
{
    public class KendoController : ApiController
    {
        [System.Web.Http.HttpPost]
        public JsonResult<List<ProductCombox>> Get(string parent)
        {
            var list = new List<ProductCombox> { new ProductCombox{ ProductId=1, ProductName="taobao" },
                new ProductCombox { ProductId=2,ProductName="jd"}
               };
            if (!string.IsNullOrEmpty(parent))
            {
                list = list.FindAll(s => s.ProductName.Contains(parent));
            }
            return Json(list);
        }
    }

    public class ProductCombox
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

    }
}
