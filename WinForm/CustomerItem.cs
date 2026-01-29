using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm
{
    public class CustomerItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string GLN { get; set; } = "";

        // ComboBox ekranda bunu gösterir
        public override string ToString() => Name;
    }
}
