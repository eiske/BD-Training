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
    public partial class Produto : Form
    {
        private DbProviderFactory _Conexao;
        private DbConnection _conexao;
        private DbCommand comandoSql;
        private DbDataReader reader;
        private DataTable dataTable;

        public Boolean insereProduto, delProduto, atuProduto;
        public Produto()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string provedor = "System.Data.OracleClient";
            string connectionString = ConfigurationManager.ConnectionStrings["dthashi"].ConnectionString;

            _Conexao = DbProviderFactories.GetFactory(provedor);

            _conexao = _Conexao.CreateConnection();
            _conexao.ConnectionString = connectionString;
            _conexao.Open();

            comando("SELECT * FROM PRODUTO ORDER BY SEQ_PRODUTO DESC");
            montaGrid();
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
            gridProduto.DataSource = dataTable;
        }

        // INSERT
        private void button2_Click(object sender, EventArgs e)
        {
            insereProduto = true;
            delProduto = false;
            atuProduto = false;
            checkBox1.Enabled = false;

            textSeq.Enabled = false;
            textCodProduto.Enabled = true;
            textCor.Enabled = true;
            textCodFamilia.Enabled = true;

            button5.Enabled = true;
        }

        //PROCEDURE
        private void comandoProcedure(string cmd)
        {
            comandoSql = _conexao.CreateCommand();
            comandoSql.CommandType = System.Data.CommandType.StoredProcedure;
            comandoSql.CommandText = cmd;
            comandoSql.ExecuteNonQuery();
        }

        //UPDATE
        private void button3_Click(object sender, EventArgs e)
        {
            insereProduto = false;
            delProduto = false;
            atuProduto = true;
            checkBox1.Enabled = false;

            textSeq.Enabled = true;
            textCodProduto.Enabled = true;
            textCor.Enabled = true;
            textCodFamilia.Enabled = true;

            button5.Enabled = true;
            comando("SELECT * FROM PRODUTO ORDER BY SEQ_PRODUTO DESC");
            montaGrid();
        }

        // DELETE
        private void button4_Click(object sender, EventArgs e)
        {
            delProduto = true;
            insereProduto = false;

            checkBox1.Enabled = true;
            textSeq.Enabled = false;
            textCodProduto.Enabled = true;
            textCor.Enabled = false;
            textCodFamilia.Enabled = false;

            button5.Enabled = true;
        }

        // COMANDOS
        private void button5_Click(object sender, EventArgs e)
        {
            /*===========
             | INSERT |
             ==========*/
            if (insereProduto)
            {
                try
                {
                    string codProduto = textCodProduto.Text;
                    string cor = textCor.Text;
                    string codFamilia = textCodFamilia.Text;

                    string cmd = "";
                    cmd = "INSERT INTO PRODUTO (COD_PRODUTO, COR, COD_FAMILIA) VALUES (" + codProduto + ",'" + cor + "'," + codFamilia + ")";

                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    textSeq.Clear();
                    textCodProduto.Clear();
                    textCor.Clear();
                    textCodFamilia.Clear();

                    MessageBox.Show("Produto inserido");
                    comando("SELECT * FROM PRODUTO ORDER BY SEQ_PRODUTO DESC");
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
            else if (delProduto)
            {
                try
                {
                    if (checkBox1.Checked)
                    {
                        string del = textCodProduto.Text;
                        string procedure = "P_DELETE_PRODUTO("+del+")";
                        comandoProcedure(procedure);

                        textSeq.Clear();
                        textCodProduto.Clear();
                        textCor.Clear();
                        textCodFamilia.Clear();

                        MessageBox.Show("Produto deletado");
                        comando("SELECT * FROM PRODUTO ORDER BY COD_PRODUTO");
                        montaGrid();
                    }
                    else
                    {
                        string del = textCodProduto.Text;
                        string cmd = "DELETE FROM PRODUTO WHERE COD_PRODUTO = " + del;
                        comando(cmd);
                        comandoSql.ExecuteNonQuery();
                        comando("COMMIT");
                        comandoSql.ExecuteNonQuery();

                        textSeq.Clear();
                        textCodProduto.Clear();
                        textCor.Clear();
                        textCodFamilia.Clear();

                        MessageBox.Show("Produto deletado");
                        comando("SELECT * FROM PRODUTO ORDER BY COD_PRODUTO");
                        montaGrid();    
                    }
                    
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
            else if (atuProduto)
            {
                string seq = textSeq.Text;
                string codProduto = textCodProduto.Text;
                string cor = textCor.Text;
                string codFamilia = textCodFamilia.Text;

                string cmd;
                cmd = "UPDATE PRODUTO SET COD_PRODUTO = " + codProduto + ", COR = '" + cor + "', COD_FAMILIA = " + codFamilia + "WHERE SEQ_PRODUTO = " + seq;

                comando(cmd);
                comandoSql.ExecuteNonQuery();
                comando("COMMIT");
                comandoSql.ExecuteNonQuery();

                MessageBox.Show("Atualizado", "Sucesso");
                comando("SELECT * FROM PRODUTO ORDER BY SEQ_PRODUTO DESC");
                montaGrid();
                textSeq.Clear();
                textCodProduto.Clear();
                textCor.Clear();
                textCodFamilia.Clear();
            }
        }

    }
}
