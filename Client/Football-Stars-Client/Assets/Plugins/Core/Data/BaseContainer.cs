using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Data
{
    public class BaseContainer
    {
        //public virtual void LoadFromExcel(ExcelSheetData sheetData, ExcelFile excelFile) { }
        public virtual void LoadFromBinary(TextAsset asset) { }
        public virtual object GetBinData() => null;
        public virtual object GetServerJsonData() => null;
        public virtual Dictionary<string, object> GetMultuServerJsonData() => null;
        public virtual void PostRuntimeFill() { }
        public virtual void Validate() { }
    }
}
