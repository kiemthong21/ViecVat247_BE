using BusinessObject;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace viecvat247Service
{
    public interface INotificationService
    {
        public PaginatedList<Notification> GetNotifications(int cid, int pageIndex, int pageSize);

        public List<Notification> GetNotificationTop(int cid, int top);

        public Notification AddNotification(string content, string uid, string? jobId, string? aid);

        public void UpdateNotification(List<int> listNoti);
    }
}
