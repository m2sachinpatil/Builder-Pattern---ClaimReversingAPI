using Abstract_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abstract_Layer.Services
{
    /// <summary>
    /// Claim director - Orchestrator
    /// </summary>
  public  class ClaimDirector: IClaimDirector
    {

        private IClaimBuilder _builder;

        public void SetClaimBuilder(IClaimBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Build Claim
        /// </summary>
        /// <param name="lines">Input</param>
        public void BuildClaim(List<string> lines)
        {
            _builder.LoadData(lines);
            _builder.LoadInputData();
            _builder.Aggregation();
            _builder.DataTransformation();
            _builder.DataMutation();
        }
    }
}
