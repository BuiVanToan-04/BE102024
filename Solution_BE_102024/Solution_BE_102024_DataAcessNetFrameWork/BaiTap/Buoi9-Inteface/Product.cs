using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi9_Inteface
{
    public class Product
    {
        private string _ProductID {  get; set; }

        private string _ProductName { get; set; }

        private int _ProductQuantity { get; set; }
        public Product() { }
        
        public Product(string id, string name, int quantity)
        {
            _ProductID = id;
            _ProductName = name;
            _ProductQuantity = quantity;
        }

        public string ProductID => _ProductID;

        public string ProductName => _ProductName;

        public int ProductQuantity => _ProductQuantity;

        public override string ToString() => $"{ProductID} {ProductName} {ProductQuantity}";
    }
}
