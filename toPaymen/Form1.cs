using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToSepa;
using static toPaymen.Tools;

namespace toPaymen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Excel Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "xlsx",
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dt = readExcel(openFileDialog1.FileName);
                ArrayList ArDest = new ArrayList();
                StreamWriter sw = new StreamWriter("paymen.txt");

                ArDest = SEPA.getArray();

                bool flgFin = false;
                var cmpt = 2;

                while (!flgFin)
                {
                    flgFin = Tools.dbToArray(ArDest, cmpt, dt);
                    Tools.trouveObject("Matricule", ArDest).valeur=cmpt.ToString();

                    SEPA.setSEPAfixe(ArDest);
                    
                    ((objectTxt)ArDest[2]).valeur = txtTrain.Text;

                    Tools.toDest(ArDest, sw);

                    cmpt++;
                }

                sw.Close();
            }
        }

        private DataTable readExcel(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Choose one of either 1 or 2:

                    // 1. Use the reader methods
                    do
                    {
                        while (reader.Read())
                        {
                            // reader.GetDouble(0);
                        }
                    } while (reader.NextResult());

                    // 2. Use the AsDataSet extension method
                    return reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    }).Tables[0];

                    // The result of each spreadsheet is in result.Tables
                }
            }
        }
    }
}
