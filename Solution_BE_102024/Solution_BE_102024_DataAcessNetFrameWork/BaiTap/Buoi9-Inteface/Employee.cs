using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi9_Inteface
{
    public class Employee
    {
        private int _EmpployeeID { get; set; }

        private string _EmployeeFullName { get; set; }

        private string _EmployeSex { get; set; }

        private int _Age {  get; set; }
        
        private double _BaseSalary { get; set; } //Lương cơ bản

        private float _SalaryCoefficient { get; set; } //Hệ số lương

        private double _Allowance { get; set; } //Phụ Cấp

        private double _TotalSalary => _BaseSalary * _SalaryCoefficient + _Allowance;

        public List<Product> ListProduct { get; set; } = new List<Product>();

        public Employee() { }

        public Employee(int employeeID, string employeeFullName, string employeeSex, int age, 
            double baseSalary, float salaryCoefficient, double allowance)
        {
            _EmpployeeID = employeeID;
            _EmployeeFullName = employeeFullName;
            _EmployeSex = employeeSex;
            _Age = age;
            _BaseSalary = baseSalary;
            _SalaryCoefficient = salaryCoefficient;
            _Allowance = allowance;
        }

        public int ID => _EmpployeeID;

        public string EmployeeFullName => _EmployeeFullName;

        public string EmployeeSex => _EmployeSex;

        public int Age => _Age;

        public double BaseSalary => _BaseSalary;

        public float SalaryCoefficient => _SalaryCoefficient;

        public double Allowance => _Allowance;

        public double TotalSalary => _TotalSalary;

        public override string ToString() => $"Mã Nhân Viên: {_EmpployeeID}, Họ Tên: {_EmployeeFullName}, " +
            $"Gioi Tính: {_EmployeSex}, Tuổi: {_Age}, Lương Cơ Bản: {_BaseSalary}, Hệ Số Lương:{_SalaryCoefficient}," +
            $"Phụ Cấp: {_Allowance}, Tổng Lương: {_TotalSalary}";

        public void AddProduct(Product product)
        {
            ListProduct.Add(product);
        }

        public bool FindEmploy(int id)
        {
            return this._EmpployeeID == id;
        }
    }
}
