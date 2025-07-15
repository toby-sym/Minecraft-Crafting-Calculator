namespace MinecraftCraftingCalculator
{
    partial class RecipeSelectionForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblItemName = new Label();
            btnOK = new Button();
            btnCancel = new Button();
            checkedListBoxRecipes = new CheckedListBox();
            SuspendLayout();
            // 
            // lblItemName
            // 
            lblItemName.AutoSize = true;
            lblItemName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblItemName.Location = new Point(12, 9);
            lblItemName.Name = "lblItemName";
            lblItemName.Size = new Size(101, 19);
            lblItemName.TabIndex = 0;
            lblItemName.Text = "Select recipes";
            // 
            // btnOK
            // 
            btnOK.Location = new Point(310, 300);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 30);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(391, 300);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 30);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // checkedListBoxRecipes
            // 
            checkedListBoxRecipes.FormattingEnabled = true;
            checkedListBoxRecipes.Location = new Point(12, 31);
            checkedListBoxRecipes.Name = "checkedListBoxRecipes";
            checkedListBoxRecipes.Size = new Size(460, 256);
            checkedListBoxRecipes.TabIndex = 4;
            // 
            // RecipeSelectionForm
            // 
            ClientSize = new Size(484, 341);
            Controls.Add(checkedListBoxRecipes);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(lblItemName);
            Name = "RecipeSelectionForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Recipes";
            ResumeLayout(false);
            PerformLayout();
        }

        private CheckedListBox checkedListBoxRecipes;
    }
}
