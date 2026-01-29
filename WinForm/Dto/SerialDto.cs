using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm.Dto
{
    public class SerialDto
    {
        public Guid Id { get; set; }
        public Guid WorkOrderId { get; set; }
        public string GTIN { get; set; } = "";
        public string SerialNo { get; set; } = "";
        public string BatchNo { get; set; } = "";
        public DateTime ExpiryDate { get; set; }
        public string Gs1String { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
