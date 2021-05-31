﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataEngine;
using DataServices;
namespace NationAssists.Areas.Admin.Models
{
    public class mUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int BranchId { get; set; }
        public DateTime UserExpiryDate { get; set; }
        public bool IsActive { get; set; }

        public List<Users> UsersList { get; set; }

        public SelectList GetAllUsers()
        {
            CommonServices objCS = new CommonServices();
            IEnumerable<SelectListItem> lstDist = objCS.BindUsers().Data.OrderBy(s => s.Name).ToList().Select(m => new SelectListItem() { Text = m.Name, Value = m.UserId.ToString() }).OrderBy(s => s.Text).ToList();
            return new SelectList(lstDist, "Value", "Text", UserId);
        }

        public SelectList GetAllRoles()
        {
            CommonServices objCS = new CommonServices();
            IEnumerable<SelectListItem> lstDist = objCS.BindRoles().Data.OrderBy(s => s.RoleName).ToList().Select(m => new SelectListItem() { Text = m.RoleName, Value = m.RoleId.ToString() }).OrderBy(s => s.Text).ToList();
            return new SelectList(lstDist, "Value", "Text", UserId);
        }

        public SelectList GetAllBranches()
        {
            CommonServices objCS = new CommonServices();
            IEnumerable<SelectListItem> lstDist = objCS.BindBranches().Data.OrderBy(s => s.BranchName).ToList().Select(m => new SelectListItem() { Text = m.BranchName, Value = m.BranchId.ToString() }).OrderBy(s => s.Text).ToList();
            return new SelectList(lstDist, "Value", "Text", UserId);
        }
    }
}