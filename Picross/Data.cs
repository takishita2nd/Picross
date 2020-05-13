using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross
{
    [JsonObject("dataModel")]
    class Data
    {
        [JsonProperty("rowdata")]
        public List<List<int>> RowData { get; set; }
        [JsonProperty("coldata")]
        public List<List<int>> ColData { get; set; }
    }
}
