namespace Fabrica
{
    partial class Inspecao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.gridInspecao = new System.Windows.Forms.DataGridView();
            this.textSeq = new System.Windows.Forms.TextBox();
            this.textCodProd = new System.Windows.Forms.TextBox();
            this.textNumSerie = new System.Windows.Forms.TextBox();
            this.textAp = new System.Windows.Forms.TextBox();
            this.textNumResultado = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textProduto = new System.Windows.Forms.TextBox();
            this.checkCodProduto = new System.Windows.Forms.CheckBox();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.combNumSerie = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridInspecao)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(101, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Inserir";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(194, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Atualizar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(284, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Deletar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(310, 152);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "Ok";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // gridInspecao
            // 
            this.gridInspecao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridInspecao.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridInspecao.Location = new System.Drawing.Point(12, 191);
            this.gridInspecao.Name = "gridInspecao";
            this.gridInspecao.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridInspecao.Size = new System.Drawing.Size(748, 155);
            this.gridInspecao.TabIndex = 5;
            this.gridInspecao.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // textSeq
            // 
            this.textSeq.Enabled = false;
            this.textSeq.Location = new System.Drawing.Point(178, 50);
            this.textSeq.Name = "textSeq";
            this.textSeq.Size = new System.Drawing.Size(100, 20);
            this.textSeq.TabIndex = 6;
            // 
            // textCodProd
            // 
            this.textCodProd.Enabled = false;
            this.textCodProd.Location = new System.Drawing.Point(178, 76);
            this.textCodProd.Name = "textCodProd";
            this.textCodProd.Size = new System.Drawing.Size(100, 20);
            this.textCodProd.TabIndex = 7;
            // 
            // textNumSerie
            // 
            this.textNumSerie.Enabled = false;
            this.textNumSerie.Location = new System.Drawing.Point(178, 102);
            this.textNumSerie.Name = "textNumSerie";
            this.textNumSerie.Size = new System.Drawing.Size(100, 20);
            this.textNumSerie.TabIndex = 8;
            // 
            // textAp
            // 
            this.textAp.Enabled = false;
            this.textAp.Location = new System.Drawing.Point(178, 128);
            this.textAp.Name = "textAp";
            this.textAp.Size = new System.Drawing.Size(100, 20);
            this.textAp.TabIndex = 9;
            // 
            // textNumResultado
            // 
            this.textNumResultado.Enabled = false;
            this.textNumResultado.Location = new System.Drawing.Point(178, 154);
            this.textNumResultado.Name = "textNumResultado";
            this.textNumResultado.Size = new System.Drawing.Size(100, 20);
            this.textNumResultado.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Seq Inspecao";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Cod Produto";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Num Serie";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(76, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Aprovado";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(76, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Num Resultado";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(365, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 79);
            this.panel1.TabIndex = 16;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(49, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(104, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Ate:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "De:";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(107, 18);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(88, 20);
            this.dateTimePicker2.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(5, 18);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(89, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(365, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(93, 17);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "Filtrar por data";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.textProduto);
            this.panel2.Enabled = false;
            this.panel2.Location = new System.Drawing.Point(571, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(189, 50);
            this.panel2.TabIndex = 18;
            // 
            // textProduto
            // 
            this.textProduto.Location = new System.Drawing.Point(3, 14);
            this.textProduto.Name = "textProduto";
            this.textProduto.Size = new System.Drawing.Size(100, 20);
            this.textProduto.TabIndex = 1;
            // 
            // checkCodProduto
            // 
            this.checkCodProduto.AutoSize = true;
            this.checkCodProduto.Location = new System.Drawing.Point(571, 12);
            this.checkCodProduto.Name = "checkCodProduto";
            this.checkCodProduto.Size = new System.Drawing.Size(109, 17);
            this.checkCodProduto.TabIndex = 19;
            this.checkCodProduto.Text = "Listar por Produto";
            this.checkCodProduto.UseVisualStyleBackColor = true;
            this.checkCodProduto.CheckedChanged += new System.EventHandler(this.checkCodProduto_CheckedChanged);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(418, 152);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(109, 23);
            this.btnLimpar.TabIndex = 20;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // combNumSerie
            // 
            this.combNumSerie.FormattingEnabled = true;
            this.combNumSerie.Location = new System.Drawing.Point(178, 101);
            this.combNumSerie.Name = "combNumSerie";
            this.combNumSerie.Size = new System.Drawing.Size(100, 21);
            this.combNumSerie.TabIndex = 21;
            this.combNumSerie.Enabled = false;
            // 
            // Inspecao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 355);
            this.Controls.Add(this.combNumSerie);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.checkCodProduto);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textNumResultado);
            this.Controls.Add(this.textAp);
            this.Controls.Add(this.textNumSerie);
            this.Controls.Add(this.textCodProd);
            this.Controls.Add(this.textSeq);
            this.Controls.Add(this.gridInspecao);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Name = "Inspecao";
            this.Text = "Inspecao";
            this.Load += new System.EventHandler(this.Form6_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridInspecao)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridView gridInspecao;
        private System.Windows.Forms.TextBox textSeq;
        private System.Windows.Forms.TextBox textCodProd;
        private System.Windows.Forms.TextBox textNumSerie;
        private System.Windows.Forms.TextBox textAp;
        private System.Windows.Forms.TextBox textNumResultado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textProduto;
        private System.Windows.Forms.CheckBox checkCodProduto;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.ComboBox combNumSerie;

    }
}