using Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi9_Inteface.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi9_Inteface
{
    public interface IEmployeeOfStage
    {
        ProductData InsertEmployeeConsole(Employee employee, List<Product> products);
        ProductData ImportEmployeesExcel(string path);
        Employee FindEmployeeById(int id);
        void GenerateProductionReport(Employee employee, Product product);
        void ExportReportExcel(string path);
        void ShowAllEmployees();
    }
}
