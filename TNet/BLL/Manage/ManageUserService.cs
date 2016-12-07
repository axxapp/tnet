using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TCom.EF;
using TNet.Models;

namespace TNet.BLL
{
    public class ManageUserService
    {
        public static List<ManageUser> GetALL() {
            TN tn = new TN();
            return tn.ManageUsers.ToList();
        }


        public static ManageUser GetManageUserByUserName(string userName) {
            return GetALL().Where(en => en.UserName == userName).FirstOrDefault();
        }

        public static List<ManageUser> GetALL(ManageUserType usertType) {
            TN tn = new TN();

            return tn.ManageUsers.Where(en => en.UserType == (int)usertType).ToList(); 
        }

        public static ManageUser Get(string manageUserId) {
            TN tn = new TN();
            return tn.ManageUsers.Find(manageUserId);
        }

        public static ManageUser Add(ManageUser manageUser)
        {
            TN db = new TN();
            db.ManageUsers.Add(manageUser);
            db.SaveChanges();
            return manageUser;
        }

        public static ManageUser Edit(ManageUser manageUser)
        {
            TN db = new TN();
            ManageUser oldManageUser = db.ManageUsers.Where(en => en.ManageUserId == manageUser.ManageUserId).FirstOrDefault();

            oldManageUser.ManageUserId = manageUser.ManageUserId;
            oldManageUser.UserName = manageUser.UserName;
            oldManageUser.phone = manageUser.phone;
            oldManageUser.idweixin = manageUser.idweixin;
            oldManageUser.recv_order = manageUser.recv_order;
            oldManageUser.recv_setup = manageUser.recv_setup;
            oldManageUser.recv_review = manageUser.recv_review;
            oldManageUser.send_setup = manageUser.send_setup;
            oldManageUser.notes = manageUser.notes;
            oldManageUser.inuse = manageUser.inuse;
            oldManageUser.UserType = manageUser.UserType;
            
            db.SaveChanges();
            return oldManageUser;
        }

        public static ManageUser PasswordEdit(ManageUser manageUser)
        {
            TN db = new TN();
            ManageUser oldManageUser = db.ManageUsers.Where(en => en.ManageUserId == manageUser.ManageUserId).FirstOrDefault();

            oldManageUser.ManageUserId = manageUser.ManageUserId;
            oldManageUser.MD5Salt = manageUser.MD5Salt;
            oldManageUser.Password = manageUser.Password;

            db.SaveChanges();
            return oldManageUser;
        }

        public static List<ManageUser> SearchByUserName(string userName) {
            List<ManageUser> entities = new List<ManageUser>();
            TN db = new TN();
            entities = db.ManageUsers.Where(en => en.UserName.Contains(userName)).ToList();

            return entities;
        }
    }
}