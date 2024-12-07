
using Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi10_Solid;
using Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi10_Solid.Interface;
using Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi9_Inteface;
using Solution_BE_102024_DataAcessNetFrameWork.Buoi8_Class_Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution_BE_102024_ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            //Buổi 8: Class-Abstract
            #region Class-Abstract
            //Bài 1
            //Console.WriteLine("Bài 1");
            //HinhChuNhat hcn = new HinhChuNhat(4, 5);
            //Console.WriteLine($"Diện tích hình chữ nhật: {hcn.DienTich()}");
            //Console.WriteLine($"Chu vi hình chữ nhật: {hcn.ChuVi()}");

            //HinhTron ht = new HinhTron(5);
            //Console.WriteLine($"Diện tích hình tròn: {ht.DienTich()}");
            //Console.WriteLine($"Chu vi hình tròn: {ht.ChuVi()}");
            //Console.WriteLine("-----------------------------------------------------------" +
            //    "--------------------------------------------------------");

            ////Bài 2
            //Console.WriteLine("Bài 2");
            //NhanVienFullTime nvF = new NhanVienFullTime("Nguyen van A", 23, 0.2f, 300000);
            //Console.WriteLine(nvF);

            //NhanVienPartTime nvP = new NhanVienPartTime("Nguyen van B", 20, 250000);
            //Console.WriteLine(nvP);
            //Console.WriteLine("-----------------------------------------------------------" +
            //    "--------------------------------------------------------");

            ////Bài 3
            //Console.WriteLine("Bài 3");
            //Bai3 b3 = new Phone("San Pham A", 500000,25000);
            //Console.WriteLine(b3);

            //Bai3 bai3 = new LapTop("San pham b", 1000000, 0.5f);
            //Console.WriteLine(bai3);
            #endregion

            //Buổi 9 
            #region Interface
            //IEmployeeOfStage employeeOfProduct = new EmployeeManager();
            //List<Product> products = new List<Product>();
            //Employee empl = new Employee();
            //while (true)
            //{
            //    Console.WriteLine("1. Thêm nhân viên (nhập tay hoặc từ file Excel)");
            //    Console.WriteLine("2. Hiển thị danh sách nhân viên");
            //    Console.WriteLine("3. Tạo sản lượng theo công đoạn");
            //    Console.WriteLine("4. Xuất báo cáo ra file Excel");
            //    Console.WriteLine("0. Thoát");
            //    Console.Write("Chọn chức năng: ");
            //    int choice = int.Parse(Console.ReadLine());

            //    switch (choice)
            //    {
            //        case 1:
            //            Console.Write("Chọn cách nhập (1: Bàn phím, 2: File Excel): ");
            //            string inputMethod = Console.ReadLine();
            //            if (inputMethod == "1")
            //            {
            //                Console.Write("Nhập ID: ");
            //                int id = int.Parse(Console.ReadLine());

            //                Console.Write("Nhập Họ và Tên: ");
            //                string fullName = Console.ReadLine();

            //                Console.Write("Nhập Giới Tính: ");
            //                string sex = Console.ReadLine();

            //                Console.Write("Nhập Tuổi: ");
            //                int age = int.Parse(Console.ReadLine());

            //                Console.Write("Nhập Lương Cơ Bản: ");
            //                double baseSalary = double.Parse(Console.ReadLine());

            //                Console.Write("Nhập Hệ Số Lương: ");
            //                float salaryCoefficient = float.Parse(Console.ReadLine());

            //                Console.Write("Nhập Phụ Cấp: ");
            //                double allowance = double.Parse(Console.ReadLine());

            //                Console.WriteLine("Số lượng Sản phẩm muốn thêm");
            //                int ProductNumber = int.Parse(Console.ReadLine());

            //                for (int i = 0; i < ProductNumber; i++)
            //                {
            //                    Console.WriteLine($"Nhập thông tin sản phẩm thứ {i + 1}");

            //                    Console.Write("Mã sản phẩm: ");
            //                    string productId = Console.ReadLine();

            //                    Console.Write("Tên sản phẩm: ");
            //                    string productName = Console.ReadLine();

            //                    Console.Write("Số lượng sản phẩm: ");
            //                    int productQuantity = int.Parse(Console.ReadLine());

            //                    var product = new Product(productId, productName, productQuantity);
            //                    products.Add(product);
            //                }

            //                //Tạo nhân viên
            //                var employee = new Employee(id, fullName, sex, age, baseSalary, salaryCoefficient, allowance);
            //                var result = employeeOfProduct.InsertEmployeeConsole(employee, products);
            //                Console.WriteLine(result.ResponseMessenger);
            //            }
            //            else if (inputMethod == "2")
            //            {
            //                Console.Write("Đường dẫn file Excel: ");
            //                string filePath = Console.ReadLine();
            //                Console.Write("Tên file Excel: ");
            //                string nameFile = Console.ReadLine();
            //                employeeOfProduct.ImportEmployeesExcel(filePath);
            //                Console.WriteLine("Đã nhập nhân viên từ file Excel.");
            //            }
            //            break;
            //        case 2:
            //            employeeOfProduct.ShowAllEmployees();
            //            break;

            //        case 3:
            //            Console.Write("ID nhân viên: ");
            //            if (int.TryParse(Console.ReadLine(), out int employeeId))
            //            {
            //                Employee emp = employeeOfProduct.FindEmployeeById(employeeId);

            //                if (emp != null)
            //                {
            //                    Console.Write("Mã công đoạn: ");
            //                    string productID = Console.ReadLine();
            //                    Console.Write("Tên công đoạn: ");
            //                    string productName = Console.ReadLine();
            //                    Console.Write("Số lượng sản phẩm: ");
            //                    if (int.TryParse(Console.ReadLine(), out int productQuantity))
            //                    {
            //                        Product step = new Product(productID, productName, productQuantity);
            //                        employeeOfProduct.GenerateProductionReport(emp, step);
            //                        Console.WriteLine("Đã tạo sản lượng cho nhân viên.");
            //                    }
            //                    else
            //                    {
            //                        Console.WriteLine("Số lượng sản phẩm không hợp lệ.");
            //                    }
            //                }
            //                else
            //                {
            //                    Console.WriteLine("Không tìm thấy nhân viên!");
            //                }
            //            }
            //            else
            //            {
            //                Console.WriteLine("ID nhân viên không hợp lệ.");
            //            }
            //            break;

            //        case 4:
            //            Console.Write("Đường dẫn xuất báo cáo Excel: ");
            //            string exportPath = Console.ReadLine();
            //            employeeOfProduct.ExportReportExcel(exportPath);
            //            Console.WriteLine("Đã xuất báo cáo.");
            //            break;

            //        case 0:
            //            return;

            //        default:
            //            Console.WriteLine("Lựa chọn không hợp lệ.");
            //            break;
            //    }
            //}
            #endregion

            //Buổi 10
            #region
            //var standardRoom1 = new StandardRoom(101);
            //var suiteRoom1 = new SuiteRoom(201);

            //// Thử thêm phòng Standard
            //var result1 = standardRoom1.InsertRoom(standardRoom1);
            //Console.WriteLine($"Result: {result1.ResponseMessenger}");

            //// Thử thêm phòng Suite
            //var result2 = suiteRoom1.InsertRoom(suiteRoom1);
            //Console.WriteLine($"Result: {result2.ResponseMessenger}");

            //// Hiển thị danh sách các phòng đã thêm
            //Console.WriteLine("\nDanh sách phòng:");
            //foreach (var room in Room.listRoom)
            //{
            //    Console.WriteLine($"Room Number: {room.RoomNumber}, Room Type: {room.RoomType}, Available: {room.IsAvailable}");
            //}

            //// Tạo đối tượng BookingService với RoomRepository
            //IRoomRepository roomRepository = new RoomRepository();
            //BookingService bookingService = new BookingService(roomRepository);

            //// Tạo đặt phòng
            //bool room1 = bookingService.BookingCreation(new Booking(1, "Khách hàng A", 101, DateTime.Now, DateTime.Now.AddDays(1)));
            //Console.WriteLine(room1 ? "Đặt thành công" : "Đặt thất bại");

            //// Hủy đặt phòng
            //room1 = bookingService.BookingCancellation(new Booking(1, "Khách hàng A", 101, DateTime.Now, DateTime.Now.AddDays(1)));
            //Console.WriteLine(room1 ? "Hủy thành công" : "Hủy thất bại");
            #endregion
        }
    }
}
