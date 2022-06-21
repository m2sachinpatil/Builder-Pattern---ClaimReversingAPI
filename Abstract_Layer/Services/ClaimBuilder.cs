using Abstract_Layer.Interface;
using Data_Model;
using System.Collections.Generic;
using System.Data;

namespace Abstract_Layer.Services
{
    /// <summary>
    /// Claim builder class
    /// </summary>
    public class ClaimBuilder : IClaimBuilder
    {
        public IDataLoader _dataLoader;
        public IDataAggregation _dataAggregation;

        public DataTable LoadTable = new DataTable();
        public List<string> ProductNames = new List<string>();
        public List<string> OriginalYears = new List<string>();
        public List<Input> Inputs = new List<Input>();
        public List<DataTable> AgreeTables = new List<DataTable>();
        public List<string> OutputList = new List<string>();

        /// <summary>
        /// Builder constructor
        /// </summary>
        /// <param name="dataLoader">data loader</param>
        /// <param name="dataAggregation">data aggregator</param>
        public ClaimBuilder(IDataLoader dataLoader, IDataAggregation dataAggregation)
        {
            this.Reset();
            this._dataLoader = dataLoader;
            this._dataAggregation = dataAggregation;
        }

        /// <summary>
        /// Reset builder.
        /// </summary>
        public void Reset()
        {
            LoadTable = new DataTable();
            ProductNames = new List<string>();
            OriginalYears = new List<string>();
            Inputs = new List<Input>();
            AgreeTables = new List<DataTable>();
            OutputList = new List<string>();
        }

        /// <summary>
        /// Load input data.
        /// </summary>
        /// <param name="lines">line of input</param>
        public void LoadData(List<string> lines)
        {
            LoadTable = _dataLoader.LoadData(lines);
        }

        /// <summary>
        /// Load input model
        /// </summary>
        public void LoadInputData()
        {
            var result = _dataLoader.LoadInputData(LoadTable);
            Inputs = result.Item1;
            ProductNames = result.Item2;
            OriginalYears = result.Item3;

        }

        /// <summary>
        /// Aggregate data
        /// </summary>
        public void Aggregation()
        {
            AgreeTables = _dataAggregation.AggregateDataTable(LoadTable, OriginalYears, Inputs);
        }

        /// <summary>
        /// Data transformation.
        /// </summary>
        public void DataTransformation()
        {
            AgreeTables = _dataAggregation.DataTransformation(Inputs, AgreeTables);
        }

        /// <summary>
        /// Data Mutation
        /// </summary>
        public void DataMutation()
        {
            OutputList = _dataAggregation.DataMutation(AgreeTables, OriginalYears);
        }

        /// <summary>
        /// Return output data
        /// </summary>
        /// <returns>output</returns>
        public List<string> GetData()
        {
            return OutputList;
        }


    }
}
