using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using HR.Core;

namespace HR.UI.Forms.Settings
{
    public partial class BackupRestoreForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly BackupManager _backupManager;
        
        public BackupRestoreForm()
        {
            InitializeComponent();
            _backupManager = new BackupManager();
        }
        
        private void BackupRestoreForm_Load(object sender, EventArgs e)
        {
            try
            {
                // تحديث قائمة النسخ الاحتياطية
                RefreshBackupList();
                
                // التحقق من الصلاحيات
                CheckPermissions();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل النموذج: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// التحقق من صلاحيات المستخدم
        /// </summary>
        private void CheckPermissions()
        {
            // التحقق من صلاحية إنشاء نسخة احتياطية
            barButtonItemCreateBackup.Enabled = SessionManager.HasPermission("Settings.CreateBackup");
            
            // التحقق من صلاحية استعادة نسخة احتياطية
            barButtonItemRestore.Enabled = SessionManager.HasPermission("Settings.RestoreBackup");
            
            // التحقق من صلاحية حذف نسخة احتياطية
            barButtonItemDelete.Enabled = SessionManager.HasPermission("Settings.DeleteBackup");
            
            // التحقق من صلاحية جدولة النسخ الاحتياطي
            barButtonItemSchedule.Enabled = SessionManager.HasPermission("Settings.ScheduleBackup");
        }
        
        /// <summary>
        /// تحديث قائمة النسخ الاحتياطية
        /// </summary>
        private void RefreshBackupList()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                // الحصول على قائمة النسخ الاحتياطية
                List<BackupInfo> backupList = _backupManager.GetBackupList();
                
                // عرض القائمة
                backupInfoBindingSource.DataSource = backupList;
                gridViewBackups.RefreshData();
                
                // تحديث حالة الأزرار
                UpdateButtonsState();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحديث قائمة النسخ الاحتياطية: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// تحديث حالة الأزرار
        /// </summary>
        private void UpdateButtonsState()
        {
            // الحصول على النسخة المحددة
            BackupInfo selectedBackup = GetSelectedBackup();
            
            // تحديث حالة زر الاستعادة
            barButtonItemRestore.Enabled = selectedBackup != null && 
                SessionManager.HasPermission("Settings.RestoreBackup");
            
            // تحديث حالة زر الحذف
            barButtonItemDelete.Enabled = selectedBackup != null && 
                SessionManager.HasPermission("Settings.DeleteBackup");
        }
        
        /// <summary>
        /// الحصول على النسخة المحددة
        /// </summary>
        /// <returns>معلومات النسخة المحددة</returns>
        private BackupInfo GetSelectedBackup()
        {
            if (gridViewBackups.GetSelectedRows().Length > 0)
            {
                int rowHandle = gridViewBackups.GetSelectedRows()[0];
                if (rowHandle >= 0)
                {
                    return gridViewBackups.GetRow(rowHandle) as BackupInfo;
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// حدث النقر على زر التحديث
        /// </summary>
        private void barButtonItemRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                RefreshBackupList();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحديث البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر إنشاء نسخة احتياطية
        /// </summary>
        private async void barButtonItemCreateBackup_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // طلب وصف النسخة الاحتياطية
                XtraInputBoxArgs args = new XtraInputBoxArgs();
                args.Caption = "إنشاء نسخة احتياطية";
                args.Prompt = "الرجاء إدخال وصف للنسخة الاحتياطية:";
                args.DefaultButtonIndex = 0;
                
                string description = XtraInputBox.Show(args) as string;
                
                if (string.IsNullOrWhiteSpace(description))
                {
                    description = "نسخة احتياطية للنظام";
                }
                
                this.Cursor = Cursors.WaitCursor;
                
                // عرض شريط التقدم
                progressBarControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                progressBarControl.Properties.Step = 1;
                progressBarControl.Properties.PercentView = true;
                progressBarControl.EditValue = 0;
                timerProgress.Start();
                
                // إنشاء النسخة الاحتياطية بشكل غير متزامن
                string backupPath = await _backupManager.CreateBackupAsync(description);
                
                // إيقاف شريط التقدم
                timerProgress.Stop();
                progressBarControl.EditValue = 100;
                progressBarControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                
                if (!string.IsNullOrEmpty(backupPath))
                {
                    XtraMessageBox.Show("تم إنشاء النسخة الاحتياطية بنجاح.", "نجاح", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // تحديث القائمة
                    RefreshBackupList();
                }
                else
                {
                    XtraMessageBox.Show("فشل إنشاء النسخة الاحتياطية.", "خطأ", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء إنشاء النسخة الاحتياطية: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// حدث النقر على زر استعادة نسخة احتياطية
        /// </summary>
        private async void barButtonItemRestore_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // الحصول على النسخة المحددة
                BackupInfo selectedBackup = GetSelectedBackup();
                if (selectedBackup == null)
                {
                    XtraMessageBox.Show("الرجاء تحديد نسخة احتياطية أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // تأكيد الاستعادة
                if (XtraMessageBox.Show($"هل أنت متأكد من استعادة النسخة الاحتياطية '{selectedBackup.FileName}'؟\n\nتنبيه: سيتم استبدال جميع البيانات الحالية بالبيانات من النسخة الاحتياطية.", 
                    "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                
                this.Cursor = Cursors.WaitCursor;
                
                // عرض شريط التقدم
                progressBarControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                progressBarControl.Properties.Step = 1;
                progressBarControl.Properties.PercentView = true;
                progressBarControl.EditValue = 0;
                timerProgress.Start();
                
                // استعادة النسخة الاحتياطية بشكل غير متزامن
                bool success = await _backupManager.RestoreBackupAsync(selectedBackup.BackupPath);
                
                // إيقاف شريط التقدم
                timerProgress.Stop();
                progressBarControl.EditValue = 100;
                progressBarControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                
                if (success)
                {
                    XtraMessageBox.Show("تمت استعادة النسخة الاحتياطية بنجاح. سيتم إعادة تشغيل النظام.", "نجاح", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // إعادة تشغيل النظام
                    Application.Restart();
                }
                else
                {
                    XtraMessageBox.Show("فشل استعادة النسخة الاحتياطية.", "خطأ", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء استعادة النسخة الاحتياطية: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// حدث النقر على زر حذف نسخة احتياطية
        /// </summary>
        private void barButtonItemDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // الحصول على النسخة المحددة
                BackupInfo selectedBackup = GetSelectedBackup();
                if (selectedBackup == null)
                {
                    XtraMessageBox.Show("الرجاء تحديد نسخة احتياطية أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // تأكيد الحذف
                if (XtraMessageBox.Show($"هل أنت متأكد من حذف النسخة الاحتياطية '{selectedBackup.FileName}'؟", 
                    "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                
                this.Cursor = Cursors.WaitCursor;
                
                // حذف النسخة الاحتياطية
                bool success = _backupManager.DeleteBackup(selectedBackup.BackupPath);
                
                if (success)
                {
                    XtraMessageBox.Show("تم حذف النسخة الاحتياطية بنجاح.", "نجاح", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // تحديث القائمة
                    RefreshBackupList();
                }
                else
                {
                    XtraMessageBox.Show("فشل حذف النسخة الاحتياطية.", "خطأ", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء حذف النسخة الاحتياطية: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// حدث النقر على زر جدولة النسخ الاحتياطي
        /// </summary>
        private void barButtonItemSchedule_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // فتح نموذج جدولة النسخ الاحتياطي
                BackupScheduleForm scheduleForm = new BackupScheduleForm();
                scheduleForm.ShowDialog();
                
                // تحديث القائمة بعد إغلاق النموذج
                RefreshBackupList();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء فتح نموذج جدولة النسخ الاحتياطي: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على صف في الجدول
        /// </summary>
        private void gridViewBackups_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                UpdateButtonsState();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث انتهاء مؤقت التقدم
        /// </summary>
        private void timerProgress_Tick(object sender, EventArgs e)
        {
            try
            {
                int value = (int)progressBarControl.EditValue;
                
                if (value < 90)
                {
                    value += new Random().Next(1, 5);
                    progressBarControl.EditValue = value;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ في مؤقت التقدم");
            }
        }
    }
    
    /// <summary>
    /// نموذج جدولة النسخ الاحتياطي
    /// </summary>
    public class BackupScheduleForm : XtraForm
    {
        private readonly BackupManager _backupManager;
        private LabelControl labelControl1;
        private ComboBoxEdit comboBoxEditInterval;
        private LabelControl labelControl2;
        private MemoEdit memoEditDescription;
        private SimpleButton simpleButtonOK;
        private SimpleButton simpleButtonCancel;
        
        public BackupScheduleForm()
        {
            InitializeComponent();
            _backupManager = new BackupManager();
            
            // تعبئة قائمة الفترات الزمنية
            comboBoxEditInterval.Properties.Items.AddRange(new string[] {
                "6 ساعات",
                "12 ساعة",
                "24 ساعة (يومي)",
                "168 ساعة (أسبوعي)"
            });
            
            comboBoxEditInterval.SelectedIndex = 2;
        }
        
        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEditInterval = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.memoEditDescription = new DevExpress.XtraEditors.MemoEdit();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditInterval.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(330, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(70, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "الفترة الزمنية:";
            // 
            // comboBoxEditInterval
            // 
            this.comboBoxEditInterval.Location = new System.Drawing.Point(12, 12);
            this.comboBoxEditInterval.Name = "comboBoxEditInterval";
            this.comboBoxEditInterval.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditInterval.Size = new System.Drawing.Size(312, 20);
            this.comboBoxEditInterval.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(330, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(61, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "وصف النسخة:";
            // 
            // memoEditDescription
            // 
            this.memoEditDescription.Location = new System.Drawing.Point(12, 38);
            this.memoEditDescription.Name = "memoEditDescription";
            this.memoEditDescription.Size = new System.Drawing.Size(312, 60);
            this.memoEditDescription.TabIndex = 3;
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(12, 104);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK.TabIndex = 4;
            this.simpleButtonOK.Text = "موافق";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButtonCancel.Location = new System.Drawing.Point(93, 104);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCancel.TabIndex = 5;
            this.simpleButtonCancel.Text = "إلغاء";
            // 
            // BackupScheduleForm
            // 
            this.AcceptButton = this.simpleButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleButtonCancel;
            this.ClientSize = new System.Drawing.Size(413, 138);
            this.Controls.Add(this.simpleButtonCancel);
            this.Controls.Add(this.simpleButtonOK);
            this.Controls.Add(this.memoEditDescription);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.comboBoxEditInterval);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BackupScheduleForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "جدولة النسخ الاحتياطي التلقائي";
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditInterval.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من إدخال الفترة الزمنية
                if (comboBoxEditInterval.SelectedIndex < 0)
                {
                    XtraMessageBox.Show("يرجى اختيار الفترة الزمنية.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBoxEditInterval.Focus();
                    return;
                }
                
                // الحصول على الفترة الزمنية بالساعات
                int interval = 24; // افتراضي: يومي
                
                switch (comboBoxEditInterval.SelectedIndex)
                {
                    case 0: interval = 6; break;
                    case 1: interval = 12; break;
                    case 2: interval = 24; break;
                    case 3: interval = 168; break;
                }
                
                // الحصول على وصف النسخة
                string description = memoEditDescription.Text;
                if (string.IsNullOrWhiteSpace(description))
                {
                    description = "نسخة احتياطية تلقائية";
                }
                
                // جدولة النسخ الاحتياطي
                bool success = _backupManager.ScheduleAutomaticBackup(interval, description);
                
                if (success)
                {
                    XtraMessageBox.Show($"تمت جدولة النسخ الاحتياطي التلقائي كل {interval} ساعة بنجاح.", "نجاح", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show("فشل جدولة النسخ الاحتياطي التلقائي.", "خطأ", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
    }
}