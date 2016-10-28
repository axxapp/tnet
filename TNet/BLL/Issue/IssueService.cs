using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using TCom.EF;
using TNet.Models;

namespace TNet.BLL
{
    public class IssueService
    {
        public static List<Issue> GetALL()
        {
            TN db = new TN();
            return db.Issues.ToList();
        }

        public static Issue Get(string idissue)
        {
            TN db = new TN();
            List<Issue> issues = db.Issues.Where(en => en.issue1 == idissue).ToList();
            return (issues != null && issues.Count > 0) ? issues.First() : null;
        }

        public static IssueViewModel GetViewModel(string idissue)
        {
            Issue issue= Get(idissue);
            if (issue==null) {
                return null;
            }
            List<Issue> issues = new List<Issue>();
            issues.Add(issue);
            List<IssueViewModel> viewModels= ConvertToViewModel(issues);
            if (viewModels==null|| viewModels.Count==0) {
                return null;
            }
            return viewModels.First();
        }
        


        public static List<Issue> GetIssuesByFilter(DateTime? startOrDate, DateTime? endOrDate, string idissue = "", string userNo = "")
        {
            TN db = new TN();
            return db.Issues.Where(en =>
            (
                (
                   (!string.IsNullOrEmpty(idissue) && idissue == en.issue1))
                    ||
                    (
                        string.IsNullOrEmpty(idissue)
                        && (startOrDate.Value == null || SqlFunctions.DateDiff("dd", startOrDate.Value, en.cretime) >= 0)
                        && (endOrDate.Value == null || SqlFunctions.DateDiff("dd", endOrDate.Value, en.cretime) <= 0)
                        && (string.IsNullOrEmpty(userNo)|| userNo == en.iduser)
                    )
                )
            ).ToList();
        }

        public static List<IssueViewModel> GetIssuesViewModelByFilter(DateTime? startOrDate, DateTime? endOrDate, string issue = "", string userNo = "")
        {
            List<Issue> issues = GetIssuesByFilter(startOrDate, endOrDate, issue, userNo);
            return ConvertToViewModel(issues);
        }


        private static List<IssueViewModel> ConvertToViewModel(List<Issue> entities)
        {
            List<IssueViewModel> viewModels = new List<IssueViewModel>();
            if (entities != null && entities.Count > 0)
            {
                TN tn = new TN();
                List<TCom.EF.User> users = tn.Users.ToList();
                viewModels = entities.Select(en => {
                    IssueViewModel viewModel = new IssueViewModel();
                    viewModel.CopyFromBase(en);
                    TCom.EF.User user = users.Where(model => model.iduser.ToString() == viewModel.iduser).First();
                    viewModel.user = user;
                    return viewModel;
                }).ToList();
            }
            return viewModels;
        }

        public static Issue Edit(Issue issue)
        {
            TN db = new TN();
            Issue oldIssue = db.Issues.Where(en => en.issue1 == issue.issue1).FirstOrDefault();

            oldIssue.issue1 = issue.issue1;
            oldIssue.iduser = issue.iduser;
            oldIssue.context = issue.context;
            //oldIssue.cretime = issue.cretime;
            //oldIssue.booktime = issue.booktime;
            oldIssue.lng = issue.lng;
            oldIssue.lat = issue.lat;
            oldIssue.address = issue.address;
            oldIssue.phone = issue.phone;
            oldIssue.notes = issue.notes;
            oldIssue.tasktype = issue.tasktype;
            oldIssue.idtask = issue.idtask;
            oldIssue.inuse = issue.inuse;
            db.SaveChanges();
            return oldIssue;
        }

        public static Issue Add(Issue issue)
        {
            TN db = new TN();
            db.Issues.Add(issue);
            db.SaveChanges();
            return issue;
        }
    }
}