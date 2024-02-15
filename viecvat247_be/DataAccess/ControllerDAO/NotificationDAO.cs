using BusinessObject;
using BussinessObject.Models;
using BussinessObject.Viecvat247Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccess.ControllerDAO
{
    public class NotificationDAO
    {
        public static PaginatedList<Notification> GetNotifications( int cid ,int pageIndex, int pageSize)
        {
            var notification = new List<Notification>();
            int count = 0;
            IQueryable<Notification> query = null;

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Notifications.Where(x => x.CustomerId == cid).OrderByDescending(x => x.NotificationId);
                    count = query.Count();
                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    notification = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Notification>(notification, count, pageIndex, pageSize);
        }

        public static List<Notification> GetNotificationsByTop( int cid, int top)
        {
            try
            {
                List<Notification> list = new List<Notification>();
                using (var context = new Viecvat247DBcontext())
                {
                    list = context.Notifications.Where(x => x.CustomerId == cid).OrderByDescending(x => x.NotificationId).Take(top).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Notification AddNotification(Notification notification)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.Notifications.Add(notification);
                    context.SaveChanges();
                    return notification;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateNotification(List<int> noti)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    foreach(var item in noti)
                    {
                       var notification =  context.Notifications.SingleOrDefault(x => x.NotificationId == item);
                        if(notification == null)
                        {
                            return;
                        }
                        else
                        {
                            notification.Status = 1;
                            context.Entry<Notification>(notification).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        } 
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
