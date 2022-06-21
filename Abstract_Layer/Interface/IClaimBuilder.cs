using System.Collections.Generic;

namespace Abstract_Layer.Interface
{
    /// <summary>
    /// Claim Builder interface
    /// </summary>
    public interface IClaimBuilder
    {
        void LoadData(List<string> lines);

        void LoadInputData();

        void Aggregation();

        void DataTransformation();

        void DataMutation();

        List<string> GetData();
    }
}
