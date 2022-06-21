using Abstract_Layer.Interface;
using Data_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Abstract_Layer.Repositories
{
    /// <summary>
    /// Loading data for processing
    /// </summary>
    public class DataLoader : IDataLoader
    {
        /// <summary>
        /// Load data from file input 
        /// </summary>
        /// <param name="lines"> Input - list</param>
        /// <returns>Data Table</returns>
        public DataTable LoadData(List<string> lines)
        {
            try
            {
                var loadTable = new DataTable();
                string[] columns = null;

                // assuming the first row contains the columns information
                if (lines.Any())
                {
                    columns = lines[0].Split(new char[] { ',' });

                    foreach (var column in columns)
                    {
                        loadTable.Columns.Add(column);
                    }
                }

                // reading rest of the data
                for (var i = 1; i < lines.Count(); i++)
                {
                    var dr = loadTable.NewRow();
                    var values = lines[i].Split(new char[] { ',' });

                    if (columns != null)
                        for (var j = 0; j < values.Count() && j < columns.Count(); j++)
                            dr[j] = values[j];

                    loadTable.Rows.Add(dr);


                }
                return loadTable;
            }
            catch (Exception)
            {
                   throw new InvalidOperationException("Cannot map data.");
            }
        }

        /// <summary>
        /// Format input data.
        /// </summary>
        /// <param name="loadTable">load table </param>
        /// <returns>input data, product and years list</returns>
        public Tuple<List<Input>, List<string>, List<string>> LoadInputData(DataTable loadTable)
        {
            try
            {
                var productNames = new List<string>();
                var originalYears = new List<string>();

                foreach (DataRow row in loadTable.Rows)
                {
                    if (!string.IsNullOrEmpty(row.Field<string>(0)))
                        productNames.Add(row.Field<string>(0));

                    if (!string.IsNullOrEmpty(row.Field<string>(0)))
                        originalYears.Add(row.Field<string>(1));
                }


                productNames = productNames.Distinct().ToList();
                originalYears = originalYears.Distinct().ToList();
                originalYears.Sort();

                var products = from table in loadTable.AsEnumerable()
                               group table by new { placeCol = table["Product"] } into groupby
                               select new
                               {
                                   Value = groupby.Key,
                                   ColumnValues = groupby
                               };

                var list = new List<Input>();
                foreach (var product in products)
                {
                    var lsProducts = new List<Product>();
                    if (string.IsNullOrEmpty(product.Value.placeCol.ToString())) continue;
                    lsProducts.AddRange(product.ColumnValues.Select(value => new Product { OYear = Convert.ToString(value.ItemArray[1]), DYear = Convert.ToString(value.ItemArray[2]), IncValue = Convert.ToDouble(value.ItemArray[3]) }));
                    list.Add(new Input { ProductName = product.Value.placeCol.ToString(), YearCount = originalYears.Count(), Years = originalYears.ToArray(), Product = lsProducts });
                }

                if (!list.Any())
                {
                    throw new NoNullAllowedException("data not loaded for input");
                }

                return Tuple.Create(list, productNames, originalYears);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
