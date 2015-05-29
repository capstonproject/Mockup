using MockupProject_K30_DotNet.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockupProject_K30_DotNet.PresentationLayer.Model
{
    public class EmployeeDetail
    {
        public string EmployTemplate { get; set; }
        public List<string> Detail { get; set; }

        public EmployeeDetail()
        {
            EmployTemplate = "";
            Detail = new List<string>();
        }
    }
}
