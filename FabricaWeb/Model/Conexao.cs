using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Configuration;
using System.Data;

namespace FabricaWeb
{
    public class Conexao
    {
        public DbProviderFactory _provider;
        public DbConnection _conexao;
        public string provedor = "System.Data.OracleClient";
        public string connectionString = ConfigurationManager.ConnectionStrings["dthashi"].ConnectionString;

        public Conexao()
        { 
            _provider = DbProviderFactories.GetFactory(provedor);
            _conexao = _provider.CreateConnection();
            _conexao.ConnectionString = connectionString;
            _conexao.Open();
        }

        

        
    }
}