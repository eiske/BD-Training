using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;

namespace FabricaWeb
{
    public partial class Linha : System.Web.UI.Page
    {
        public DbCommand comandoSql;
        public DbDataReader reader;
        Conexao conn = new Conexao();
        public string select = "SELECT * FROM LINHA ORDER BY SEQ_LINHA";
        //public Boolean insereLinha, delLinha, atuLinha;


        protected void Page_Load(object sender, EventArgs e)
        {
            var listaLinha = ConsultaLinha();

            if (listaLinha != null && listaLinha.Count > 0)
            {
                this.grdDados.DataSource = listaLinha;
                this.grdDados.DataBind();
            }
        }

        #region *** SELECIONA LINHA DA GRIDVIEW ***
        public void SelecionaLinha(Object sender, EventArgs e)
        {
            GridViewRow linha = grdDados.SelectedRow;
        }

        public void LinhaSelecionada(Object sender, GridViewSelectEventArgs e)
        {
            GridViewRow linha = grdDados.Rows[e.NewSelectedIndex];
            txtSeq.Text = linha.Cells[1].Text;
            txtCodLinha.Text = linha.Cells[2].Text;
            txtDescLinha.Text = linha.Cells[3].Text;
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

        #region *** CONSULTA LINHA ***
        public List<ModeloLinha> ConsultaLinha()
        {
            var lstRetorno = new List<ModeloLinha>();

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

        #region *** PREENCE VALORES ***
        public List<ModeloLinha> PreencheValores(DbDataReader _reader)
        {
            var lista = new List<ModeloLinha>();

            while (_reader.Read())
            {
                var modeloLinha = new ModeloLinha();
                modeloLinha.SEQ_LINHA = Convert.ToInt32(_reader["SEQ_LINHA"].ToString());
                modeloLinha.COD_LINHA = Convert.ToInt32(_reader["COD_LINHA"].ToString());
                modeloLinha.DES_LINHA = _reader["DES_LINHA"].ToString();
                lista.Add(modeloLinha);
            }
            return lista;
        }
        #endregion

        #region *** BOTÃO INSERE ***
        public void BtnInsereClick(object sender, EventArgs e)
        {
            controleBtn(1);

            LimpaCampos();
            txtSeq.Enabled = false;
            txtCodLinha.Visible = true;
            txtCodLinha.Enabled = true;
            DropDownCodLinha.Enabled = false;
            DropDownCodLinha.Visible = false;
            txtDescLinha.Enabled = true;

            btnOk.Enabled = true;
        }
        #endregion

        #region *** BOTÃO ATUALIZAR ***
        public void BtnAtualizarClick(object sender, EventArgs e)
        {
            controleBtn(2);

            LimpaCampos();
            txtSeq.Enabled = true;
            txtCodLinha.Visible = true;
            txtCodLinha.Enabled = true;
            DropDownCodLinha.Enabled = false;
            DropDownCodLinha.Visible = false;
            txtDescLinha.Enabled = true;

            btnOk.Enabled = true;
        }
        #endregion

        #region *** BOTÃO DELETAR ***
        public void BtnDeletarClick(object sender, EventArgs e)
        {
            controleBtn(3);

            LimpaCampos();
            txtSeq.Enabled = false;
            txtCodLinha.Enabled = false;
            txtCodLinha.Visible = false;
            DropDownCodLinha.Enabled = true;
            DropDownCodLinha.Visible = true;
            txtDescLinha.Enabled = false;

            comando("SELECT COD_LINHA FROM LINHA ORDER BY TO_NUMBER(COD_LINHA)");
            DropDownCodLinha.DataSource = comandoSql.ExecuteReader();
            DropDownCodLinha.DataTextField = "COD_LINHA";
            DropDownCodLinha.DataValueField = "COD_LINHA";
            DropDownCodLinha.DataBind();

            btnOk.Enabled = true;
        }
        #endregion

        #region *** LIMPA CAMPOS ***
        private void LimpaCampos()
        {
            txtSeq.Text = string.Empty;
            txtCodLinha.Text = string.Empty;
            txtDescLinha.Text = string.Empty;
            LabelOk.Text = string.Empty;
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

        public void BtnOk_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["insere"]))
            {
                Session["SEQ_LINHA"] = txtSeq.Text;
                Session["COD_LINHA"] = txtCodLinha.Text;
                Session["DES_LINHA"] = txtDescLinha.Text;
                string cmd = "INSERT INTO LINHA (COD_LINHA, DES_LINHA) VALUES (" + Session["COD_LINHA"].ToString() + ",'" + Session["DES_LINHA"].ToString() + "')";
                try
                {
                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    LimpaCampos();
                    LabelOk.Visible = true;
                    LabelOk.Text = "Inserido";

                    var lista = ConsultaLinha();
                    this.grdDados.DataSource = lista;
                    this.grdDados.DataBind();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            else if (Convert.ToBoolean(Session["atualiza"]))
            {
                Session["SEQ_LINHA"] = txtSeq.Text;
                Session["COD_LINHA"] = txtCodLinha.Text;
                Session["DES_LINHA"] = txtDescLinha.Text;
                string cmd = "UPDATE LINHA SET COD_LINHA = " + Session["COD_LINHA"].ToString() + ", DES_LINHA = '" + Session["DES_LINHA"].ToString() + "' WHERE SEQ_LINHA = " + Session["SEQ_LINHA"].ToString();
                try
                {
                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    LimpaCampos();
                    LabelOk.Visible = true;
                    LabelOk.Text = "Atualizado";

                    var lista = ConsultaLinha();
                    this.grdDados.DataSource = lista;
                    this.grdDados.DataBind();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            else if (Convert.ToBoolean(Session["deleta"]))
            {
                Session["COD_LINHA"] = DropDownCodLinha.Text;
                string cmd = "DELETE FROM LINHA WHERE COD_LINHA = " + Session["COD_LINHA"];
                try
                {
                    comando(cmd);
                    comandoSql.ExecuteNonQuery();
                    comando("COMMIT");
                    comandoSql.ExecuteNonQuery();

                    LimpaCampos();
                    LabelOk.Visible = true;
                    LabelOk.Text = "Deletado";
                    DropDownCodLinha.SelectedIndex = 0;
                    var listaProduto = ConsultaLinha();
                    this.grdDados.DataSource = listaProduto;
                    this.grdDados.DataBind();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}