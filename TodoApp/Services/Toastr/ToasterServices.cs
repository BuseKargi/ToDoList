using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoApp
{
    public static class ToasterServices
    {
        private static readonly List<(DateTime Date, string SessionId, Toastr Toastr)> _toastrs =
            new List<(DateTime Date, string SessionId, Toastr Toastr)>();

        private static string GetSessionId()
        {
            return HttpContext.Current.Session.SessionID;
        }

        public static void AddToTaskQueue(Toastr toastr)
        {
            _toastrs.Add((Date: DateTime.Now, SessionId: GetSessionId(), Toastr: toastr));
        }
        public static void AddToTaskQueue(string message, string title,ToastrType type)
        {
            AddToTaskQueue(new Toastr(message,title,type));
        }
        public static bool HasTaskQueue()
        {
            string sessionId = GetSessionId();
            return _toastrs.Any(x => x.SessionId ==sessionId);
        }
        public static void RemoveTaskQueue()
        {
            string sessionId = GetSessionId();
             _toastrs.RemoveAll(x => x.SessionId == sessionId);
        }
        public static void ClearAll()
        {
            _toastrs.Clear();
        }
        public static List<(DateTime Date, string sessionId, Toastr toastr)> ReadTaskQueue()
        {
            string sessionId = GetSessionId();
            return _toastrs.Where(x => x.SessionId == sessionId).OrderBy(x => x.Date).ToList();
        }
        public static List<(DateTime Date,string sessionId, Toastr toastr)> ReadAndRemoveTaskQueue()
        {
            var list = ReadTaskQueue();
            RemoveTaskQueue();
            return list;
        }
    }
}