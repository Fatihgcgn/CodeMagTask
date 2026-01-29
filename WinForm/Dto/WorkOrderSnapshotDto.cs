using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm.Dto
{
    public class WorkOrderSnapshotDto
    {
        public WorkOrderInfoDto WorkOrder { get; set; } = new();
        public ProductInfoDto Product { get; set; } = new();
        public List<SerialDto> Serials { get; set; } = new();
        public List<LogisticUnitDto> LogisticUnits { get; set; } = new();
        public List<AggregationLinkDto> AggregationLinks { get; set; } = new();
    }
}
