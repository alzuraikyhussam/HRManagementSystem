using System;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;

namespace HR.UI.Forms.Attendance
{
    /// <summary>
    /// نموذج فرعي لانتظار العمليات
    /// </summary>
    public partial class WaitForm1 : WaitForm
    {
        /// <summary>
        /// إنشاء نموذج جديد
        /// </summary>
        /// <param name="caption">عنوان النموذج</param>
        public WaitForm1(string caption)
        {
            InitializeComponent();
            this.Caption = caption;
        }

        /// <summary>
        /// تعيين وصف العملية
        /// </summary>
        /// <param name="description">وصف العملية</param>
        public void SetDescription(string description)
        {
            if (lblStatus != null)
            {
                lblStatus.Text = description;
            }
        }

        #region OverRidden Methods
        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.Caption = caption;
        }

        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            SetDescription(description);
        }

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }
        #endregion
    }
}