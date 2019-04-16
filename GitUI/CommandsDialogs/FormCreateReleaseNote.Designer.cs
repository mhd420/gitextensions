namespace GitUI.CommandsDialogs
{
    partial class FormCreateReleaseNote
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreateReleaseNote));
            this.Ok = new System.Windows.Forms.Button();
            this.textReleaseNote = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textNotes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Ok
            // 
            this.Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Ok.Location = new System.Drawing.Point(364, 191);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 0;
            this.Ok.Text = "Create";
            this.Ok.UseVisualStyleBackColor = false;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // textReleaseNote
            // 
            this.textReleaseNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textReleaseNote.Location = new System.Drawing.Point(3, 16);
            this.textReleaseNote.Name = "textReleaseNote";
            this.textReleaseNote.Size = new System.Drawing.Size(436, 20);
            this.textReleaseNote.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Ok, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.textReleaseNote, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textNotes, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(442, 217);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // textNotes
            // 
            this.textNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textNotes.Location = new System.Drawing.Point(3, 55);
            this.textNotes.Multiline = true;
            this.textNotes.Name = "textNotes";
            this.textNotes.Size = new System.Drawing.Size(436, 130);
            this.textNotes.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Release Note Summary";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Notes for Testing";
            // 
            // FormCreateReleaseNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 217);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCreateReleaseNote";
            this.Text = "Create Release Note";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.TextBox textReleaseNote;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textNotes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}