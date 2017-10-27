using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FabricaWeb
{
    public class ModeloProducao
    {
        public int SEQ_PRODUCAO { get; set; }
        public int COD_LINHA { get; set; }
        public int COD_PRODUTO { get; set; }
        public int NUM_SERIE { get; set; }
        public double PESO { get; set; }
        public DateTime DAT_DATA { get; set; }
    }
}