using MockupProject_K30_DotNet.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockupProject_K30_DotNet.PresentationLayer.Model
{
    public class FSU
    {
        public string FsuName { get; set; }
        public List<Employee> Employeees { get; set; }
        public FSU()
        {
            Employeees = new List<Employee>();
        }

    }
}
