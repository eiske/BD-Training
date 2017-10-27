using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FabricaWeb
{
    public class ModeloProduto
    {
        public int SEQ_PRODUTO { get; set; }
        public int COD_PRODUTO { get; set; }
        public string COR { get; set; }
        public int COD_FAMILIA { get; set; }
    }
}