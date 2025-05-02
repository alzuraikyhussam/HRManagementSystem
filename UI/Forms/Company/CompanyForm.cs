using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.Models;

namespace HR.UI.Forms.Company
{
    /// <summary>
    /// نموذج إدارة بيانات الشركة
    /// </summary>
    public partial class CompanyForm : XtraForm
    {
        // كائن الشركة الحالي
        private Models.Company _company;
        
        // مسار الصورة المحددة
        private string _selectedLogoPath;
        
        // تحديد ما إذا كان هناك تغييرات
        private bool _hasChanges = false;
        
        /// <summary>
        /// تهيئة نموذج إدارة الشركة
        /// </summary>
        public CompanyForm()
        {
            InitializeComponent();
            
            // ضبط خصائص النموذج
            this.Text = "إدارة بيانات الشركة";
            
            // تهيئة عناصر التحكم
            InitializeControls();
            
            // تسجيل الأحداث
            this.Load += CompanyForm_Load;
        }

        /// <summary>
        /// تهيئة عناصر التحكم
        /// </summary>
        private void InitializeControls()
        {
            try
            {
                // تسجيل أحداث التغيير للحقول
                RegisterChangeEvents();
                
                // تسجيل أحداث الأزرار
                buttonSave.Click += ButtonSave_Click;
                buttonCancel.Click += ButtonCancel_Click;
                buttonSelectLogo.Click += ButtonSelectLogo_Click;
                buttonClearLogo.Click += ButtonClearLogo_Click;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تهيئة عناصر التحكم في نموذج إدارة بيانات الشركة");
            }
        }

        /// <summary>
        /// تسجيل أحداث التغيير للحقول
        /// </summary>
        private void RegisterChangeEvents()
        {
            textEditName.EditValueChanged += Control_ValueChanged;
            textEditLegalName.EditValueChanged += Control_ValueChanged;
            textEditCommercialRecord.EditValueChanged += Control_ValueChanged;
            textEditTaxNumber.EditValueChanged += Control_ValueChanged;
            textEditPhone.EditValueChanged += Control_ValueChanged;
            textEditEmail.EditValueChanged += Control_ValueChanged;
            textEditWebsite.EditValueChanged += Control_ValueChanged;
            memoEditAddress.EditValueChanged += Control_ValueChanged;
            memoEditNotes.EditValueChanged += Control_ValueChanged;
            dateEditEstablishment.EditValueChanged += Control_ValueChanged;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void CompanyForm_Load(object sender, EventArgs e)
        {
            try
            {
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // جلب بيانات الشركة
                LoadCompanyData();
                
                // تعطيل الأزرار
                UpdateButtonState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحميل بيانات الشركة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحميل بيانات الشركة: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// تحميل بيانات الشركة
        /// </summary>
        private void LoadCompanyData()
        {
            try
            {
                // جلب بيانات الشركة من قاعدة البيانات
                using (var unitOfWork = new UnitOfWork())
                {
                    _company = unitOfWork.CompanyRepository.GetCompany();
                    
                    // إذا لم يتم العثور على بيانات الشركة، إنشاء كائن جديد
                    if (_company == null)
                    {
                        _company = new Models.Company
                        {
                            ID = 1, // معرف افتراضي
                            Name = "",
                            LegalName = "",
                            CommercialRecord = "",
                            TaxNumber = "",
                            Address = "",
                            Phone = "",
                            Email = "",
                            Website = "",
                            Logo = null,
                            EstablishmentDate = null,
                            Notes = "",
                            CreatedAt = DateTime.Now,
                            UpdatedAt = null
                        };
                    }
                    
                    // عرض البيانات في النموذج
                    DisplayCompanyData();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل جلب بيانات الشركة");
                throw;
            }
        }

        /// <summary>
        /// عرض بيانات الشركة في النموذج
        /// </summary>
        private void DisplayCompanyData()
        {
            if (_company == null)
                return;
            
            // عرض البيانات في الحقول
            textEditName.Text = _company.Name;
            textEditLegalName.Text = _company.LegalName;
            textEditCommercialRecord.Text = _company.CommercialRecord;
            textEditTaxNumber.Text = _company.TaxNumber;
            textEditPhone.Text = _company.Phone;
            textEditEmail.Text = _company.Email;
            textEditWebsite.Text = _company.Website;
            memoEditAddress.Text = _company.Address;
            memoEditNotes.Text = _company.Notes;
            
            // تعيين تاريخ التأسيس إذا كان متوفرًا
            if (_company.EstablishmentDate.HasValue)
            {
                dateEditEstablishment.DateTime = _company.EstablishmentDate.Value;
            }
            else
            {
                dateEditEstablishment.EditValue = null;
            }
            
            // عرض الشعار
            if (_company.Logo != null && _company.Logo.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(_company.Logo))
                {
                    pictureEditLogo.Image = Image.FromStream(ms);
                }
            }
            else
            {
                pictureEditLogo.Image = null;
            }
            
            // إعادة تعيين حالة التغييرات
            _hasChanges = false;
            
            // تحديث حالة الأزرار
            UpdateButtonState();
        }

        /// <summary>
        /// حدث تغيير قيمة أي عنصر تحكم
        /// </summary>
        private void Control_ValueChanged(object sender, EventArgs e)
        {
            _hasChanges = true;
            UpdateButtonState();
        }

        /// <summary>
        /// تحديث حالة الأزرار
        /// </summary>
        private void UpdateButtonState()
        {
            buttonSave.Enabled = _hasChanges;
            buttonClearLogo.Enabled = pictureEditLogo.Image != null;
        }

        /// <summary>
        /// حدث النقر على زر الحفظ
        /// </summary>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من صحة البيانات
                if (!ValidateData())
                    return;
                
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // تحديث كائن الشركة
                UpdateCompanyObject();
                
                // حفظ البيانات
                SaveCompanyData();
                
                // عرض رسالة النجاح
                XtraMessageBox.Show(
                    "تم حفظ بيانات الشركة بنجاح",
                    "تم الحفظ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                
                // إعادة تعيين حالة التغييرات
                _hasChanges = false;
                UpdateButtonState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل حفظ بيانات الشركة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء حفظ بيانات الشركة: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// التحقق من صحة البيانات
        /// </summary>
        private bool ValidateData()
        {
            // التحقق من اسم الشركة
            if (string.IsNullOrWhiteSpace(textEditName.Text))
            {
                XtraMessageBox.Show(
                    "الرجاء إدخال اسم الشركة",
                    "خطأ في البيانات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                textEditName.Focus();
                return false;
            }
            
            // التحقق من صحة البريد الإلكتروني إذا كان مدخلاً
            if (!string.IsNullOrWhiteSpace(textEditEmail.Text))
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(textEditEmail.Text);
                }
                catch
                {
                    XtraMessageBox.Show(
                        "الرجاء إدخال عنوان بريد إلكتروني صحيح",
                        "خطأ في البيانات",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    textEditEmail.Focus();
                    return false;
                }
            }
            
            // التحقق من صحة الموقع الإلكتروني إذا كان مدخلاً
            if (!string.IsNullOrWhiteSpace(textEditWebsite.Text))
            {
                if (!Uri.TryCreate(textEditWebsite.Text, UriKind.Absolute, out Uri uriResult) ||
                    (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
                {
                    XtraMessageBox.Show(
                        "الرجاء إدخال عنوان موقع إلكتروني صحيح (يبدأ بـ http:// أو https://)",
                        "خطأ في البيانات",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    textEditWebsite.Focus();
                    return false;
                }
            }
            
            return true;
        }

        /// <summary>
        /// تحديث كائن الشركة
        /// </summary>
        private void UpdateCompanyObject()
        {
            if (_company == null)
                _company = new Models.Company();
            
            // تحديث البيانات من الحقول
            _company.Name = textEditName.Text;
            _company.LegalName = textEditLegalName.Text;
            _company.CommercialRecord = textEditCommercialRecord.Text;
            _company.TaxNumber = textEditTaxNumber.Text;
            _company.Phone = textEditPhone.Text;
            _company.Email = textEditEmail.Text;
            _company.Website = textEditWebsite.Text;
            _company.Address = memoEditAddress.Text;
            _company.Notes = memoEditNotes.Text;
            
            // تحديث تاريخ التأسيس
            if (dateEditEstablishment.EditValue != null)
            {
                _company.EstablishmentDate = dateEditEstablishment.DateTime.Date;
            }
            else
            {
                _company.EstablishmentDate = null;
            }
            
            // تحديث وقت التعديل
            _company.UpdatedAt = DateTime.Now;
            
            // تحديث الشعار إذا تم تغييره
            if (!string.IsNullOrEmpty(_selectedLogoPath))
            {
                _company.Logo = File.ReadAllBytes(_selectedLogoPath);
                _selectedLogoPath = null;
            }
            else if (pictureEditLogo.Image == null && _company.Logo != null)
            {
                _company.Logo = null;
            }
        }

        /// <summary>
        /// حفظ بيانات الشركة
        /// </summary>
        private void SaveCompanyData()
        {
            try
            {
                // حفظ بيانات الشركة في قاعدة البيانات
                using (var unitOfWork = new UnitOfWork())
                {
                    if (_company.ID > 0)
                    {
                        // تحديث الشركة الموجودة
                        unitOfWork.CompanyRepository.Update(_company);
                    }
                    else
                    {
                        // إضافة شركة جديدة
                        unitOfWork.CompanyRepository.Add(_company);
                    }
                    
                    unitOfWork.Complete();
                    
                    // تحديث كائن الجلسة
                    SessionManager.SetCompanyData(_company);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل حفظ بيانات الشركة في قاعدة البيانات");
                throw;
            }
        }

        /// <summary>
        /// حدث النقر على زر الإلغاء
        /// </summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود تغييرات
                if (_hasChanges)
                {
                    DialogResult result = XtraMessageBox.Show(
                        "هل تريد تجاهل التغييرات؟",
                        "تأكيد",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    
                    if (result == DialogResult.No)
                        return;
                }
                
                // إعادة تحميل البيانات
                LoadCompanyData();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل إلغاء التغييرات");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء إلغاء التغييرات: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث النقر على زر اختيار الشعار
        /// </summary>
        private void ButtonSelectLogo_Click(object sender, EventArgs e)
        {
            try
            {
                // فتح مربع حوار اختيار الملف
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "ملفات الصور (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                    openFileDialog.Title = "اختر شعار الشركة";
                    
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // تخزين مسار الملف المحدد
                        _selectedLogoPath = openFileDialog.FileName;
                        
                        // عرض الصورة في عنصر التحكم
                        pictureEditLogo.Image = Image.FromFile(_selectedLogoPath);
                        
                        // تحديث حالة التغييرات
                        _hasChanges = true;
                        UpdateButtonState();
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل اختيار شعار الشركة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء اختيار شعار الشركة: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث النقر على زر مسح الشعار
        /// </summary>
        private void ButtonClearLogo_Click(object sender, EventArgs e)
        {
            try
            {
                // مسح الصورة
                pictureEditLogo.Image = null;
                _selectedLogoPath = null;
                
                // تحديث حالة التغييرات
                _hasChanges = true;
                UpdateButtonState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل مسح شعار الشركة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء مسح شعار الشركة: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}