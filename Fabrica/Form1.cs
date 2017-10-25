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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto();
            produto.MdiParent = this;
            produto.Show();
        }

        private void linhaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Linha linha = new Linha();
            linha.Visible = true;
        }

        private void producaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Producao producao = new Producao();
            producao.MdiParent = this;
            producao.Show();
        }

        private void inspecaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inspecao inspecao = new Inspecao();
            inspecao.MdiParent = this;
            inspecao.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}
