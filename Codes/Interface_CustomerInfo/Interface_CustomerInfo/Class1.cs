using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_CustomerInfo
{
    public interface ICustomerInfo
    {
        void Init(string username, string password);
        bool ExecuteSelectCommand(string selCommand);
        string GetRow();
    }
}
