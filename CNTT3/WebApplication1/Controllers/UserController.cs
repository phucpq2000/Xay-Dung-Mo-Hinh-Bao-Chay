using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data;
using System.Net;
using System.Web.Security;
using WebApplication1.Service;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {



        // GET: User
        public ActionResult login()
        {
            return View(new UserDTO());
        }

        [HttpPost]
        public ActionResult login(string username, string password)
        {
            if (new UserService().checklogin(username, password))
            {
                int IdUser = new UserService().getIdUser(username);
                return RedirectToAction("StayLogin", new { IdUser = IdUser });
            }
            else
            {
                Session["message"] = "Vui lòng đăng nhập lại !";
                return RedirectToAction("login");
            }

        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult StayLogin(int IdUser)
        {
            UserDTO u = new UserService().getUserDetails(IdUser);
            Session["IdUser"] = IdUser;
            Session["StayLogin"] = "logged";
            return View(u);
        }

        public ActionResult ManageUser(int Id)
        {
            Session["IdUser"] = Id;
            return View(new UserService().GetAll());
        }


        public ActionResult Details(int id)     
        {
            //var dataid = db.Tb_Users.Single(x => x.IdUser == id);dataid
            return View(new UserService().getUserDetails(id));


        }
        public ActionResult Edit(int id)
        {
            return View(new UserService().getUserDetails(id));
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(UserDTO collection)
        {
            if (new UserService().EditUser(collection))
            {
                return RedirectToAction("ManageUser", new { Id = int.Parse(Session["IdUser"].ToString()) });
            }
            else
            {
                return View();
            }
        }



        public ActionResult Create()
        {
            return View();
        }

        public ActionResult BackToManagePage()
        {
            return RedirectToAction("StayLogin", new { Id = int.Parse(Session["IdUser"].ToString()) });
        }


        [HttpPost]
        public ActionResult Create(Tb_User user)
        {

            if (new UserService().CreateNewUser(user))
            {
                return RedirectToAction("ManageUser", new { Id = int.Parse(Session["IdUser"].ToString()) });
            }
            else
            {
                return View();
            }

        }

        public ActionResult ManageIoT()
        {
            return View();
        }
        public ActionResult Delete(bool confirm, int id)
        {
            new UserService().DeleteUser(id);
            return RedirectToAction("ManageUser", new { Id = int.Parse(Session["IdUser"].ToString()) });
        }

       
    }
}