using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationPortal.ViewModels
{
    public class HistoryViewModel
    {
        public List<HistoryItem> history = new List<HistoryItem>(); 

        public class HistoryItem
        {
            public HistoryItem(HistoryActionType type)
            {
                Type = type;
            }

            public enum HistoryActionType {
                ACCOUNT_CREATED,
                LESSON_ADDED,
                COURSE_ADDED
            }

            public HistoryActionType Type { get; protected set; }
            public DateTime dateTime;
            public object Item;
        }

        public static HistoryViewModel CreateHistory(Models.EducationPortalContainer db, DateTime from, DateTime to, Models.Account account = null)
        {
            var history = new List<HistoryItem>();

            var accountUpdates = new List<Models.Account>();
            if(account == null)
            {
                accountUpdates.AddRange(db.Accounts.Where(m => m.CreationTime >= from && m.CreationTime <= to));
            } else
            {
                accountUpdates.Add(account);
            }

            var lessonUpdates = new List<Models.Lesson>();
            lessonUpdates.AddRange(db.Lessons.Where(m => m.CreationTime >= from && m.CreationTime <= to));

            var coursesUpdates = new List<Models.Course>();
            coursesUpdates.AddRange(db.Courses.Where(m => m.CreationTime >= from && m.CreationTime <= to));

            history.AddRange(accountUpdates.Select(m
                => new HistoryItem(HistoryItem.HistoryActionType.ACCOUNT_CREATED)
                {
                    dateTime = m.CreationTime,
                    Item = m
                }));

            history.AddRange(lessonUpdates.Select(m
                => new HistoryItem(HistoryItem.HistoryActionType.LESSON_ADDED)
                {
                    dateTime = m.CreationTime,
                    Item = m
                }));

            history.AddRange(coursesUpdates.Select(m
                => new HistoryItem(HistoryItem.HistoryActionType.COURSE_ADDED)
                {
                    dateTime = m.CreationTime,
                    Item = m
                }));

            history = history.OrderByDescending(m => m.dateTime).ToList();

            return new HistoryViewModel() { history = history };
        }
        
    }
}