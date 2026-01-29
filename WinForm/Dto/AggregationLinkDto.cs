using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm.Dto
{
    public class AggregationLinkDto
    {
        public Guid Id { get; set; }
        public Guid ParentLogisticUnitId { get; set; }
        public int ChildType { get; set; }
        public Guid? ChildLogisticUnitId { get; set; }
        public Guid? ChildSerialId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
