using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;

namespace HR.Core
{
    /// <summary>
    /// Manages notifications and alerts in the application
    /// </summary>
    public static class NotificationManager
    {
        // The alerter control used to display notifications
        private static AlertControl _alertControl;
        
        // Pending notifications queue
        private static Queue<NotificationInfo> _pendingNotifications = new Queue<NotificationInfo>();
        
        // Flag to indicate if a notification is currently being shown
        private static bool _isShowingNotification = false;
        
        /// <summary>
        /// Initializes the notification manager with an alert control
        /// </summary>
        /// <param name="alertControl">The alert control to use for notifications</param>
        public static void Initialize(AlertControl alertControl)
        {
            _alertControl = alertControl;
            
            if (_alertControl != null)
            {
                // Set up the alert control appearance
                _alertControl.FormLocation = DevExpress.XtraBars.Alerter.AlertFormLocation.BottomRight;
                _alertControl.FormShowingEffect = DevExpress.XtraBars.Alerter.AlertFormShowingEffect.SlideVertical;
                _alertControl.FormClosingEffect = DevExpress.XtraBars.Alerter.AlertFormClosingEffect.Fade;
                _alertControl.AutoFormDelay = 5000; // 5 seconds delay
                
                // Handle alert closed event to show next notification
                _alertControl.AlertClick += (sender, e) => 
                {
                    _isShowingNotification = false;
                    ShowNextNotification();
                };
                
                _alertControl.FormClosed += (sender, e) => 
                {
                    _isShowingNotification = false;
                    ShowNextNotification();
                };
            }
        }
        
        /// <summary>
        /// Shows an information notification
        /// </summary>
        /// <param name="title">The notification title</param>
        /// <param name="message">The notification message</param>
        /// <param name="clickHandler">Optional handler for when the notification is clicked</param>
        public static void ShowInfo(string title, string message, EventHandler<AlertClickEventArgs> clickHandler = null)
        {
            QueueNotification(title, message, Color.FromArgb(58, 142, 186), clickHandler);
        }
        
        /// <summary>
        /// Shows a success notification
        /// </summary>
        /// <param name="title">The notification title</param>
        /// <param name="message">The notification message</param>
        /// <param name="clickHandler">Optional handler for when the notification is clicked</param>
        public static void ShowSuccess(string title, string message, EventHandler<AlertClickEventArgs> clickHandler = null)
        {
            QueueNotification(title, message, Color.FromArgb(80, 160, 80), clickHandler);
        }
        
        /// <summary>
        /// Shows a warning notification
        /// </summary>
        /// <param name="title">The notification title</param>
        /// <param name="message">The notification message</param>
        /// <param name="clickHandler">Optional handler for when the notification is clicked</param>
        public static void ShowWarning(string title, string message, EventHandler<AlertClickEventArgs> clickHandler = null)
        {
            QueueNotification(title, message, Color.FromArgb(215, 155, 46), clickHandler);
        }
        
        /// <summary>
        /// Shows an error notification
        /// </summary>
        /// <param name="title">The notification title</param>
        /// <param name="message">The notification message</param>
        /// <param name="clickHandler">Optional handler for when the notification is clicked</param>
        public static void ShowError(string title, string message, EventHandler<AlertClickEventArgs> clickHandler = null)
        {
            QueueNotification(title, message, Color.FromArgb(196, 65, 65), clickHandler);
        }
        
        /// <summary>
        /// Shows a message box with the specified message and title
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="title">The title of the message box</param>
        /// <param name="buttons">The buttons to display</param>
        /// <param name="icon">The icon to display</param>
        /// <returns>The result of the message box</returns>
        public static DialogResult ShowMessageBox(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return XtraMessageBox.Show(message, title, buttons, (DevExpress.XtraEditors.Controls.MessageBoxIcon)icon);
        }
        
        /// <summary>
        /// Shows a confirmation message box
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="title">The title of the message box</param>
        /// <returns>True if the user confirms, false otherwise</returns>
        public static bool Confirm(string message, string title = "تأكيد")
        {
            return XtraMessageBox.Show(message, title, MessageBoxButtons.YesNo, 
                DevExpress.XtraEditors.Controls.MessageBoxIcon.Question) == DialogResult.Yes;
        }
        
        /// <summary>
        /// Queues a notification to be shown
        /// </summary>
        /// <param name="title">The notification title</param>
        /// <param name="message">The notification message</param>
        /// <param name="color">The background color of the notification</param>
        /// <param name="clickHandler">Handler for when the notification is clicked</param>
        private static void QueueNotification(string title, string message, Color color, EventHandler<AlertClickEventArgs> clickHandler)
        {
            if (_alertControl == null)
                return;
                
            _pendingNotifications.Enqueue(new NotificationInfo
            {
                Title = title,
                Message = message,
                Color = color,
                ClickHandler = clickHandler
            });
            
            // Show the notification if no notification is currently being shown
            if (!_isShowingNotification)
            {
                ShowNextNotification();
            }
        }
        
        /// <summary>
        /// Shows the next notification in the queue
        /// </summary>
        private static void ShowNextNotification()
        {
            if (_pendingNotifications.Count > 0 && _alertControl != null)
            {
                var notification = _pendingNotifications.Dequeue();
                
                // Set up the alert control
                _alertControl.ContentTemplate = GetNotificationTemplate(notification.Title, notification.Message, notification.Color);
                
                // Register the click handler if provided
                if (notification.ClickHandler != null)
                {
                    // Remove previous handlers
                    foreach (EventHandler<AlertClickEventArgs> handler in _alertControl.AlertClick.GetInvocationList())
                    {
                        if (handler != null && handler.Target != _alertControl)
                            _alertControl.AlertClick -= handler;
                    }
                    
                    // Add the new handler
                    _alertControl.AlertClick += notification.ClickHandler;
                }
                
                // Show the notification
                _isShowingNotification = true;
                _alertControl.Show(null);
            }
        }
        
        /// <summary>
        /// Creates a notification template
        /// </summary>
        /// <param name="title">The notification title</param>
        /// <param name="message">The notification message</param>
        /// <param name="color">The background color</param>
        /// <returns>The template to use for the notification</returns>
        private static AlertControl.AlertTemplateContentContainer GetNotificationTemplate(string title, string message, Color color)
        {
            var container = new AlertControl.AlertTemplateContentContainer();
            container.Template = new NotificationTemplate();
            container.Template.Title = title;
            container.Template.Message = message;
            container.Template.BackColor = color;
            return container;
        }
        
        /// <summary>
        /// Represents notification information
        /// </summary>
        private class NotificationInfo
        {
            public string Title { get; set; }
            public string Message { get; set; }
            public Color Color { get; set; }
            public EventHandler<AlertClickEventArgs> ClickHandler { get; set; }
        }
    }
    
    /// <summary>
    /// Custom template for notifications
    /// </summary>
    public class NotificationTemplate : DevExpress.XtraBars.Alerter.AlertControl.AlertTemplateBase
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public Color BackColor { get; set; }
        
        protected override void Draw(DevExpress.XtraBars.Alerter.AlertPaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            // Fill background
            using (SolidBrush brush = new SolidBrush(BackColor))
            {
                g.FillRectangle(brush, e.Bounds);
            }
            
            // Draw icon if provided
            if (Image != null)
            {
                g.DrawImage(Image, new Rectangle(5, 5, 32, 32));
            }
            
            // Draw title
            int leftMargin = Image != null ? 42 : 5;
            using (Font titleFont = new Font("Tahoma", 9, FontStyle.Bold))
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                g.DrawString(Title, titleFont, brush, new PointF(leftMargin, 5));
            }
            
            // Draw message
            using (Font messageFont = new Font("Tahoma", 8, FontStyle.Regular))
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                g.DrawString(Message, messageFont, brush, new RectangleF(leftMargin, 25, e.Bounds.Width - leftMargin - 5, e.Bounds.Height - 30));
            }
        }
    }
}
