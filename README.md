## ISO 20022 account statement format to YNAB CSV

This is a command line tool that converts bank statements in BankToCustomerStatementV02 format (as described by the ISO 20022 standard) to CSV files readable by YNAB personal budgeting tool.

The tool is written as C# console application.

## Usage

Build the source code and run the tool from the command line:

usage: BankToCustomerStatementV02ToYnabCsv.exe input_file

The tool will create an output file with the same name and location as the input file, but with CSV extension.

## More information about formats

YNAB CSV file format is documented here:  
http://classic.youneedabudget.com/support/article/csv-file-importing

BankToCustomerStatementV02 format is documented here:
https://www.iso20022.org/standardsrepository/public/wqt/Content/mx/camt.053.001.02