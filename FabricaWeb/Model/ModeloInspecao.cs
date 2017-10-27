using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FabricaWeb
{
    public class ModeloInspecao
    {
        public int SEQ_INSPECAO { get; set; }
        public int COD_PRODUTO { get; set; }
        public int NUM_SERIE { get; set; }
        public char APROVADO { get; set; }
        public int NUM_RESULTADO { get; set; }
        public DateTime DAT_DATA { get; set; }
    }
}