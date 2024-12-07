using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution_BE_102024_Common
{
    public class Validation
    {
        public static bool CheckSting(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            int number;
            if (int.TryParse(input, out number)) //Kiểm tra chuỗi có phải số
            {
                return false;
            }

            if (input.Length > 100)
            {
                return false;
            }

            return true;
        }

        public static bool CheckNumber(string input) 
        {
            if (string.IsNullOrEmpty(input)) 
            {
                return false;
            }
            int number;
            if (!int.TryParse(input, out number)) //Kiểm tra và ép kiểu
            {
                return false;
            }
            return true;
        }

        public static bool CheckXSSInput(string input)
        {
            try
            { 
                var listdangerousString = new List<string>() { "<applet", "<body", "<embed", "<frame", 
                    "<script", "<frameset", "<html", "<iframe", "<img", "<style", "<layer", "<link", "<ilayer", 
                    "<meta", "<object", "<h", "<input", "<a", "&lt", "&gt" };
                if (string.IsNullOrEmpty(input))
                {
                    return false;
                }
                foreach (var dangerous in listdangerousString) 
                {
                    if (input.Trim().ToLower().IndexOf(dangerous) >= 0) //Kiểm tra chuỗi danh sách
                                                                        //dangerous có tồn tại trong input không
                    {
                        return false;
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}
