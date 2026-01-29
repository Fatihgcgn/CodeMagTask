using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm.Dto
{
    public class WorkOrderDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string GTIN { get; set; }
        public decimal Quantity { get; set; }
        public string BatchNo { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int SerialStart { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
