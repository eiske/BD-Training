using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;
using FabricaWeb;

namespace FabricaWeb
{
    public partial class Produto : System.Web.UI.Page
    {
        public string select = "SELECT * FROM PRODUTO ORDER BY TO_NUMBER(COD_PRODUTO)";
        Conexao conn = new Conexao();

        public DbCommand comandoSql;
        public DbDataReader reader;
        public Boolean insereProduto, delProduto, atuProduto;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                var listaProduto = ConsultaProduto();

                if (listaProduto != null && listaProduto.Count > 0)
                {
                    this.grdDados.DataSource = listaProduto;
                    this.grdDados.DataBind();
                }
            }
        }

        #region *** CONSULTA PRODUTO ***
        public List<ModeloProduto> ConsultaProduto()
        {
            var lstRetorno = new List<ModeloProduto>();

            try
            {
                comando(select);
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

        #region *** PREENCHE VALORES ***
        public List<ModeloProduto> PreencheValores(DbDataReader _reader)
        {
            var lista = new List<ModeloProduto>();

            while (_reader.Read())
            {
                var modelo = new ModeloProduto();
                modelo.SEQ_PRODUTO = Convert.ToInt32(_reader["SEQ_PRODUTO"].ToString());
                modelo.COD_PRODUTO = Convert.ToInt32(_reader["COD_PRODUTO"].ToString());
                modelo.COR = _reader["COR"].ToString();
                modelo.COD_FAMILIA = Convert.ToInt32(_reader["COD_FAMILIA"].ToString());
                lista.Add(modelo);
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
            txtSeqProduto.Text = linha.Cells[1].Text;
            txtProduto.Text = linha.Cells[2].Text;
            txtCor.Text = linha.Cells[3].Text;
            txtFamilia.Text = linha.Cells[4].Text;
        }
        #endregion

        #region *** COMANDO ***
        public void comando(string cmd)
        {
            comandoSql = conn._conexao.CreateCommand();
            comandoSql.CommandType = System.Data.CommandType.Text;
            comandoSql.CommandText = cmd;
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

        #region *** LIMPA CAMPOS ***
        private void LimpaCampos()
        {
            txtSeqProduto.Text = string.Empty;
            txtProduto.Text = string.Empty;
            txtCor.Text = string.Empty;
            txtFamilia.Text = string.Empty;
            CheckBox1.Checked = false;
            LabelOk.Text = string.Empty;
        }
        #endregion

        #region *** COMANDO PROCEDURE ***
        private void comandoProcedure(string cmd)
        {
            comandoSql = conn._conexao.CreateCommand();
            comandoSql.CommandType = System.Data.CommandType.StoredProcedure;
            comandoSql.CommandText = cmd;
            comandoSql.ExecuteNonQuery();
        }
        #endregion

        #region *** BOTÃO INSERIR ***
        public void BtnInsereClick(object sender, EventArgs e)
        {
            controleBtn(1);

            LimpaCampos();

            txtSeqProduto.Enabled = false;
            txtProduto.Enabled = true;
            txtProduto.Visible = true;
            DropDownCodProduto.Enabled = false;
            DropDownCodProduto.Visible = false;
            txtCor.Enabled = true;
            txtFamilia.Enabled = true;
            CheckBox1.Visible = false;

            btnOk.Enabled = true;
        }
        #endregion

        #region *** BOTÃO ATUALIZAR ***
        public void BtnAtualizarClick(object sender, EventArgs e)
        {
            controleBtn(2);

            LimpaCampos();

            txtSeqProduto.Enabled = true;
            txtProduto.Enabled = true;
            txtProduto.Visible = true;
            DropDownCodProduto.Enabled = false;
            DropDownCodProduto.Visible = false;
            txtCor.Enabled = true;
            txtFamilia.Enabled = true;
            CheckBox1.Visible = false;

            btnOk.Enabled = true;
        }
        #endregion

        #region *** BOTÃO DELETAR ***
        public void BtnDeletarClick(object sender, EventArgs e)
        {
            controleBtn(3);

            LimpaCampos();

            txtSeqProduto.Enabled = false;
            txtProduto.Enabled = false;
            txtProduto.Visible = false;
            DropDownCodProduto.Enabled = true;
            DropDownCodProduto.Visible = true;
            txtCor.Enabled = false;
            txtFamilia.Enabled = false;
            CheckBox1.Visible = true;

            comando("SELECT COD_PRODUTO FROM PRODUTO ORDER BY TO_NUMBER(COD_PRODUTO)");
            DropDownCodProduto.DataSource = comandoSql.ExecuteReader();
            DropDownCodProduto.DataTextField = "COD_PRODUTO";
            DropDownCodProduto.DataValueField = "COD_PRODUTO";
            DropDownCodProduto.DataBind();

            btnOk.Enabled = true;
        }
        #endregion

        #region *** BOTAO OK ***
        public void BtnOk(object sender, EventArgs e)
        {            
            if (Convert.ToBoolean(Session["insere"]))
            {
                Session["SEQ_PRODUTO"] = txtSeqProduto.Text;
                Session["COD_PRODUTO"] = txtProduto.Text;
                Session["COR"] = txtCor.Text;
                Session["COD_FAMILIA"] = txtFamilia.Text;
                string cmd = "INSERT INTO PRODUTO (COD_PRODUTO, COR, COD_FAMILIA) VALUES (" + Session["COD_PRODUTO"].ToString() + ", '" + Session["COR"].ToString() + "', " + Session["COD_FAMILIA"].ToString() + ")";
                try
                {
                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    LimpaCampos();
                    LabelOk.Visible = true;
                    LabelOk.Text = "Inserido";


                    var listaProduto = ConsultaProduto();
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
                Session["SEQ_PRODUTO"] = txtSeqProduto.Text;
                Session["COD_PRODUTO"] = txtProduto.Text;
                Session["COR"] = txtCor.Text;
                Session["COD_FAMILIA"] = txtFamilia.Text;
                string cmd = "UPDATE PRODUTO SET COD_PRODUTO = " + Session["COD_PRODUTO"].ToString() + ", COR = '" + Session["COR"].ToString() + "', COD_FAMILIA = " + Session["COD_FAMILIA"].ToString() + "WHERE SEQ_PRODUTO = " + Session["SEQ_PRODUTO"].ToString();
                try
                {
                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    LimpaCampos();
                    LabelOk.Visible = true;
                    LabelOk.Text = "Atualizado";


                    var listaProduto = ConsultaProduto();
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
                Session["COD_PRODUTO"] = DropDownCodProduto.Text;
                string cmd = "DELETE FROM PRODUTO WHERE COD_PRODUTO = " + Session["COD_PRODUTO"];
                try
                {
                    if (CheckBox1.Checked)
                    {
                        string procedure = "P_DELETE_PRODUTO(" + Session["COD_PRODUTO"].ToString() + ")";
                        comandoProcedure(procedure);
                        comando("COMMIT");
                        comandoSql.ExecuteNonQuery();

                        LimpaCampos();
                        LabelOk.Visible = true;
                        LabelOk.Text = "Deletado";
                        DropDownCodProduto.SelectedIndex = 0;
                        var listaProduto = ConsultaProduto();
                        this.grdDados.DataSource = listaProduto;
                        this.grdDados.DataBind();
                    }
                    else
                    {
                        comando(cmd);
                        comandoSql.ExecuteNonQuery();
                        comando("COMMIT");
                        comandoSql.ExecuteNonQuery();

                        LimpaCampos();
                        LabelOk.Visible = true;
                        LabelOk.Text = "Deletado";


                        DropDownCodProduto.SelectedIndex = 0;
                        var listaProduto = ConsultaProduto();
                        this.grdDados.DataSource = listaProduto;
                        this.grdDados.DataBind();
                    }
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