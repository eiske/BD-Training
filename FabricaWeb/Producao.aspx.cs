using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Text;

namespace FabricaWeb
{
    public partial class Producao : System.Web.UI.Page
    {
        public DbCommand comandoSql;
        public DbDataReader reader;
        Conexao conn = new Conexao();

        public string Select()
        {
            if (CheckBox1.Checked)
            {
                return "SELECT * FROM PRODUCAO WHERE DAT_DATA BETWEEN TO_DATE('" + Session["Inicio"].ToString() + "', 'dd/mm/yyyy hh24:mi:ss') AND TO_DATE('" + Session["Fim"].ToString() + "', 'dd/mm/yyyy hh24:mi:ss')";
            }
            else
            {
                return "SELECT * FROM PRODUCAO ORDER BY SEQ_PRODUCAO";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            var listaProducao = ConsultaProducao();

            if (listaProducao != null && listaProducao.Count > 0)
            {
                this.grdDados.DataSource = listaProducao;
                this.grdDados.DataBind();
            }
            
            DataFim.SelectedDate = DateTime.Today;
        }

        #region *** COMANDO ***
        public void comando(string cmd)
        {
            comandoSql = conn._conexao.CreateCommand();
            comandoSql.CommandType = System.Data.CommandType.Text;
            comandoSql.CommandText = cmd;
        }
        #endregion

        #region *** LIMPA CAMPOS ***
        private void LimpaCampos()
        {
            txtSeqProducao.Text = string.Empty;
            txtCodLinha.Text = string.Empty;
            txtCodProduto.Text = string.Empty;
            txtNumserie.Text = string.Empty;
            txtPeso.Text = string.Empty;
            LabelOk.Text = string.Empty;
        }
        #endregion

        #region *** CONSULTA PRODUCAO ***
        public List<ModeloProducao> ConsultaProducao()
        {
            var lstRetorno = new List<ModeloProducao>();

            try
            {
                comando(Select());
                var objDataReader = comandoSql.ExecuteReader();
                if (objDataReader.HasRows)
                    lstRetorno = PreencheValores(objDataReader);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lstRetorno;
        }
        #endregion

        #region *** EXPORTAR ***
        public void exportar(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("contet-disposition", string.Format("attachment;filename=GridViewExport.csv"));
            Response.Charset = "";
            Response.ContentType = "application/text";

            grdDados.AllowPaging = false;
            grdDados.DataBind();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < grdDados.Columns.Count; i++)
            {
                sb.Append(grdDados.Columns[i].HeaderText + ';');
            }
            sb.Append("\r\n");
            for (int i = 1; i < grdDados.Rows.Count; i++)
            {
                for (int j = 0; j < grdDados.Columns.Count; j++)
                {
                    sb.Append(grdDados.Rows[i].Cells[j+1].Text + ';');
                }
                sb.Append("\r\n");
            }

            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }
        #endregion

        #region *** PREENCHE VALORES ***
        public List<ModeloProducao> PreencheValores(DbDataReader _reader)
        {
            var lista = new List<ModeloProducao>();

            while (_reader.Read())
            {
                var modeloProducao = new ModeloProducao();
                modeloProducao.SEQ_PRODUCAO = Convert.ToInt32(_reader["SEQ_PRODUCAO"].ToString());
                modeloProducao.COD_LINHA = Convert.ToInt32(_reader["COD_LINHA"].ToString());
                modeloProducao.COD_PRODUTO = Convert.ToInt32(_reader["COD_PRODUTO"].ToString());
                modeloProducao.NUM_SERIE = Convert.ToInt32(_reader["NUM_SERIE"].ToString());
                modeloProducao.PESO = Convert.ToDouble(_reader["PESO"].ToString());
                modeloProducao.DAT_DATA = Convert.ToDateTime(_reader["DAT_DATA"].ToString());
                lista.Add(modeloProducao);
            }
            return lista;
        }
        #endregion

        #region *** SELECIONA LINHA DA GRIDVIEW ***
        public void SelecionaLinha(Object sender, EventArgs e)
        {
            GridViewRow linha = grdDados.SelectedRow;
        }

        public void LinhaSelecionada(Object sender, GridViewSelectEventArgs e)
        {
            GridViewRow linha = grdDados.Rows[e.NewSelectedIndex];
            txtSeqProducao.Text = linha.Cells[1].Text;
            txtCodLinha.Text = linha.Cells[2].Text;
            txtCodProduto.Text = linha.Cells[3].Text;
            txtNumserie.Text = linha.Cells[4].Text;
            txtPeso.Text = linha.Cells[5].Text;
        }
        #endregion

        #region *** BOTAO FILTRAR ***
        public void BtnFiltrar(object sender, EventArgs e)
        {
            try
            {
                Session["Inicio"] = DataInicio.SelectedDate;
                Session["Fim"] = DataFim.SelectedDate;
                comando(Select());

                var listaProduto = ConsultaProducao();
                this.grdDados.DataSource = listaProduto;
                this.grdDados.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region *** CONTROLE BTN OK ***
        public void controleBtn(int ctrl)
        {
            switch (ctrl)
            {
                case 1:
                    Session["insere"] = true;
                    Session["atualiza"] = false;
                    Session["deleta"] = false;
                    break;
                case 2:
                    Session["insere"] = false;
                    Session["atualiza"] = true;
                    Session["deleta"] = false;
                    break;
                case 3:
                    Session["insere"] = false;
                    Session["atualiza"] = false;
                    Session["deleta"] = true;
                    break;
            }
        }
        #endregion

        #region *** BOTÃO INSERE ***
        public void BtnInsereClick(object sender, EventArgs e)
        {
            controleBtn(1);
            LimpaCampos();

            txtSeqProducao.Enabled = false;
            txtCodLinha.Enabled = true;
            txtCodProduto.Enabled = true;
            txtNumserie.Enabled = true;
            txtPeso.Enabled = true;

            btnOk.Enabled = true;
        }
        #endregion

        #region *** BOTÃO ATUALIZAR ***
        public void BtnAtualizarClick(object sender, EventArgs e)
        {
            controleBtn(2);
            LimpaCampos();

            txtSeqProducao.Enabled = true;
            txtCodLinha.Enabled = true;
            txtCodProduto.Enabled = true;
            txtNumserie.Enabled = true;
            txtPeso.Enabled = true;

            btnOk.Enabled = true;
        }
        #endregion

        #region *** BOTÃO DELETAR ***
        public void BtnDeletarClick(object sender, EventArgs e)
        {
            controleBtn(3);
            LimpaCampos();

            txtSeqProducao.Enabled = true;
            txtCodLinha.Enabled = false;
            txtCodProduto.Enabled = false;
            txtNumserie.Enabled = false;
            txtPeso.Enabled = false;

            btnOk.Enabled = true;
        }
        #endregion

        #region *** BOTAO OK ***
        public void BtnOk(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["insere"]))
            {
                Session["SEQ_PRODUCAO"] = txtSeqProducao.Text;
                Session["COD_LINHA"] = txtCodLinha.Text;
                Session["COD_PRODUTO"] = txtCodProduto.Text;
                Session["NUM_SERIE"] = txtNumserie.Text;
                Session["PESO"] = txtPeso.Text;
                string cmd = "INSERT INTO PRODUCAO (COD_LINHA, COD_PRODUTO, NUM_SERIE, PESO, DAT_DATA) VALUES (" + Session["COD_LINHA"].ToString() + "," + Session["COD_PRODUTO"].ToString() + "," + Session["NUM_SERIE"].ToString() + "," + Session["PESO"].ToString() + ",TO_DATE(Trunc(DBMS_RANDOM.value(TO_CHAR(date '2000-01-01','J'),TO_CHAR(SYSDATE,'J'))),'J'))";
                try
                {
                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    LimpaCampos();
                    LabelOk.Visible = true;
                    LabelOk.Text = "Inserido";


                    var listaProduto = ConsultaProducao();
                    this.grdDados.DataSource = listaProduto;
                    this.grdDados.DataBind();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else if (Convert.ToBoolean(Session["atualiza"]))
            {
                Session["SEQ_PRODUCAO"] = txtSeqProducao.Text;
                Session["COD_LINHA"] = txtCodLinha.Text;
                Session["COD_PRODUTO"] = txtCodProduto.Text;
                Session["NUM_SERIE"] = txtNumserie.Text;
                Session["PESO"] = txtPeso.Text;
                string cmd = "UPDATE PRODUCAO SET COD_LINHA = " + Session["COD_LINHA"].ToString() + ", COD_PRODUTO = " + Session["COD_PRODUTO"].ToString() + ", NUM_SERIE = " + Session["NUM_SERIE"].ToString() + ", PESO = " + Session["PESO"].ToString() + " WHERE SEQ_PRODUCAO = " + Session["SEQ_PRODUCAO"].ToString();
                try
                {
                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    LimpaCampos();
                    LabelOk.Visible = true;
                    LabelOk.Text = "Atualizado";


                    var listaProduto = ConsultaProducao();
                    this.grdDados.DataSource = listaProduto;
                    this.grdDados.DataBind();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else if (Convert.ToBoolean(Session["deleta"]))
            {
                Session["SEQ_PRODUCAO"] = txtSeqProducao.Text;
                string cmd = "DELETE FROM PRODUCAO WHERE SEQ_PRODUCAO = " + Session["SEQ_PRODUCAO"];
                try
                {
                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    LimpaCampos();
                    LabelOk.Visible = true;
                    LabelOk.Text = "Deletado";

                    var listaProduto = ConsultaProducao();
                    this.grdDados.DataSource = listaProduto;
                    this.grdDados.DataBind();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        #endregion
    }
}