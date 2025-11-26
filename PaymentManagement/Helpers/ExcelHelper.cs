using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PaymentBusinessLayer;

namespace PaymentManagement
{
    public static class ExcelHelper
    {
        /// <summary>
        /// Exports DataTable to CSV format (Excel-compatible)
        /// </summary>
        public static void ExportToExcel(DataTable dataTable, string title = "Transactions")
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", PaymentConstants.DialogTitles.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
                FileName = $"{title}_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                Title = "Export to Excel"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportDataTableToCsv(dataTable, saveDialog.FileName);
                    MessageBox.Show($"Data exported successfully to:\\n{saveDialog.FileName}", 
                        PaymentConstants.DialogTitles.Success, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (MessageBox.Show("Do you want to open the exported file?", "Open File", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting data: {ex.Message}", 
                        PaymentConstants.DialogTitles.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static void ExportDataTableToCsv(DataTable dataTable, string filePath)
        {
            StringBuilder sb = new StringBuilder();

            int[] columnWidths = new int[dataTable.Columns.Count];
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                columnWidths[i] = dataTable.Columns[i].ColumnName.Length;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    string value = row[i]?.ToString() ?? "";
                    if (value.Length > columnWidths[i])
                    {
                        columnWidths[i] = value.Length;
                    }
                }
            }

            for (int i = 0; i < columnWidths.Length; i++)
            {
                columnWidths[i] = Math.Min(columnWidths[i] + 2, 50);
            }

            string[] columnNames = new string[dataTable.Columns.Count];
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                columnNames[i] = EscapeCsvField(dataTable.Columns[i].ColumnName.PadRight(columnWidths[i]));
            }
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dataTable.Rows)
            {
                string[] fields = new string[dataTable.Columns.Count];
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    string value = row[i]?.ToString() ?? "";
                    fields[i] = EscapeCsvField(value.PadRight(columnWidths[i]));
                }
                sb.AppendLine(string.Join(",", fields));
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private static string EscapeCsvField(string field)
        {
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            {
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }
            return field;
        }

        /// <summary>
        /// Imports data from CSV file
        /// </summary>
        public static DataTable ImportFromExcel()
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
                Title = "Import from Excel"
            };

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable dt = ImportCsvToDataTable(openDialog.FileName);
                    MessageBox.Show($"Data imported successfully!\\nRows imported: {dt.Rows.Count}", 
                        PaymentConstants.DialogTitles.Success, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error importing data: {ex.Message}", 
                        PaymentConstants.DialogTitles.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return null;
        }

        private static DataTable ImportCsvToDataTable(string filePath)
        {
            DataTable dt = new DataTable();
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length == 0)
                throw new Exception("The file is empty.");

            string[] headers = ParseCsvLine(lines[0]);
            foreach (string header in headers)
            {
                dt.Columns.Add(header.Trim());
            }

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                string[] fields = ParseCsvLine(lines[i]);
                if (fields.Length == dt.Columns.Count)
                {
                    dt.Rows.Add(fields);
                }
            }

            return dt;
        }

        private static string[] ParseCsvLine(string line)
        {
            var fields = new System.Collections.Generic.List<string>();
            bool inQuotes = false;
            StringBuilder currentField = new StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '\"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '\"')
                    {
                        currentField.Append('\"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    fields.Add(currentField.ToString());
                    currentField.Clear();
                }
                else
                {
                    currentField.Append(c);
                }
            }

            fields.Add(currentField.ToString());
            return fields.ToArray();
        }

        /// <summary>
        /// Imports transactions from CSV and saves to database
        /// </summary>
        public static int ImportTransactions()
        {
            DataTable dt = ImportFromExcel();
            if (dt == null || dt.Rows.Count == 0)
                return 0;

            string[] requiredColumns = { "Amount", "TransactionDate", "CategoryID", "CurrencyID" };
            foreach (string col in requiredColumns)
            {
                if (!dt.Columns.Contains(col))
                {
                    MessageBox.Show($"Missing required column: {col}\\n\\nRequired columns: Amount, TransactionDate, CategoryID, CurrencyID, Notes (optional)", 
                        PaymentConstants.DialogTitles.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }

            int successCount = 0;
            int errorCount = 0;
            StringBuilder errors = new StringBuilder();

            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    clsPaymentEntities.ClsPayment payment = new clsPaymentEntities.ClsPayment
                    {
                        Amount = Convert.ToDecimal(row["Amount"]),
                        TransactionDate = Convert.ToDateTime(row["TransactionDate"]),
                        CategoryID = Convert.ToInt32(row["CategoryID"]),
                        CurrencyID = Convert.ToInt32(row["CurrencyID"]),
                        Notes = dt.Columns.Contains("Notes") ? row["Notes"].ToString() : "",
                        Mode = clsPaymentEntities.ClsPayment.enMode.AddNew
                    };

                    if (PaymentBusinessLayer.clsPaymentServices.Save(ref payment))
                    {
                        successCount++;
                    }
                    else
                    {
                        errorCount++;
                        errors.AppendLine($"Row {dt.Rows.IndexOf(row) + 2}: Failed to save");
                    }
                }
                catch (Exception ex)
                {
                    errorCount++;
                    errors.AppendLine($"Row {dt.Rows.IndexOf(row) + 2}: {ex.Message}");
                }
            }

            string message = $"Import completed!\\n\\nSuccessful: {successCount}\\nFailed: {errorCount}";
            if (errorCount > 0)
            {
                message += $"\\n\\nErrors:\\n{errors}";
            }

            MessageBox.Show(message, PaymentConstants.DialogTitles.Information, MessageBoxButtons.OK, 
                errorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

            return successCount;
        }
    }
}
