using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using _99xAssessment2.Models;
using _99xAssessment2.Repository.ORM;
using ExcelDataReader;

namespace _99xAssessment2.Utils
{
    public class ExcelUtils
    {
        public static void ProcessExcel()
        {

        }

        internal static List<AccountJournalDescriptor> ProcessExcel(HttpPostedFileBase file, int mon, int year)
        {
            if (file != null)
            {
                using (var stream = file.InputStream)
                {
                    // Auto-detect format, supports:
                    //  - Binary Excel files (2.0-2003 format; *.xls)
                    //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        // 2. Use the AsDataSet extension method
                        var result = reader.AsDataSet();

                        // The result of each spreadsheet is in result.Tables
                        if (result != null)
                        {
                            try
                            {
                                var toBeReturned = new List<AccountJournalDescriptor>();
                                var excelRows = result.Tables[0].Rows;
                                if (excelRows.Count > 0)
                                {
                                    //Has data.
                                    int row = 1;
                                    foreach (DataRow excelRow in excelRows)
                                    {
                                        if (row == 1)
                                        {
                                            row++;
                                            continue;
                                        }

                                        if (row > 1)
                                        {
                                            var accName = excelRow.ItemArray[0].ToString();

                                            toBeReturned.Add(new AccountJournalDescriptor
                                            {
                                                Account = new Account
                                                {
                                                    AccountName = excelRow.ItemArray[0].ToString()
                                                },
                                                AccountJournal = new List<AccountJournal>()
                                                {
                                                    new AccountJournal
                                                    {
                                                        Year = year,
                                                        Month = mon,
                                                        Value = Convert.ToDecimal(excelRow.ItemArray[1])
                                                    }
                                                }
                                            });
                                        }

                                        row++;
                                    }

                                    return toBeReturned;
                                }
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}