using System.Drawing;
using UT.Data.Controls.Validated;

namespace UT.Dnd.Modules.Mapping
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
            tabControl = new TabControl();
            tabPage_List = new TabPage();
            tabPage_add = new TabPage();
            tabPage_add_btn_save = new Button();
            tabPage_add_vtb_name = new ValidatedTextBox();
            tabPage_add_lbl_name = new Label();
            tabPage_edit = new TabPage();
            button1 = new Button();
            tabPage_delete = new TabPage();
            tabPage_delete_tb_id = new TextBox();
            tabPage_delete_btn_no = new Button();
            tabPage_delete_btn_yes = new Button();
            tabPage_delete_lbl_message = new Label();
            tabControl.SuspendLayout();
            tabPage_add.SuspendLayout();
            tabPage_edit.SuspendLayout();
            tabPage_delete.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPage_List);
            tabControl.Controls.Add(tabPage_add);
            tabControl.Controls.Add(tabPage_edit);
            tabControl.Controls.Add(tabPage_delete);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 50);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(2040, 1105);
            tabControl.TabIndex = 1;
            // 
            // tabPage_List
            // 
            tabPage_List.Location = new Point(4, 24);
            tabPage_List.Name = "tabPage_List";
            tabPage_List.Padding = new Padding(3);
            tabPage_List.Size = new Size(2032, 1077);
            tabPage_List.TabIndex = 0;
            tabPage_List.Text = "List";
            tabPage_List.UseVisualStyleBackColor = true;
            // 
            // tabPage_add
            // 
            tabPage_add.Controls.Add(tabPage_add_btn_save);
            tabPage_add.Controls.Add(tabPage_add_vtb_name);
            tabPage_add.Controls.Add(tabPage_add_lbl_name);
            tabPage_add.Location = new Point(4, 24);
            tabPage_add.Name = "tabPage_add";
            tabPage_add.Padding = new Padding(3);
            tabPage_add.Size = new Size(2032, 1077);
            tabPage_add.TabIndex = 1;
            tabPage_add.Text = "Add";
            tabPage_add.UseVisualStyleBackColor = true;
            // 
            // tabPage_add_btn_save
            // 
            tabPage_add_btn_save.Location = new Point(61, 47);
            tabPage_add_btn_save.Name = "tabPage_add_btn_save";
            tabPage_add_btn_save.Size = new Size(150, 35);
            tabPage_add_btn_save.TabIndex = 2;
            tabPage_add_btn_save.Text = "Save";
            tabPage_add_btn_save.UseVisualStyleBackColor = true;
            tabPage_add_btn_save.Click += TabPage_add_btn_save_Click;
            // 
            // tabPage_add_vtb_name
            // 
            tabPage_add_vtb_name.IsRequired = true;
            tabPage_add_vtb_name.Location = new Point(93, 18);
            tabPage_add_vtb_name.Name = "tabPage_add_vtb_name";
            tabPage_add_vtb_name.Size = new Size(200, 23);
            tabPage_add_vtb_name.TabIndex = 1;
            // 
            // tabPage_add_lbl_name
            // 
            tabPage_add_lbl_name.AutoSize = true;
            tabPage_add_lbl_name.Location = new Point(23, 21);
            tabPage_add_lbl_name.Name = "tabPage_add_lbl_name";
            tabPage_add_lbl_name.Size = new Size(42, 15);
            tabPage_add_lbl_name.TabIndex = 0;
            tabPage_add_lbl_name.Text = "Name:";
            // 
            // tabPage_edit
            // 
            tabPage_edit.Controls.Add(button1);
            tabPage_edit.Location = new Point(4, 24);
            tabPage_edit.Name = "tabPage_edit";
            tabPage_edit.Size = new Size(2032, 1077);
            tabPage_edit.TabIndex = 2;
            tabPage_edit.Text = "Edit";
            tabPage_edit.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(503, 197);
            button1.Name = "button1";
            button1.Size = new Size(150, 35);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // tabPage_delete
            // 
            tabPage_delete.Controls.Add(tabPage_delete_tb_id);
            tabPage_delete.Controls.Add(tabPage_delete_btn_no);
            tabPage_delete.Controls.Add(tabPage_delete_btn_yes);
            tabPage_delete.Controls.Add(tabPage_delete_lbl_message);
            tabPage_delete.Location = new Point(4, 24);
            tabPage_delete.Name = "tabPage_delete";
            tabPage_delete.Size = new Size(2032, 1077);
            tabPage_delete.TabIndex = 3;
            tabPage_delete.Text = "Delete";
            tabPage_delete.UseVisualStyleBackColor = true;
            // 
            // tabPage_delete_tb_id
            // 
            tabPage_delete_tb_id.Location = new Point(8, 77);
            tabPage_delete_tb_id.Name = "tabPage_delete_tb_id";
            tabPage_delete_tb_id.Size = new Size(150, 21);
            tabPage_delete_tb_id.TabIndex = 3;
            tabPage_delete_tb_id.Visible = false;
            // 
            // tabPage_delete_btn_no
            // 
            tabPage_delete_btn_no.Location = new Point(164, 37);
            tabPage_delete_btn_no.Name = "tabPage_delete_btn_no";
            tabPage_delete_btn_no.Size = new Size(150, 35);
            tabPage_delete_btn_no.TabIndex = 2;
            tabPage_delete_btn_no.Text = "No";
            tabPage_delete_btn_no.UseVisualStyleBackColor = true;
            tabPage_delete_btn_no.Click += TabPage_delete_btn_no_Click;
            // 
            // tabPage_delete_btn_yes
            // 
            tabPage_delete_btn_yes.Location = new Point(8, 37);
            tabPage_delete_btn_yes.Name = "tabPage_delete_btn_yes";
            tabPage_delete_btn_yes.Size = new Size(150, 35);
            tabPage_delete_btn_yes.TabIndex = 1;
            tabPage_delete_btn_yes.Text = "Yes";
            tabPage_delete_btn_yes.UseVisualStyleBackColor = true;
            tabPage_delete_btn_yes.Click += TabPage_delete_btn_yes_Click;
            // 
            // tabPage_delete_lbl_message
            // 
            tabPage_delete_lbl_message.AutoSize = true;
            tabPage_delete_lbl_message.Location = new Point(8, 14);
            tabPage_delete_lbl_message.Name = "tabPage_delete_lbl_message";
            tabPage_delete_lbl_message.Size = new Size(266, 15);
            tabPage_delete_lbl_message.TabIndex = 0;
            tabPage_delete_lbl_message.Text = "Are you sure you want to delete \"{0}\"";
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
            tabPage_edit.ResumeLayout(false);
            tabPage_delete.ResumeLayout(false);
            tabPage_delete.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_add;
        private System.Windows.Forms.TabPage tabPage_List;
        private ValidatedTextBox tabPage_add_vtb_name;
        private System.Windows.Forms.Label tabPage_add_lbl_name;
        private System.Windows.Forms.Button tabPage_add_btn_save;
        private System.Windows.Forms.TabPage tabPage_edit;
        private System.Windows.Forms.TabPage tabPage_delete;
        private System.Windows.Forms.Label tabPage_delete_lbl_message;
        private System.Windows.Forms.Button tabPage_delete_btn_no;
        private System.Windows.Forms.Button tabPage_delete_btn_yes;
        private System.Windows.Forms.TextBox tabPage_delete_tb_id;
        private System.Windows.Forms.Button button1;
    }
}
