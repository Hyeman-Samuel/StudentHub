using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Infrastructure.Network
{
    public interface IOcrScanner
    {
        Task<string> RunImageOcrScan(string link);
    }
}
