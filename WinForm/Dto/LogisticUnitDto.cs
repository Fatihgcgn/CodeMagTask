using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm.Dto
{
    public class LogisticUnitDto
    {
        public Guid Id { get; set; }
        public Guid WorkOrderId { get; set; }
        public string SSCC { get; set; } = "";
        public int Type { get; set; } // enum yerine int geliyor olabilir
        public DateTime CreatedAt { get; set; }
    }
}
