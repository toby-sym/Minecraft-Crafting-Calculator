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
            lstResults = new ListBox();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            SuspendLayout();
            // 
            // txtItemName
            // 
            txtItemName.Enabled = false;
            txtItemName.Location = new Point(12, 12);
            txtItemName.Name = "txtItemName";
            txtItemName.Size = new Size(100, 23);
            txtItemName.TabIndex = 0;
            // 
            // numQuantity
            // 
            numQuantity.Enabled = false;
            numQuantity.Location = new Point(118, 12);
            numQuantity.Name = "numQuantity";
            numQuantity.Size = new Size(31, 23);
            numQuantity.TabIndex = 1;
            numQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnAddItem
            // 
            btnAddItem.Enabled = false;
            btnAddItem.Location = new Point(155, 12);
            btnAddItem.Name = "btnAddItem";
            btnAddItem.Size = new Size(75, 23);
            btnAddItem.TabIndex = 2;
            btnAddItem.Text = "Add";
            btnAddItem.UseVisualStyleBackColor = true;
            btnAddItem.Click += button1_Click;
            // 
            // lstCraftingList
            // 
            lstCraftingList.FormattingEnabled = true;
            lstCraftingList.ItemHeight = 15;
            lstCraftingList.Location = new Point(12, 50);
            lstCraftingList.Name = "lstCraftingList";
            lstCraftingList.Size = new Size(218, 214);
            lstCraftingList.TabIndex = 3;
            lstCraftingList.SelectedIndexChanged += lstCraftingList_SelectedIndexChanged;
            // 
            // btnCalculate
            // 
            btnCalculate.Enabled = false;
            btnCalculate.Location = new Point(12, 270);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(218, 23);
            btnCalculate.TabIndex = 4;
            btnCalculate.Text = "Calculate Raw Materials";
            btnCalculate.UseVisualStyleBackColor = true;
            // 
            // lstResults
            // 
            lstResults.FormattingEnabled = true;
            lstResults.ItemHeight = 15;
            lstResults.Location = new Point(502, 50);
            lstResults.Name = "lstResults";
            lstResults.Size = new Size(286, 229);
            lstResults.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lstResults);
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
        private ListBox lstResults;
    }
}
