using System.Collections.Generic;

namespace Data_Model
{
    /// <summary>
    /// Input Model
    /// </summary>
    public class Input
    {
        /// <summary>
        /// Product Name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Product
        /// </summary>
        public List<Product> Product { get; set; }

        /// <summary>
        /// Year count
        /// </summary>
        public int YearCount { get; set; }

        /// <summary>
        /// Array of years
        /// </summary>
        public string[] Years { get; set; }
    }
}
