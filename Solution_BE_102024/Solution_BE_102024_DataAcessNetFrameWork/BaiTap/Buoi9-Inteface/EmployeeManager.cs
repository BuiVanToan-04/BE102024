using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Solution_BE_102024_Common;
using Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi9_Inteface.Bussiness;
//using Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi9_Inteface.Bussiness;

namespace Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi9_Inteface
{
    public class EmployeeManager : IEmployeeOfStage
    {

        private List<Employee> Listemployees = new List<Employee>();
        //Input từ bàn phím
        public ProductData InsertEmployeeConsole(Employee employee, List<Product> products)
        {
            var returnData = new ProductData();

            try
            {
                // Kiểm tra ID
                if (employee.ID < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "ID không hợp lệ";
                    return returnData;
                }

                // Kiểm tra Name
                if (!Validation.CheckSting(employee.EmployeeFullName))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.ProductNameNull;
                    returnData.ResponseMessenger = "Name không hợp lệ";
                    return returnData;
                }

                if (!Validation.CheckXSSInput(employee.EmployeeFullName))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.InvalidXSSInput;
                    returnData.ResponseMessenger = "Name không hợp lệ";
                    return returnData;
                }

                // Kiểm tra xem tên nhân viên đã tồn tại chưa
                var isExits = Listemployees.Any(s => s.EmployeeFullName == employee.EmployeeFullName);
                if (isExits)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.Dupplicate;
                    returnData.ResponseMessenger = "Name đã tồn tại";
                    return returnData;
                }

                // Kiểm tra giới tính
                if (!Validation.CheckSting(employee.EmployeeSex))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Giới tính không hợp lệ";
                    return returnData;
                }
                if (!Validation.CheckXSSInput(employee.EmployeeSex))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.InvalidXSSInput;
                    returnData.ResponseMessenger = "Giới tính không hợp lệ";
                    return returnData;
                }

                // Kiểm tra tuổi
                if (employee.Age <= 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Tuổi không hợp lệ";
                    return returnData;
                }

                // Kiểm tra lương cơ bản
                if (employee.BaseSalary < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Lương cơ bản không hợp lệ";
                    return returnData;
                }

                // Kiểm tra hệ số lương
                if (employee.SalaryCoefficient < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Hệ số lương không hợp lệ";
                    return returnData;
                }

                // Kiểm tra phụ cấp
                if (employee.Allowance < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Phụ cấp không hợp lệ";
                    return returnData;
                }

                //Kiểm tra sản phẩm 
                foreach (var product in products)
                {
                    // Kiểm tra mã sản phẩm
                    if (!Validation.CheckSting(product.ProductID))
                    {
                        returnData.ReponseCode = (int)ProductInsertStatus.ProductNameNull;
                        returnData.ResponseMessenger = $"Mã sản phẩm {product.ProductID} không hợp lệ";
                        return returnData;
                    }

                    if (!Validation.CheckXSSInput(product.ProductID))
                    {
                        returnData.ReponseCode = (int)ProductInsertStatus.InvalidXSSInput;
                        returnData.ResponseMessenger = $"Mã sản phẩm {product.ProductID} không hợp lệ";
                        return returnData;
                    }

                    // Kiểm tra tên sản phẩm
                    if (!Validation.CheckSting(product.ProductName))
                    {
                        returnData.ReponseCode = (int)ProductInsertStatus.ProductNameNull;
                        returnData.ResponseMessenger = $"Tên sản phẩm {product.ProductName} không hợp lệ";
                        return returnData;
                    }

                    // Kiểm tra số lượng sản phẩm
                    if (product.ProductQuantity < 0)
                    {
                        returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                        returnData.ResponseMessenger = $"Số lượng sản phẩm {product.ProductName} không hợp lệ";
                        return returnData;
                    }

                    employee.AddProduct(product);
                }

                //Tạo đối tượng sản phẩm
                Listemployees.Add(employee);
                returnData.ReponseCode = (int)ProductInsertStatus.Success;
                returnData.ResponseMessenger = "Thêm nhân viên thành công";
                return returnData;
            }
            catch (Exception ex)
            {
                returnData.ReponseCode = -99;
                returnData.ResponseMessenger = ex.StackTrace;
                return returnData;
            }
        }



        public void ExportReportExcel(string path)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.AddWorksheet("Report");
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "Gender";
                worksheet.Cell(1, 4).Value = "Age";
                worksheet.Cell(1, 5).Value = "TotalSalary";
                worksheet.Cell(1, 6).Value = "ProductID";
                worksheet.Cell(1, 7).Value = "ProductName";
                worksheet.Cell(1, 8).Value = "ProductQuantity";

                int row = 2;

                foreach (var employee in Listemployees)
                {
                    int totalProductQuantity = 0;
                    double totalSalary = 0;

                    foreach (var product in employee.ListProduct)
                    {
                        worksheet.Cell(row, 1).Value = employee.ID;
                        worksheet.Cell(row, 2).Value = employee.EmployeeFullName;
                        worksheet.Cell(row, 4).Value = employee.EmployeeSex;
                        worksheet.Cell(row, 3).Value = employee.Age;
                        worksheet.Cell(row, 5).Value = employee.TotalSalary;
                        worksheet.Cell(row, 6).Value = product.ProductID;
                        worksheet.Cell(row, 7).Value = product.ProductName;
                        worksheet.Cell(row, 8).Value = product.ProductQuantity;

                        totalProductQuantity += product.ProductQuantity;
                        totalSalary += employee.TotalSalary;

                        row++;
                    }
                    worksheet.Cell(row, 5).Value = totalSalary;
                    worksheet.Cell(row, 8).Value = totalProductQuantity;

                    row++;
                }

                workbook.SaveAs(path);
            }
        }



        public Employee FindEmployeeById(int id)
        {
            var result = from empl in Listemployees
                         where empl.FindEmploy(id)
                         select empl;
            return result.FirstOrDefault();
        }

        public void GenerateProductionReport(Employee employee, Product product)
        {
            employee.ListProduct.Add(product);
        }

        public ProductData ImportEmployeesExcel(string path)
        {
            var returnData = new ProductData();

            try
            {
                using (var workbook = new XLWorkbook(path))
                {
                    var worksheet = workbook.Worksheet(1);
                    int rowCount = worksheet.RowsUsed().Count();
                    for (int i = 2; i <= rowCount; i++)
                    {
                        int ID;
                        string id = worksheet.Cell(i, 1).GetValue<string>();
                        if (!int.TryParse(id, out ID) || ID < 0)
                        {
                            returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                            returnData.ResponseMessenger = "Dữ liệu ID không hợp lệ";
                            return returnData;
                        }

                        string name = worksheet.Cell(i, 2).GetValue<string>();
                        if (!Validation.CheckSting(name))
                        {
                            returnData.ReponseCode = (int)ProductInsertStatus.ProductNameNull;
                            returnData.ResponseMessenger = "Name không được null!";
                            return returnData;
                        }
                        if (!Validation.CheckSting(name))
                        {
                            returnData.ReponseCode = (int)ProductInsertStatus.InvalidXSSInput;
                            returnData.ResponseMessenger = "Name không hợp lệ";
                            return returnData;
                        }

                        string gioitinh = worksheet.Cell(i, 3).GetValue<string>();
                        if (!Validation.CheckSting(gioitinh))
                        {
                            returnData.ReponseCode = (int)ProductInsertStatus.ProductNameNull;
                            returnData.ResponseMessenger = "Gioi tính không được null!";
                            return returnData;
                        }
                        if (!Validation.CheckSting(gioitinh))
                        {
                            returnData.ReponseCode = (int)ProductInsertStatus.InvalidXSSInput;
                            returnData.ResponseMessenger = "Gioi tính không hợp lệ";
                            return returnData;
                        }

                        int age = worksheet.Cell(i, 4).GetValue<int>();
                        if (age < 0)
                        {
                            returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                            returnData.ResponseMessenger = "Dữ liệu Age không hợp lệ";
                            return returnData;
                        }

                        double baseSalary = worksheet.Cell(i, 5).GetValue<double>();
                        if (baseSalary < 0)
                        {
                            returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                            returnData.ResponseMessenger = "Dữ liệu BaseSalary không hợp lệ";
                            return returnData;
                        }

                        float salaryCoefficient = worksheet.Cell(i, 6).GetValue<float>();
                        if (salaryCoefficient < 0)
                        {
                            returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                            returnData.ResponseMessenger = "Dữ liệu SalaryCoeficient không hợp lệ";
                            return returnData;
                        }

                        double allowance = worksheet.Cell(i, 7).GetValue<double>();
                        if (allowance < 0)
                        {
                            returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                            returnData.ResponseMessenger = "Dữ liệu Allowance không hợp lệ";
                            return returnData;
                        }

                        Employee epl = new Employee(ID, name, gioitinh, age, baseSalary, salaryCoefficient, allowance);
                        Listemployees.Add(epl);

                    }
                    returnData.ReponseCode = (int)ProductInsertStatus.Success;
                    returnData.ResponseMessenger = "Thêm nhân viên từ Excel thành công!";
                    return returnData;
                }
            }
            catch (Exception ex)
            {
                returnData.ReponseCode = -99;
                returnData.ResponseMessenger = ex.StackTrace;
                
            }
            return returnData;
        }

        public void ShowAllEmployees()
        {
            if (Listemployees.Count == 0)
            {
                Console.WriteLine("Không có nhân viên nào trong danh sách.");
                return;
            }

            Console.WriteLine("Danh sách nhân viên và sản phẩm của họ:");
            Console.WriteLine($"{"ID",-10}{"Name",-20}{"Giới Tính",-10}{"Tuổi",-5}{"Lương Cơ Bản",-15}{"Hệ Số Lương",-15}{"Phụ Cấp",-10}{"Tổng Lương",-15}");

            foreach (var employee in Listemployees)
            {
                Console.WriteLine($"{employee.ID,-10}{employee.EmployeeFullName,-20}{employee.EmployeeSex,-10}{employee.Age,-5}{employee.BaseSalary,-15}" +
                    $"{employee.SalaryCoefficient,-15}{employee.Allowance,-10}{employee.TotalSalary,-15}");
                if (employee.ListProduct.Count > 0)
                {
                    Console.WriteLine("\tDanh sách sản phẩm của nhân viên:");
                    Console.WriteLine($"\t{"ProductID",-15}{"ProductName",-20}{"ProductQuantity",-15}");

                    foreach (var product in employee.ListProduct)
                    {
                        Console.WriteLine($"\t{product.ProductID,-15}{product.ProductName,-20}{product.ProductQuantity,-15}");
                    }
                }
                else
                {
                    Console.WriteLine("Không có sản phẩm nào.");
                }

                Console.WriteLine();
            }
        }

        ProductData IEmployeeOfStage.InsertEmployeeConsole(Employee employee, List<Product> products)
        {
            throw new NotImplementedException();
        }

        ProductData IEmployeeOfStage.ImportEmployeesExcel(string path)
        {
            throw new NotImplementedException();
        }
    }
}
