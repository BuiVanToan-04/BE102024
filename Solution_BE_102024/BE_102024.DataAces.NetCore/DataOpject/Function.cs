using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DataOpject
{
    public class Function
    {
        [Key]
        public int FunctionID { get; set; }
        public string FunctionCode { get; set; }
        public string FunctioonName { get; set; }
    }
}
