using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankToCustomerStatementV02ToYnabCsv
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public string Payee { get; set; }
        public string Category { get; set; }
        public string Memo { get; set; }
        public decimal? Outflow { get; set; }
        public decimal? Inflow { get; set; }
        
        public Transaction(ReportEntry2 entry)
        {
            Date = entry.BookgDt.Item.Date;
            Memo = entry.NtryDtls.First().TxDtls.First().RmtInf.Ustrd.First();
            if (entry.CdtDbtInd.HasFlag(CreditDebitCode.DBIT))
            {
                Outflow = entry.Amt.Value;
                Payee = entry.NtryDtls.First().TxDtls.First().RltdPties.Cdtr.Nm;
            }
            else
            {
                Inflow = entry.Amt.Value;
                Payee = entry.NtryDtls.First().TxDtls.First().RltdPties.Dbtr.Nm;
            }
        }
    }
}
