using System.Collections.Generic;

namespace Abstract_Layer.Interface
{
    /// <summary>
    /// Claim director interface
    /// </summary>
  public  interface IClaimDirector
    {
        void SetClaimBuilder(IClaimBuilder builder);

        void BuildClaim(List<string> lines);
    }
}
