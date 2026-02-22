namespace App.Forms
{
    partial class AddConnectionForm
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
            textBoxName = new TextBox();
            textBoxURL = new TextBox();
            btnAdd = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // textBoxName
            // 
            textBoxName.Font = new Font("Segoe UI", 13F);
            textBoxName.Location = new Point(39, 42);
            textBoxName.Name = "textBoxName";
            textBoxName.PlaceholderText = "Naziv Povezave";
            textBoxName.Size = new Size(228, 31);
            textBoxName.TabIndex = 1;
            // 
            // textBoxURL
            // 
            textBoxURL.Font = new Font("Segoe UI", 13F);
            textBoxURL.Location = new Point(39, 99);
            textBoxURL.Name = "textBoxURL";
            textBoxURL.PlaceholderText = "URL";
            textBoxURL.Size = new Size(228, 31);
            textBoxURL.TabIndex = 2;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 13F);
            btnAdd.Location = new Point(199, 156);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(86, 34);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Dodaj";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 13F);
            btnCancel.Location = new Point(291, 156);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(86, 34);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Prekliči";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // AddConnectionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(389, 206);
            Controls.Add(btnCancel);
            Controls.Add(btnAdd);
            Controls.Add(textBoxURL);
            Controls.Add(textBoxName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddConnectionForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Dodaj Povezavo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBoxName;
        private TextBox textBoxURL;
        private Button btnAdd;
        private Button btnCancel;
    }
}