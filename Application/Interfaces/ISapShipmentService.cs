using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISapShipmentService
    {
        Task<string> PostShipmentSapAsync(string onefisShipmentCode, DateTime updatedAt, string updatedBy);
        Task<string> PostShipmentGiSapAsync(string onefisShipmentCode, DateTime updatedAt, string updatedBy);
    }

}
