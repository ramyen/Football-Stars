using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public class ExcelFile
    {
        public struct ExcelSheetData
        {
            public string sheetName;
            public string filename;
            public string jsonString;
            public List<Dictionary<string, string>> data;
        }

        public Dictionary<string, ExcelSheetData> sheets = new Dictionary<string, ExcelSheetData>();
    }
}
