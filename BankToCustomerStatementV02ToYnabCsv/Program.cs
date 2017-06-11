using CsvHelper;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BankToCustomerStatementV02ToYnabCsv
{
    class Program
    {
        static void Main(string[] args)
        {
            // open a file
            if (!args.Any() || string.IsNullOrWhiteSpace(args[0]))
            {
                Console.WriteLine($"usage: {AppDomain.CurrentDomain.FriendlyName} input_file");
                Console.ReadLine();
                Environment.Exit(-1);
            }
            string inputString = File.ReadAllText(args[0]);

            // parse data to Document object (camt_053_001_02)
            Document document = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Document));
            using (StringReader reader = new StringReader(inputString))
            {
                document = (Document)(serializer.Deserialize(reader));
            }
            
            // convert data from camt_053_001_02 format to YNAB format
            var transactions = new List<Transaction>();
            foreach (var entry in document.BkToCstmrStmt.Stmt.First().Ntry.ToList())
            {
                transactions.Add(new Transaction(entry));
            }

            // write CSV file to disk
            var options = new TypeConverterOptions
            {
                Format = "yyyy-MM-dd",
            };
            TypeConverterOptionsFactory.AddOptions<DateTime>(options);
            var newFileName = Path.ChangeExtension(args[0], "csv");
            using (TextWriter writer = File.CreateText(newFileName))
            {
                var csv = new CsvWriter(writer);
                csv.WriteRecords(transactions);
            }

            // done
            Console.WriteLine($"Done! Records converted: {transactions.Count}");
            Console.Read();
        }
    }
}
