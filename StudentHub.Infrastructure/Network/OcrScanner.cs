using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Infrastructure.Network
{
    public class OcrScanner : IOcrScanner
    {
        public async Task<string> RunImageOcrScan(string link)
        {
            return "latex";
        }
    }
}
