using Data_Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Abstract_Layer.Interface
{
    /// <summary>
    /// Data Loader interface
    /// </summary>
   public interface IDataLoader
   {
       DataTable LoadData(List<string> lines);

       Tuple<List<Input>, List<string>, List<string>> LoadInputData(DataTable loadTable);
   }
}
