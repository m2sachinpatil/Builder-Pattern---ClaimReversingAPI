using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using Abstract_Layer.Interface;
using Data_Model;

namespace Abstract_Layer.Repositories
{
    /// <summary>
    /// Data aggregation 
    /// </summary>
    public class DataAggregation : IDataAggregation
    {
        /// <summary>
        /// Aggregate data 
        /// </summary>
        /// <param name="loadTable">input table </param>
        /// <param name="originalYears">list of years</param>
        /// <param name="inputs">input data</param>
        /// <returns>aggregated data</returns>
        public List<DataTable> AggregateDataTable(DataTable loadTable, List<string> originalYears, List<Input> inputs)
        {
            try
            {
                var agreeTables = new List<DataTable>();

                if (loadTable == null)
                    throw new NoNullAllowedException("data not available");

                var yearCount = originalYears.Count();

                for (var i = 0; i < inputs.Count(); i++)
                {
                    var aggreTable = new DataTable(inputs[i].ProductName);

                    for (var j = 0; j <= yearCount; j++)
                    {
                        aggreTable.Columns.Add(j == 0 ? "year" : j.ToString(), typeof(string));
                    }

                    foreach (var (year, row) in from year in originalYears
                                                let row = aggreTable.NewRow()
                                                select (year, row))
                    {
                        row["year"] = year;
                        for (var k = 1; k <= yearCount; k++)
                        {
                            row[k.ToString()] = "0";
                        }

                        aggreTable.Rows.Add(row);
                    }

                    agreeTables.Add(aggreTable);
                }

                return agreeTables;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Data transformation 
        /// </summary>
        /// <param name="inputs">input data</param>
        /// <param name="agreeTables">Aggregation table</param>
        /// <returns>Data table</returns>
        public List<DataTable> DataTransformation(List<Input> inputs, List<DataTable> agreeTables)
        {
            try
            {
                var i = 0;
                foreach (var input in inputs)
                {
                    foreach (DataRow row in agreeTables[i].Rows)
                    {
                        foreach (var product in input.Product)
                        {
                            if (row.Field<string>(0) != product.OYear) continue;
                            var distYears = DistData(input, product.OYear);
                            var dYearIndex = Array.IndexOf(distYears, Convert.ToInt32(product.DYear));
                            row[dYearIndex + 1] = product.IncValue;
                        }
                    }
                    i++;
                }

                return agreeTables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mutate aggregated data.
        /// </summary>
        /// <param name="agreeTables">Aggrigation data</param>
        /// <returns>output list of string</returns>
        public List<string> DataMutation(List<DataTable> agreeTables, List<string> oYears)
        {
            try
            {
                var outPutList = new List<string>();
                outPutList.Add(oYears[0].Trim() + ", " + oYears.Count);
                foreach (var agreeTable in agreeTables)
                {
                    var outputString = new StringBuilder();

                    agreeTable.Columns.Remove("year");
                    foreach (DataRow row in agreeTable.Rows)
                    {
                        var strData = string.Join(", ", row.ItemArray.Select(c => c.ToString()).ToArray());

                        var strArr = strData.Split(",").Select(p => p.Trim()).ToArray();

                        var isNanZero = strArr.Select(arr => Convert.ToDouble(arr) > 0).FirstOrDefault();

                        if (isNanZero)
                        {
                            strArr = strArr.Reverse().SkipWhile(e => Convert.ToInt32(e) == 0).Reverse().ToArray();
                        }

                        for (var i = 0; i < strArr.Length; i++)
                        {
                            if (i > 0)
                                strArr[i] = Convert.ToString(Convert.ToDouble(strArr[i]) + Convert.ToDouble(strArr[i - 1]), CultureInfo.InvariantCulture);

                            outputString.Append(strArr[i]);
                            outputString.Append(",");
                        }
                    }
                    outPutList.Add(agreeTable.TableName +" - " +outputString.ToString());

                }
                return outPutList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Recursive year call.
        /// </summary>
        /// <param name="input">input data</param>
        /// <param name="oYear">original year</param>
        /// <returns>list of years</returns>
        private static int[] DistData(Input input, string oYear)
        {
            var distYears = new List<int>();

            foreach (var distYear in input.Product)
            {
                if (!distYears.Contains(Convert.ToInt32(distYear.DYear)))
                    distYears.Add(Convert.ToInt32(distYear.DYear));

                if (!distYears.Contains(Convert.ToInt32(distYear.OYear)))
                    distYears.Add(Convert.ToInt32(distYear.OYear));
            }

            distYears.Sort();

            distYears.RemoveAll(x => x < Convert.ToInt32(oYear));

            var data = distYears.ToArray();

            return data;
        }
    }
}
