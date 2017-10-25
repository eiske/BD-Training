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
using System.IO;
//using System.Reflection;


namespace Fabrica
{
    public partial class Producao : Form
    {
        private DbProviderFactory _Conexao;
        private DbConnection _conexao;
        private DbCommand comandoSql;
        private DbDataReader reader;
        private System.Data.DataTable _dataTable;

        public Boolean insereProducao, delProducao, atuProducao;

        public Producao()
        {
            InitializeComponent();
        }

        // INSERT
        private void button2_Click(object sender, EventArgs e)
        {
            insereProducao = true;
            delProducao = false;
            atuProducao = false;

            textSeq.Enabled = false;
            textCodLinha.Enabled = true;
            textCodProduto.Enabled = true;
            textNumSerie.Enabled = true;
            textPeso.Enabled = true;

            button5.Enabled = true;
        }

        // UPDATE
        private void button3_Click(object sender, EventArgs e)
        {
            insereProducao = false;
            delProducao = false;
            atuProducao = true;

            textSeq.Enabled = true;
            textCodLinha.Enabled = true;
            textCodProduto.Enabled = true;
            textNumSerie.Enabled = true;
            textPeso.Enabled = true;

            button5.Enabled = true;
            comando("SELECT * FROM PRODUCAO ORDER BY SEQ_PRODUCAO DESC");
            montaGrid();
        }

        // DELETE
        private void button4_Click(object sender, EventArgs e)
        {
            atuProducao = false;
            delProducao = true;
            insereProducao = false;

            textSeq.Enabled = true;
            textCodLinha.Enabled = false;
            textCodProduto.Enabled = false;
            textNumSerie.Enabled = false;
            textPeso.Enabled = false;

            button5.Enabled = true;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string provedor = "System.Data.OracleClient";
            //string urlConexao = "Data Source=dthashi;Persist Security Info=True;User ID=treinamento;Password=treinamento;Unicode=True;Pooling=true;Min Pool Size=5;Max Pool Size=100;";
            string connectionString = ConfigurationManager.ConnectionStrings["dthashi"].ConnectionString;

            _Conexao = DbProviderFactories.GetFactory(provedor);

            _conexao = _Conexao.CreateConnection();
            _conexao.ConnectionString = connectionString;
            _conexao.Open();

            comando("SELECT * FROM PRODUCAO ORDER BY SEQ_PRODUCAO DESC");
            montaGrid();
        }

        private void comando(string cmd)
        {
            comandoSql = _conexao.CreateCommand();
            comandoSql.CommandType = System.Data.CommandType.Text;
            comandoSql.CommandText = cmd;
        }

        #region *** PROCEDURE ***
        private void comandoProcedure(string cmd)
        {
            comandoSql = _conexao.CreateCommand();
            comandoSql.CommandType = System.Data.CommandType.StoredProcedure;
            comandoSql.CommandText = cmd;
            comandoSql.ExecuteNonQuery();
        }
        #endregion


        #region *** MONTAGRID ***
        private void montaGrid()
        {
            reader = comandoSql.ExecuteReader();
            _dataTable = new System.Data.DataTable();
            _dataTable.Load(reader);
            gridProducao.DataSource = _dataTable;
        }
        #endregion


        #region *** BOTAO DE COMANDO ***
        private void button5_Click(object sender, EventArgs e)
        {
            /*===========
             | INSERT |
             ==========*/
            if (insereProducao)
            {
                try
                {
                    string codLinha = textCodLinha.Text;
                    string codProduto = textCodProduto.Text;
                    string numSerie = textNumSerie.Text;
                    string peso = textPeso.Text;

                    string cmd = "";
                    cmd = "INSERT INTO PRODUCAO (COD_LINHA, COD_PRODUTO, NUM_SERIE, PESO, DAT_DATA) VALUES (" + codLinha + "," + codProduto + "," + numSerie + "," + peso + ",TO_DATE(Trunc(DBMS_RANDOM.value(TO_CHAR(date '2000-01-01','J'),TO_CHAR(SYSDATE,'J'))),'J'))";

                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    textSeq.Clear();
                    textCodLinha.Clear();
                    textCodProduto.Clear();
                    textNumSerie.Clear();
                    textPeso.Clear();

                    MessageBox.Show("Producao inserido");
                    comando("SELECT * FROM PRODUCAO ORDER BY SEQ_PRODUCAO DESC");
                    montaGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao inserir.");
                    textSeq.Clear();
                    textCodLinha.Clear();
                    textCodProduto.Clear();
                    textNumSerie.Clear();
                    textPeso.Clear();
                }
            }
            /*==========
              | DELETE |
              ==========*/
            else if (delProducao)
            {
                try
                {
                    string del = textSeq.Text;
                    string cmd = "DELETE FROM PRODUCAO WHERE SEQ_PRODUCAO = " + del;
                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    textSeq.Clear();
                    textCodLinha.Clear();
                    textCodProduto.Clear();
                    textNumSerie.Clear();
                    textPeso.Clear();

                    MessageBox.Show("Producao deletado");
                    comando("SELECT * FROM PRODUCAO ORDER BY SEQ_PRODUCAO DESC");
                    montaGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro");
                    textSeq.Clear();
                    textCodLinha.Clear();
                    textCodProduto.Clear();
                    textNumSerie.Clear();
                    textPeso.Clear();
                }
            }
            /*
               ==========
             * | UPDATE |
             * ==========
             */
            else if (atuProducao)
            {
                try
                {



                    string seq = textSeq.Text;
                    string codLinha = textCodLinha.Text;
                    string codProduto = textCodProduto.Text;
                    string numSerie = textNumSerie.Text;
                    string peso = textPeso.Text;
                    string cmd;
                    cmd = "UPDATE PRODUCAO SET COD_LINHA = " + codLinha + ", COD_PRODUTO = " + codProduto + ", NUM_SERIE = " + numSerie + ", PESO = " + peso + " WHERE SEQ_PRODUCAO = " + seq;

                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    MessageBox.Show("atualizado", "Sucesso");
                    comando("SELECT * FROM PRODUCAO ORDER BY SEQ_PRODUCAO DESC");
                    montaGrid();
                    textSeq.Clear();
                    textCodLinha.Clear();
                    textCodProduto.Clear();
                    textNumSerie.Clear();
                    textPeso.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro");
                    textSeq.Clear();
                    textCodLinha.Clear();
                    textCodProduto.Clear();
                    textNumSerie.Clear();
                    textPeso.Clear();
                }
            }
        }
        #endregion
        

        #region *** CONTROLE DE PANEL COM CHECKBOX ***
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

        //CONTROLE DE PANEL2 COM CHECKBOX
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                panel2.Enabled = true;
            else
                panel2.Enabled = false;
        }
        #endregion


        #region *** FILTRO POR DATA ***
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string dataInicial = dateTimePicker1.Text;
                string datafinal = dateTimePicker2.Text;
                string cmd;
                cmd = "SELECT * FROM PRODUCAO WHERE DAT_DATA BETWEEN TO_DATE('" + dataInicial + "', 'dd/mm/yyyy hh24:mi:ss') AND TO_DATE('" + datafinal + "', 'dd/mm/yyyy hh24:mi:ss')";

                comando(cmd);
                montaGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region *** EXPORT ***
        private void ExportToExcel()
        {
            string path = @"C:\Documents and Settings\Dell\Meus documentos\Eiske\Fabrica\Fabrica\Grid.csv";
            try 
            { 
                StreamWriter sw = new StreamWriter(path, false);

                string columnHeaderText = "";

                int icolCount = gridProducao.Columns.Count;

                for (int i = 0; i < icolCount; i++)
                {
                    columnHeaderText = columnHeaderText + gridProducao.Columns[i].HeaderText + ';';
                }
                sw.WriteLine(columnHeaderText);

                foreach (DataGridViewRow dataRowObject in gridProducao.Rows)
                {
                    if (!dataRowObject.IsNewRow)
                    {
                        string dataFromGrid = "";

                        for (int i = 0; i < icolCount; i++)
                        {
                            dataFromGrid = dataFromGrid + dataRowObject.Cells[i].Value.ToString() + ';';
                        }
                        sw.WriteLine(dataFromGrid);
                    }
                }
                sw.Flush();
                sw.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
        #endregion


        #region *** INSERT COM PROCEDURE ***
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string qtd = textBox6.Text;
                string procedure;
                procedure = "P_INSERT_PRODUCAO(" + qtd + ")";
                comandoProcedure(procedure);

                MessageBox.Show("Inserido");
                textBox6.Clear();

                comando("SELECT * FROM PRODUCAO ORDER BY SEQ_PRODUCAO DESC");
                montaGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }
        #endregion


        #region *** SELECIONA LINHA GRID ***
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            insereProducao = false;
            delProducao = false;
            atuProducao = true;

            textSeq.Enabled = false;
            textCodLinha.Enabled = true;
            textCodProduto.Enabled = true;
            textNumSerie.Enabled = true;
            textPeso.Enabled = true;

            button5.Enabled = true;

            textSeq.Text = gridProducao.CurrentRow.Cells[0].Value.ToString();
            textCodLinha.Text = gridProducao.CurrentRow.Cells[1].Value.ToString();
            textCodProduto.Text = gridProducao.CurrentRow.Cells[2].Value.ToString();
            textNumSerie.Text = gridProducao.CurrentRow.Cells[3].Value.ToString();
            textPeso.Text = gridProducao.CurrentRow.Cells[4].Value.ToString();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            insereProducao = false;
            delProducao = false;
            atuProducao = true;

            textSeq.Enabled = false;
            textCodLinha.Enabled = true;
            textCodProduto.Enabled = true;
            textNumSerie.Enabled = true;
            textPeso.Enabled = true;

            button5.Enabled = true;

            textSeq.Text = gridProducao.CurrentRow.Cells[0].Value.ToString();
            textCodLinha.Text = gridProducao.CurrentRow.Cells[1].Value.ToString();
            textCodProduto.Text = gridProducao.CurrentRow.Cells[2].Value.ToString();
            textNumSerie.Text = gridProducao.CurrentRow.Cells[3].Value.ToString();
            textPeso.Text = gridProducao.CurrentRow.Cells[4].Value.ToString();
        }
        #endregion

        
        #region *** CONTROLE DE RADIO ***
        private void btnProcurar_Click(object sender, EventArgs e)
        {
            if (radioLinha.Checked)
            {
                string linha = textLinhaProduto.Text;
                string cmd;
                cmd = "SELECT * FROM PRODUCAO WHERE COD_LINHA = " + linha;
                comando(cmd);
                montaGrid();
            }
            else if (radioProduto.Checked)
            {
                string produto = textLinhaProduto.Text;
                string cmd;
                cmd = "SELECT * FROM PRODUCAO WHERE COD_PRODUTO = " + produto;
                comando(cmd);
                montaGrid();
            }
        }
        #endregion


        #region *** LIMPA CAMPO ***
        private void btnLimpafiltro_Click(object sender, EventArgs e)
        {
            textLinhaProduto.Clear();

            comando("SELECT * FROM PRODUCAO ORDER BY SEQ_PRODUCAO DESC");
            montaGrid();
        }
        #endregion
        

    }
}
