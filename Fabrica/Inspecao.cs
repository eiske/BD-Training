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
    public partial class Inspecao : Form
    {
        private DbProviderFactory _Conexao;
        private DbConnection _conexao;
        private DbCommand comandoSql;
        private DbDataReader reader;
        private DataTable dataTable;

        public Boolean insereInspecao, delInspecao, atuInspecao;

        public Inspecao()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            string provedor = "System.Data.OracleClient";
            //string urlConexao = "Data Source=dthashi;Persist Security Info=True;User ID=treinamento;Password=treinamento;Unicode=True;Pooling=true;Min Pool Size=5;Max Pool Size=100;";
            string connectionString = ConfigurationManager.ConnectionStrings["dthashi"].ConnectionString;

            _Conexao = DbProviderFactories.GetFactory(provedor);

            _conexao = _Conexao.CreateConnection();
            _conexao.ConnectionString = connectionString;
            _conexao.Open();

            DbDataReader _reader;
            comando("SELECT DISTINCT(NUM_SERIE) FROM PRODUCAO ORDER BY NUM_SERIE");
            _reader = comandoSql.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("NUM_SERIE", typeof(string));
            dt.Load(_reader);

            combNumSerie.ValueMember = "NUM_SERIE";
            combNumSerie.DisplayMember = "NUM_SERIE";
            combNumSerie.DataSource = dt;
            

            comando("SELECT * FROM INSPECAO ORDER BY SEQ_INSPECAO DESC");
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
            gridInspecao.DataSource = dataTable;
        }

        // INSERT
        private void button2_Click(object sender, EventArgs e)
        {
            textSeq.Clear();
            textCodProd.Clear();
            textNumSerie.Clear();
            textNumResultado.Clear();
            textAp.Clear();

            insereInspecao = true;
            delInspecao = false;
            atuInspecao = false;

            textSeq.Enabled = false;
            textCodProd.Enabled = true;
            //textNumSerie.Enabled = true;
            combNumSerie.Enabled = true;
            textAp.Enabled = true;
            textNumResultado.Enabled = true;

            button5.Enabled = true;
        }

        // UPDATE
        private void button3_Click(object sender, EventArgs e)
        {
            textSeq.Clear();
            textCodProd.Clear();
            textNumSerie.Clear();
            textNumResultado.Clear();
            textAp.Clear();

            insereInspecao = false;
            delInspecao = false;
            atuInspecao = true;

            textSeq.Enabled = true;
            textCodProd.Enabled = true;
            //textNumSerie.Enabled = true;
            combNumSerie.Enabled = true;
            textAp.Enabled = true;
            textNumResultado.Enabled = true;

            button5.Enabled = true;

            comando("SELECT * FROM INSPECAO ORDER BY SEQ_INSPECAO DESC");
            montaGrid();
        }

        // DELETE
        private void button4_Click(object sender, EventArgs e)
        {
            textSeq.Clear();
            textCodProd.Clear();
            textNumSerie.Clear();
            textNumResultado.Clear();
            textAp.Clear();

            delInspecao = true;
            insereInspecao = false;
            atuInspecao = false;

            textSeq.Enabled = true;
            textCodProd.Enabled = false;
            //textNumSerie.Enabled = false;
            combNumSerie.Enabled = false;
            textAp.Enabled = false;
            textNumResultado.Enabled = false;

            button5.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*===========
             | INSERT |
             ==========*/
            if (insereInspecao)
            {
                try
                {
                    string codProduto = textCodProd.Text;
                    string numSerie = combNumSerie.Text;
                    string ap = textAp.Text;
                    string numRes = textNumResultado.Text;

                    string cmd = "";
                    cmd = "INSERT INTO INSPECAO (COD_PRODUTO, NUM_SERIE, APROVADO, NUM_RESULTADO, DAT_DATA) VALUES (" + codProduto + "," + numSerie + ", '" + ap + "', " + numRes + ", TO_DATE(Trunc(DBMS_RANDOM.value(TO_CHAR(date '2000-01-01','J'),TO_CHAR(SYSDATE,'J'))),'J'))";

                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    textSeq.Clear();
                    textCodProd.Clear();
                    textNumSerie.Clear();
                    textAp.Clear();
                    textNumResultado.Clear();

                    MessageBox.Show("Inspecao inserido");
                    comando("SELECT * FROM INSPECAO ORDER BY SEQ_INSPECAO DESC");
                    montaGrid();
                }
                catch (Exception ex)
                {
                    textSeq.Clear();
                    textCodProd.Clear();
                    textNumSerie.Clear();
                    textAp.Clear();
                    textNumResultado.Clear();
                    MessageBox.Show(ex.Message, "Erro ao inserir.");
                }
            }
            /*==========
              | DELETE |
              ==========*/
            else if (delInspecao)
            {
                try
                {
                    string del = textSeq.Text;
                    string cmd = "DELETE FROM INSPECAO WHERE SEQ_INSPECAO = " + del;
                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    textSeq.Clear();
                    textCodProd.Clear();
                    textNumSerie.Clear();
                    textAp.Clear();
                    textNumResultado.Clear();

                    MessageBox.Show("Inspecao deletado");
                    comando("SELECT * FROM INSPECAO ORDER BY SEQ_INSPECAO DESC");
                    montaGrid();
                }
                catch (Exception ex)
                {
                    textSeq.Clear();
                    textCodProd.Clear();
                    textNumSerie.Clear();
                    textAp.Clear();
                    textNumResultado.Clear();
                    MessageBox.Show(ex.Message, "Erro");
                }
            }
            /*
               ==========
             * | UPDATE |
             * ==========
             */
            else if (atuInspecao)
            {
                try
                {
                    string seq = textSeq.Text;
                    string codProduto = textCodProd.Text;
                    string numSerie = combNumSerie.Text;
                    //string numSerie = textNumSerie.Text;
                    string ap = textAp.Text;
                    string numR = textNumResultado.Text;

                    string cmd;
                    cmd = "UPDATE INSPECAO SET COD_PRODUTO = " + codProduto + ", NUM_SERIE = " + numSerie + ", APROVADO = '" + ap + "', NUM_RESULTADO = " + numR + " WHERE SEQ_INSPECAO = " + seq;

                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    MessageBox.Show("Atualizado", "Sucesso");
                    comando("SELECT * FROM INSPECAO ORDER BY SEQ_INSPECAO DESC");
                    montaGrid();
                    textSeq.Clear();
                    textCodProd.Clear();
                    textNumSerie.Clear();
                    textAp.Clear();
                    textNumResultado.Clear();
                }
                catch (Exception ex)
                {
                    textSeq.Clear();
                    textCodProd.Clear();
                    textNumSerie.Clear();
                    textAp.Clear();
                    textNumResultado.Clear();
                    MessageBox.Show(ex.Message, "Erro");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                panel1.Enabled = true;
                button5.Enabled = false;
            }
            else
            {
                panel1.Enabled = false;
                button5.Enabled = true;
            }
        }

        // SELECT COM FILTRO
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && checkCodProduto.Checked == false)
            {
                string dataInicial = dateTimePicker1.Text;
                string dataFinal = dateTimePicker2.Text;
                string cmd;
                cmd = "SELECT * FROM INSPECAO WHERE DAT_DATA BETWEEN TO_DATE('" + dataInicial + "', 'dd/mm/yyyy hh24:mi:ss') AND TO_DATE('" + dataFinal + "', 'dd/mm/yyyy hh24:mi:ss')";
                comando(cmd);
                montaGrid();
            }
            else if (checkBox1.Checked == false && checkCodProduto.Checked == true)
            {
                string produto = textProduto.Text;
                string cmd;
                cmd = "SELECT * FROM INSPECAO WHERE COD_PRODUTO = " + produto;
                comando(cmd);
                montaGrid();
            }
            else if (checkBox1.Checked == true && checkCodProduto.Checked == true)
            {
                string dataInicial = dateTimePicker1.Text;
                string dataFinal = dateTimePicker2.Text;
                string produto = textProduto.Text;
                string cmd;
                cmd = "SELECT * FROM INSPECAO WHERE COD_PRODUTO = "+produto+" AND DAT_DATA BETWEEN TO_DATE('" + dataInicial + "', 'dd/mm/yyyy hh24:mi:ss') AND TO_DATE('" + dataFinal + "', 'dd/mm/yyyy hh24:mi:ss')";
                comando(cmd);
                montaGrid();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            insereInspecao = false;
            delInspecao = false;
            atuInspecao = true;

            textSeq.Enabled = false;
            textCodProd.Enabled = true;
            combNumSerie.Enabled = true;
            textAp.Enabled = true;
            textNumResultado.Enabled = true;

            button5.Enabled = true;

            textSeq.Text = gridInspecao.CurrentRow.Cells[0].Value.ToString();
            textCodProd.Text = gridInspecao.CurrentRow.Cells[1].Value.ToString();
            combNumSerie.Text = gridInspecao.CurrentRow.Cells[2].Value.ToString();
            textAp.Text = gridInspecao.CurrentRow.Cells[3].Value.ToString();
            textNumResultado.Text = gridInspecao.CurrentRow.Cells[4].Value.ToString();
        }

        // CHECK CONTROLE PANEL 2
        private void checkCodProduto_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCodProduto.Checked)
            {
                panel2.Enabled = true;
            }
            else
            {
                panel2.Enabled = false;
            }
        }

        // LIMPA FILTRO
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            textProduto.Clear();
            comando("SELECT * FROM INSPECAO ORDER BY COD_PRODUTO");
            montaGrid();
        }

        private void combNumSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID = combNumSerie.SelectedValue.ToString();
        }




    }
}
