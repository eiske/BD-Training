using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Data;

namespace FabricaWeb
{
    public partial class Inspecao : System.Web.UI.Page
    {
        public DbCommand comandoSql;
        public DbDataReader reader;
        Conexao conn = new Conexao();

        public string Select()
        {
            if (CheckBox1.Checked)
            {
                return "SELECT * FROM INSPECAO WHERE DAT_DATA BETWEEN TO_DATE('" + Session["Inicio"].ToString() + "', 'dd/mm/yyyy hh24:mi:ss') AND TO_DATE('" + Session["Fim"].ToString() + "', 'dd/mm/yyyy hh24:mi:ss')";
            }
            else
            {
                return "SELECT * FROM INSPECAO ORDER BY SEQ_INSPECAO";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var listaInspecao = ConsultaInspecao();

                if (listaInspecao != null && listaInspecao.Count > 0)
                {
                    this.grdDados.DataSource = listaInspecao;
                    this.grdDados.DataBind();
                }
            }

            comando("SELECT DISTINCT(NUM_SERIE) FROM PRODUCAO ORDER BY NUM_SERIE");
            DropListNumSerie.DataSource = comandoSql.ExecuteReader();
            DropListNumSerie.DataTextField = "NUM_SERIE";
            DropListNumSerie.DataValueField = "NUM_SERIE";
            DropListNumSerie.DataBind();
        }

        #region *** COMANDO ***
        public void comando(string cmd)
        {
            comandoSql = conn._conexao.CreateCommand();
            comandoSql.CommandType = System.Data.CommandType.Text;
            comandoSql.CommandText = cmd;
        }
        #endregion

        #region *** LIMA CAMPOS ***
        private void LimpaCampos()
        {
            txtSeqInspecao.Text = string.Empty;
            txtCodProduto.Text = string.Empty;
            txtNumResultado.Text = string.Empty;
            txtAprovado.Text = string.Empty;
            LabelOk.Text = string.Empty;
        }
        #endregion

        #region *** CONSULTA E PREECHE DROPDOWN ***
        public List<ModeloInspecao> ConsultaInspecao()
        {
            var lstRetorno = new List<ModeloInspecao>();

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

        public List<ModeloInspecao> PreencheValores(DbDataReader _reader)
        {
            var lista = new List<ModeloInspecao>();

            while (_reader.Read())
            {
                var modelo = new ModeloInspecao();
                modelo.SEQ_INSPECAO = Convert.ToInt32(_reader["SEQ_INSPECAO"].ToString());
                modelo.COD_PRODUTO = Convert.ToInt32(_reader["COD_PRODUTO"].ToString());
                modelo.NUM_SERIE = Convert.ToInt32(_reader["NUM_SERIE"].ToString());
                modelo.APROVADO = Convert.ToChar(_reader["APROVADO"].ToString());
                modelo.NUM_RESULTADO = Convert.ToInt32(_reader["NUM_RESULTADO"].ToString());
                modelo.DAT_DATA = Convert.ToDateTime(_reader["DAT_DATA"].ToString());
                lista.Add(modelo);
            }
            return lista;
        }
        #endregion

        #region *** BOTÃO INSERE ***
        public void BtnInsereClick(object sender, EventArgs e)
        {
            controleBtn(1);
            LimpaCampos();

            txtSeqInspecao.Enabled = false;
            txtCodProduto.Enabled = true;
            DropListNumSerie.Enabled = true;
            txtAprovado.Enabled = true;
            txtNumResultado.Enabled = true;

            btnOk.Enabled = true;
        }
        #endregion

        #region *** BOTÃO ATUALIZAR ***
        public void BtnAtualizarClick(object sender, EventArgs e)
        {
            controleBtn(2);
            LimpaCampos();

            txtSeqInspecao.Enabled = true;
            txtCodProduto.Enabled = true;
            DropListNumSerie.Enabled = true;
            txtAprovado.Enabled = true;
            txtNumResultado.Enabled = true;

            btnOk.Enabled = true;
        }
        #endregion

        #region *** BOTÃO DELETAR ***
        public void BtnDeletarClick(object sender, EventArgs e)
        {
            controleBtn(3);
            LimpaCampos();

            txtSeqInspecao.Enabled = true;
            txtCodProduto.Enabled = false;
            DropListNumSerie.Enabled = false;
            txtAprovado.Enabled = false;
            txtNumResultado.Enabled = false;

            btnOk.Enabled = true;
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

        #region *** SELECIONA LINHA DA GRIDVIEW ***
        public void SelecionaLinha(Object sender, EventArgs e)
        {
            GridViewRow linha = grdDados.SelectedRow;
        }

        public void LinhaSelecionada(Object sender, GridViewSelectEventArgs e)
        {
            GridViewRow linha = grdDados.Rows[e.NewSelectedIndex];
            txtSeqInspecao.Text = linha.Cells[1].Text;
            txtCodProduto.Text = linha.Cells[2].Text;
            DropListNumSerie.Text = linha.Cells[3].Text;
            txtAprovado.Text = linha.Cells[4].Text;
            txtNumResultado.Text = linha.Cells[5].Text;
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

                var listaProduto = ConsultaInspecao();
                this.grdDados.DataSource = listaProduto;
                this.grdDados.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region *** BOTAO OK ***
        public void BtnOk(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["insere"]))
            {
                Session["SEQ_INSPECAO"] = txtSeqInspecao.Text;
                Session["COD_PRODUTO"] = txtCodProduto.Text;
                Session["NUM_SERIE"] = DropListNumSerie.Text;
                Session["APROVADO"] = txtAprovado.Text;
                Session["NUM_RESULTADO"] = txtNumResultado.Text;
                string cmd = "INSERT INTO INSPECAO (COD_PRODUTO, NUM_SERIE, APROVADO, NUM_RESULTADO, DAT_DATA) VALUES (" + Session["COD_PRODUTO"].ToString() + "," + Session["NUM_SERIE"].ToString() + ", '" + Session["APROVADO"].ToString() + "', " + Session["NUM_RESULTADO"].ToString() + ", TO_DATE(Trunc(DBMS_RANDOM.value(TO_CHAR(date '2000-01-01','J'),TO_CHAR(SYSDATE,'J'))),'J'))";
                try
                {
                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    LimpaCampos();
                    LabelOk.Visible = true;
                    LabelOk.Text = "Inserido";


                    var listaProduto = ConsultaInspecao();
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
                Session["SEQ_INSPECAO"] = txtSeqInspecao.Text;
                Session["COD_PRODUTO"] = txtCodProduto.Text;
                Session["NUM_SERIE"] = DropListNumSerie.Text;
                Session["APROVADO"] = txtAprovado.Text;
                Session["NUM_RESULTADO"] = txtNumResultado.Text;
                string cmd = "UPDATE INSPECAO SET COD_PRODUTO = " + Session["COD_PRODUTO"].ToString() + ", NUM_SERIE = " + Session["NUM_SERIE"].ToString() + ", APROVADO = '" + Session["APROVADO"].ToString() + "', NUM_RESULTADO = " + Session["NUM_RESULTADO"].ToString() + " WHERE SEQ_INSPECAO = " + Session["SEQ_INSPECAO"].ToString();
                try
                {
                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    LimpaCampos();
                    LabelOk.Visible = true;
                    LabelOk.Text = "Atualizado";


                    var listaProduto = ConsultaInspecao();
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
                Session["SEQ_INSPECAO"] = txtSeqInspecao.Text;
                string cmd = "DELETE FROM INSPECAO WHERE SEQ_INSPECAO = " + Session["SEQ_INSPECAO"].ToString();
                try
                {
                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    LimpaCampos();
                    LabelOk.Visible = true;
                    LabelOk.Text = "Deletado";

                    var listaProduto = ConsultaInspecao();
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