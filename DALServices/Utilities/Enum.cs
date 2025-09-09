using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities
{
    public enum LoadingTypes
    {
        BULK = 1,
        BAGS = 2,
        CONTAINER = 3
    }
    public enum AccountType
    {
        Asset,
        Liability,
        Equity,
        Revenue,
        Expense
    }
    public enum OwnershipType
    {
        Vendor,
        Own,
        Client
    }
    public enum VoucherStatus
    {
        DRAFT = 1,
        PAID = 2
    }
}
