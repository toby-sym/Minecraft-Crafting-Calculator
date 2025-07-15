namespace MinecraftCraftingCalculator
{
    partial class RecipeSelectionItem
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.CheckBox chkSelect;
        private System.Windows.Forms.NumericUpDown numQuantity;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.chkSelect = new System.Windows.Forms.CheckBox();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // chkSelect
            // 
            this.chkSelect.AutoSize = true;
            this.chkSelect.Location = new System.Drawing.Point(3, 6);
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.Size = new System.Drawing.Size(88, 19);
            this.chkSelect.TabIndex = 0;
            this.chkSelect.Text = "Recipe text";
            this.chkSelect.UseVisualStyleBackColor = true;
            // 
            // numQuantity
            // 
            this.numQuantity.Location = new System.Drawing.Point(350, 4);
            this.numQuantity.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(60, 23);
            this.numQuantity.TabIndex = 1;
            this.numQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // RecipeSelectionItem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.chkSelect);
            this.Name = "RecipeSelectionItem";
            this.Size = new System.Drawing.Size(420, 30);
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
