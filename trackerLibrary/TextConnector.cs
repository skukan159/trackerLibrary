using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackerLibrary
{
    public class TextConnector : IDataConnection
    {
        // TODO - Make the CreatePrize method actually save to the File
        /// <summary>
        /// Saves a new prize to the File
        /// </summary>
        /// <param name="model"> The prize information</param>
        /// <returns>The prize information, including the unique identifier</returns>

        public PrizeModel CreatePrize(PrizeModel model)
        {
            model.PrizeId = 1;
            return model;
        }
    }
}
