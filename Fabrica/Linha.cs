using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fabrica
{
    public partial class Linha : Form
    {
        private DbProviderFactory _Conexao;
        private DbConnection _conexao;
        private DbCommand comandoSql;
        private DbDataReader reader;
        private DataTable dataTable;

        public Boolean insereLinha, delLinha, atuLinha;

        public Linha()
        {
            InitializeComponent();
        }

        private void comando(string cmd)
        {
            comandoSql = _conexao.CreateCommand();
            comandoSql.CommandType = System.Data.CommandType.Text;
            comandoSql.CommandText = cmd;
        }

        private void montaGrid()
        {
            reader = comandoSql.ExecuteReader();
            dataTable = new DataTable();
            dataTable.Load(reader);
            gridLinha.DataSource = dataTable;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string provedor = "System.Data.OracleClient";
            //string urlConexao = "Data Source=dthashi;Persist Security Info=True;User ID=treinamento;Password=treinamento;Unicode=True;Pooling=true;Min Pool Size=5;Max Pool Size=100;";
            string connectionString = ConfigurationManager.ConnectionStrings["dthashi"].ConnectionString;

            _Conexao = DbProviderFactories.GetFactory(provedor);

            _conexao = _Conexao.CreateConnection();
            _conexao.ConnectionString = connectionString;
            _conexao.Open();

            comando("SELECT * FROM LINHA ORDER BY SEQ_LINHA DESC");
            montaGrid();
        }

        // INSERT
        private void button2_Click(object sender, EventArgs e)
        {
            insereLinha = true;
            delLinha = false;
            atuLinha = false;

            textSeq.Enabled = false;
            textCodLinha.Enabled = true;
            textDescLinha.Enabled = true;

            button5.Enabled = true;
        }

        // UPDATE
        private void button3_Click(object sender, EventArgs e)
        {
            insereLinha = false;
            delLinha = false;
            atuLinha = true;

            textSeq.Enabled = true;
            textCodLinha.Enabled = true;
            textDescLinha.Enabled = true;

            button5.Enabled = true;

            comando("SELECT * FROM LINHA ORDER BY SEQ_LINHA DESC");
            montaGrid();
        }

        // DELETE
        private void button4_Click(object sender, EventArgs e)
        {
            delLinha = true;
            insereLinha = false;
            atuLinha = false;

            textSeq.Enabled = true;
            textCodLinha.Enabled = false;
            textDescLinha.Enabled = false;

            button5.Enabled = true;
        }

        #region *** COMANDOS ***
        private void button5_Click(object sender, EventArgs e)
        {
            /*===========
             | INSERT |
             ==========*/
            if (insereLinha)
            {
                try
                {
                    string codLinha = textCodLinha.Text;
                    string desLinha = textDescLinha.Text;

                    string cmd = "";
                    cmd = "INSERT INTO LINHA (COD_LINHA, DES_LINHA) VALUES (" + codLinha + ",'" + desLinha + "')";

                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    textSeq.Clear();
                    textCodLinha.Clear();
                    textDescLinha.Clear();

                    MessageBox.Show("Linha inserido");
                    comando("SELECT * FROM LINHA ORDER BY SEQ_LINHA DESC");
                    montaGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao inserir.");
                }
            }
            /*==========
              | DELETE |
              ==========*/
            else if (delLinha)
            {
                try
                {
                    string del = textSeq.Text;
                    string cmd = "DELETE FROM LINHA WHERE SEQ_LINHA = " + del;
                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    textDescLinha.Clear();

                    MessageBox.Show("Linha deletado");
                    comando("SELECT * FROM LINHA ORDER BY SEQ_LINHA DESC");
                    montaGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro");
                }
            }
            /*
               ==========
             * | UPDATE |
             * ==========
             */
            else if (atuLinha)
            {
                try
                {
                    string seq = textSeq.Text;
                    string codLinha = textCodLinha.Text;
                    string desLinha = textDescLinha.Text;

                    string cmd;
                    cmd = "UPDATE LINHA SET COD_LINHA = " + codLinha + ", DES_LINHA = '" + desLinha + "' WHERE SEQ_LINHA = " + seq;

                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    MessageBox.Show("Atualizado", "Sucesso");
                    comando("SELECT * FROM LINHA ORDER BY SEQ_LINHA DESC");
                    montaGrid();
                    textSeq.Clear();
                    textCodLinha.Clear();
                    textDescLinha.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro");
                }
                
            }
        }
        #endregion
        
    }
}
