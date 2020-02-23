namespace cmaftei_Assembler_CS4100
{
    partial class Form_Cmaftei_Assembler
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
            this.btn_assemble = new System.Windows.Forms.Button();
            this.richTxt_asmFileContents = new System.Windows.Forms.RichTextBox();
            this.richTxt_Binary = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_showSymbolTable = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_assemble
            // 
            this.btn_assemble.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_assemble.Location = new System.Drawing.Point(485, 272);
            this.btn_assemble.Name = "btn_assemble";
            this.btn_assemble.Size = new System.Drawing.Size(217, 92);
            this.btn_assemble.TabIndex = 0;
            this.btn_assemble.Text = "Assemble!";
            this.btn_assemble.UseVisualStyleBackColor = true;
            this.btn_assemble.Click += new System.EventHandler(this.btn_assemble_Click);
            // 
            // richTxt_asmFileContents
            // 
            this.richTxt_asmFileContents.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTxt_asmFileContents.Location = new System.Drawing.Point(31, 68);
            this.richTxt_asmFileContents.Name = "richTxt_asmFileContents";
            this.richTxt_asmFileContents.Size = new System.Drawing.Size(448, 543);
            this.richTxt_asmFileContents.TabIndex = 1;
            this.richTxt_asmFileContents.Text = "";
            // 
            // richTxt_Binary
            // 
            this.richTxt_Binary.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTxt_Binary.Location = new System.Drawing.Point(708, 68);
            this.richTxt_Binary.Name = "richTxt_Binary";
            this.richTxt_Binary.Size = new System.Drawing.Size(448, 543);
            this.richTxt_Binary.TabIndex = 2;
            this.richTxt_Binary.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(93, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Place .asm Contents Here!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(754, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(369, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "Press \"Assemble!\" To Get Binary!";
            // 
            // btn_showSymbolTable
            // 
            this.btn_showSymbolTable.Enabled = false;
            this.btn_showSymbolTable.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_showSymbolTable.Location = new System.Drawing.Point(502, 461);
            this.btn_showSymbolTable.Name = "btn_showSymbolTable";
            this.btn_showSymbolTable.Size = new System.Drawing.Size(183, 66);
            this.btn_showSymbolTable.TabIndex = 5;
            this.btn_showSymbolTable.Text = "Show Symbol Table";
            this.btn_showSymbolTable.UseVisualStyleBackColor = true;
            this.btn_showSymbolTable.Click += new System.EventHandler(this.btn_showSymbolTable_Click);
            // 
            // Form_Cmaftei_Assembler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1178, 644);
            this.Controls.Add(this.btn_showSymbolTable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTxt_Binary);
            this.Controls.Add(this.richTxt_asmFileContents);
            this.Controls.Add(this.btn_assemble);
            this.Name = "Form_Cmaftei_Assembler";
            this.Text = "Cmaftei Assembler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_assemble;
        private System.Windows.Forms.RichTextBox richTxt_asmFileContents;
        private System.Windows.Forms.RichTextBox richTxt_Binary;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_showSymbolTable;
    }
}

