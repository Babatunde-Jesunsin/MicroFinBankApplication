using BankData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankData
{
    class AccountNumberGenrator
    {
        
        public string AccountNumberGen(int id)
        {
            Random random = new Random();
            int num = random.Next(10000000, 40000000);
            string AccountNumberGenerated = "0" + "" + id + "" + num;
            return AccountNumberGenerated;

        }
    }
}
