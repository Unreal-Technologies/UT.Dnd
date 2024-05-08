using System.Drawing;
using UT.Data.Controls.Validated;

namespace UT.Dnd.Mapping
{
    public partial class MapEditor
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
            tabControl = new System.Windows.Forms.TabControl();
            tabPage_List = new System.Windows.Forms.TabPage();
            tabPage_add = new System.Windows.Forms.TabPage();
            tabPage_add_lbl_name = new System.Windows.Forms.Label();
            tabPage_add_vtb_name = new ValidatedTextBox();
            tabPage_add_btn_save = new System.Windows.Forms.Button();
            tabControl.SuspendLayout();
            tabPage_add.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPage_List);
            tabControl.Controls.Add(tabPage_add);
            tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl.Location = new Point(0, 50);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(2040, 1105);
            tabControl.TabIndex = 1;
            // 
            // tabPage_List
            // 
            tabPage_List.Location = new Point(4, 29);
            tabPage_List.Name = "tabPage_List";
            tabPage_List.Padding = new System.Windows.Forms.Padding(3);
            tabPage_List.Size = new Size(2032, 1072);
            tabPage_List.TabIndex = 0;
            tabPage_List.Text = "List";
            tabPage_List.UseVisualStyleBackColor = true;
            // 
            // tabPage_add
            // 
            tabPage_add.Controls.Add(tabPage_add_btn_save);
            tabPage_add.Controls.Add(tabPage_add_vtb_name);
            tabPage_add.Controls.Add(tabPage_add_lbl_name);
            tabPage_add.Location = new Point(4, 29);
            tabPage_add.Name = "tabPage_add";
            tabPage_add.Padding = new System.Windows.Forms.Padding(3);
            tabPage_add.Size = new Size(2032, 1072);
            tabPage_add.TabIndex = 1;
            tabPage_add.Text = "Add";
            tabPage_add.UseVisualStyleBackColor = true;
            // 
            // tabPage_add_lbl_name
            // 
            tabPage_add_lbl_name.AutoSize = true;
            tabPage_add_lbl_name.Location = new Point(23, 21);
            tabPage_add_lbl_name.Name = "tabPage_add_lbl_name";
            tabPage_add_lbl_name.Size = new Size(64, 20);
            tabPage_add_lbl_name.TabIndex = 0;
            tabPage_add_lbl_name.Text = "Name:";
            // 
            // tabPage_add_vtb_name
            // 
            tabPage_add_vtb_name.IsRequired = true;
            tabPage_add_vtb_name.Location = new Point(93, 18);
            tabPage_add_vtb_name.Name = "tabPage_add_vtb_name";
            tabPage_add_vtb_name.Size = new Size(200, 23);
            tabPage_add_vtb_name.TabIndex = 1;
            // 
            // tabPage_add_btn_save
            // 
            tabPage_add_btn_save.Location = new Point(93, 47);
            tabPage_add_btn_save.Name = "tabPage_add_btn_save";
            tabPage_add_btn_save.Size = new Size(200, 34);
            tabPage_add_btn_save.TabIndex = 2;
            tabPage_add_btn_save.Text = "Save";
            tabPage_add_btn_save.UseVisualStyleBackColor = true;
            tabPage_add_btn_save.Click += TabPage_add_btn_save_Click;
            // 
            // MapEditor
            // 
            ClientSize = new Size(2040, 1155);
            Controls.Add(tabControl);
            Name = "MapEditor";
            Controls.SetChildIndex(tabControl, 0);
            tabControl.ResumeLayout(false);
            tabPage_add.ResumeLayout(false);
            tabPage_add.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_add;
        private System.Windows.Forms.TabPage tabPage_List;
        private ValidatedTextBox tabPage_add_vtb_name;
        private System.Windows.Forms.Label tabPage_add_lbl_name;
        private System.Windows.Forms.Button tabPage_add_btn_save;
    }
}
