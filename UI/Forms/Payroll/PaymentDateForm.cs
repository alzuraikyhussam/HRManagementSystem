using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HR.UI.Forms.Payroll
{
    /// <summary>
    /// نموذج إدخال تاريخ الدفع
    /// </summary>
    public class PaymentDateForm : XtraForm
    {
        private LabelControl labelControl1;
        private DateEdit dateEditPaymentDate;
        private SimpleButton simpleButtonOK;
        private SimpleButton simpleButtonCancel;
        
        public DateTime PaymentDate { get; private set; }
        
        public PaymentDateForm()
        {
            InitializeComponent();
            dateEditPaymentDate.DateTime = DateTime.Now.Date;
        }
        
        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateEditPaymentDate = new DevExpress.XtraEditors.DateEdit();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPaymentDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPaymentDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(235, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "تاريخ الدفع:";
            // 
            // dateEditPaymentDate
            // 
            this.dateEditPaymentDate.EditValue = null;
            this.dateEditPaymentDate.Location = new System.Drawing.Point(20, 22);
            this.dateEditPaymentDate.Name = "dateEditPaymentDate";
            this.dateEditPaymentDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPaymentDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPaymentDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateEditPaymentDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditPaymentDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateEditPaymentDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditPaymentDate.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dateEditPaymentDate.Size = new System.Drawing.Size(200, 20);
            this.dateEditPaymentDate.TabIndex = 1;
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(20, 60);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK.TabIndex = 2;
            this.simpleButtonOK.Text = "موافق";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButtonCancel.Location = new System.Drawing.Point(110, 60);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCancel.TabIndex = 3;
            this.simpleButtonCancel.Text = "إلغاء";
            // 
            // PaymentDateForm
            // 
            this.AcceptButton = this.simpleButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleButtonCancel;
            this.ClientSize = new System.Drawing.Size(294, 100);
            this.Controls.Add(this.simpleButtonCancel);
            this.Controls.Add(this.simpleButtonOK);
            this.Controls.Add(this.dateEditPaymentDate);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaymentDateForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "تاريخ الدفع";
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPaymentDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPaymentDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (dateEditPaymentDate.EditValue == null)
            {
                XtraMessageBox.Show("يرجى إدخال تاريخ الدفع.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            PaymentDate = dateEditPaymentDate.DateTime;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}