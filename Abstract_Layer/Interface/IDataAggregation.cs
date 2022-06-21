using Data_Model;
using System.Collections.Generic;
using System.Data;

namespace Abstract_Layer.Interface
{
    /// <summary>
    /// Data Aggregation Interface
    /// </summary>
   public interface IDataAggregation
   {
       List<DataTable> AggregateDataTable(DataTable loadTable, List<string> originalYears, List<Input> inputs);

       List<DataTable> DataTransformation(List<Input> inputs, List<DataTable> agreeTables);

        List<string> DataMutation(List<DataTable> agreeTables, List<string> oYears);
   }
}
