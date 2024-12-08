using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DataOpject
{
    public class User
    {
           public int UserID{get; set;} 
           public string? UserName{get; set;} 
           public string? PassWord{get; set;} 
           public int IsAdmin{get; set;} 
           public DateTime? TokenExprired{get; set;} 
           public string? RefeshToken{get; set;}
           public string? Roles { get; set;}
    }
}
