using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm.Dto
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public string GLN { get; set; } = "";
    }
}
