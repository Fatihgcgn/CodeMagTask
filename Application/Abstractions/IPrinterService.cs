using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IPrinterService
    {
        Task PrintAsync(string payload, CancellationToken ct = default);
    }
}
