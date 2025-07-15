namespace MinecraftCraftingCalculator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            txtItemName = new TextBox();
            numQuantity = new NumericUpDown();
            btnAddItem = new Button();
            lstCraftingList = new ListBox();
            btnCalculate = new Button();
            lstRawMaterials = new ListBox();
            btnLoadRecipes = new Button();
            lblRecipeCount = new Label();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            SuspendLayout();
            // 
            // txtItemName
            // 
            txtItemName.Enabled = false;
            txtItemName.Location = new Point(12, 12);
            txtItemName.Name = "txtItemName";
            txtItemName.Size = new Size(234, 23);
            txtItemName.TabIndex = 0;
            // 
            // numQuantity
            // 
            numQuantity.Enabled = false;
            numQuantity.Location = new Point(252, 12);
            numQuantity.Name = "numQuantity";
            numQuantity.Size = new Size(31, 23);
            numQuantity.TabIndex = 1;
            numQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnAddItem
            // 
            btnAddItem.Enabled = false;
            btnAddItem.Location = new Point(289, 12);
            btnAddItem.Name = "btnAddItem";
            btnAddItem.Size = new Size(75, 23);
            btnAddItem.TabIndex = 2;
            btnAddItem.Text = "Add";
            btnAddItem.UseVisualStyleBackColor = true;
            // 
            // lstCraftingList
            // 
            lstCraftingList.FormattingEnabled = true;
            lstCraftingList.ItemHeight = 15;
            lstCraftingList.Location = new Point(12, 50);
            lstCraftingList.Name = "lstCraftingList";
            lstCraftingList.Size = new Size(352, 214);
            lstCraftingList.TabIndex = 3;
            // 
            // btnCalculate
            // 
            btnCalculate.Enabled = false;
            btnCalculate.Location = new Point(12, 270);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(352, 23);
            btnCalculate.TabIndex = 4;
            btnCalculate.Text = "Calculate Raw Materials";
            btnCalculate.UseVisualStyleBackColor = true;
            // 
            // lstRawMaterials
            // 
            lstRawMaterials.FormattingEnabled = true;
            lstRawMaterials.ItemHeight = 15;
            lstRawMaterials.Location = new Point(502, 50);
            lstRawMaterials.Name = "lstRawMaterials";
            lstRawMaterials.Size = new Size(286, 229);
            lstRawMaterials.TabIndex = 5;
            // 
            // btnLoadRecipes
            // 
            btnLoadRecipes.Location = new Point(663, 16);
            btnLoadRecipes.Name = "btnLoadRecipes";
            btnLoadRecipes.Size = new Size(125, 23);
            btnLoadRecipes.TabIndex = 6;
            btnLoadRecipes.Text = "Load Recipes JSON";
            btnLoadRecipes.UseVisualStyleBackColor = true;
            btnLoadRecipes.Click += btnLoadRecipes_Click;
            // 
            // lblRecipeCount
            // 
            lblRecipeCount.AutoSize = true;
            lblRecipeCount.Location = new Point(531, 20);
            lblRecipeCount.Name = "lblRecipeCount";
            lblRecipeCount.Size = new Size(114, 15);
            lblRecipeCount.TabIndex = 7;
            lblRecipeCount.Text = "Upload Recipe JSON";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblRecipeCount);
            Controls.Add(btnLoadRecipes);
            Controls.Add(lstRawMaterials);
            Controls.Add(btnCalculate);
            Controls.Add(lstCraftingList);
            Controls.Add(btnAddItem);
            Controls.Add(numQuantity);
            Controls.Add(txtItemName);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Minecraft Crafting Calculator";
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtItemName;
        private NumericUpDown numQuantity;
        private Button btnAddItem;
        private ListBox lstCraftingList;
        private Button btnCalculate;
        private ListBox lstRawMaterials;
        private Button btnLoadRecipes;
        private Label lblRecipeCount;
    }
}
