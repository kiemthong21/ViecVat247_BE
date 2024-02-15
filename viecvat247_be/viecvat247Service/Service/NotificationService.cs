using BusinessObject;
using BussinessObject.Models;
using DataAccess.ControllerDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace viecvat247Service.Service
{
    public class NotificationService : INotificationService
    {
        public Notification AddNotification(Notification notification)
        => NotificationDAO.AddNotification(notification);

        public Notification AddNotification(string content, string uid, string? jobId, string? aid)
        {
            Notification notification = new Notification();
            notification.Status = 0;
            notification.Timestamp = DateTime.Now;
            notification.CustomerId = Int32.Parse(uid);
            if(aid != null)
            {
                notification.Aid = Int32.Parse(aid);
            }
           
            if (!string.IsNullOrEmpty(jobId))
            {
                notification.JobId = Int32.Parse(jobId);
            }
            notification.Description = content;
            notification = NotificationDAO.AddNotification(notification);
            return notification;

        }

        public PaginatedList<Notification> GetNotifications(int cid, int pageIndex, int pageSize)
        => NotificationDAO.GetNotifications(cid, pageIndex, pageSize);

        public List<Notification> GetNotificationTop(int cid, int top)
        => NotificationDAO.GetNotificationsByTop(cid, top);

        public void UpdateNotification(List<int> listNoti)
        => NotificationDAO.UpdateNotification(listNoti);
    }
}
