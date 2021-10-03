using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactTestTask.Models
{
    public enum OperationResult 
    {
        None = 0,
        Success = 1,
        InsufficientFunds = 2,
        LastItemBought = 3,
        Unavailable = 4
    }
    public class DrinkTransactionResult
    {
        public int DrinkId;
        public OperationResult OperationCode;
        public int NewBalance; 
    }
}
