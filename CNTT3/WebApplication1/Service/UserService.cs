using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Service
{
    public class UserService
    {
        MyDBDataContext db = new MyDBDataContext(ConfigurationManager.ConnectionStrings["DataSourceConnectionString"].ConnectionString);

        public List<Tb_User> GetAll()
        {
            List<Tb_User> tb_Users = db.Tb_Users.ToList();
            return tb_Users;
        }

        public bool checklogin(string username, string password)
        {
            try
            {
                if (db.Tb_Users.Where(x => x.Username.Equals(username) && x._Password.Equals(password)).FirstOrDefault() != null)
                    return true;
                else
                    return false;
            }catch(Exception e)
            {
                Console.WriteLine("Lỗi truy vấn : " + e);
                return false;
            }
        }

        public int getIdUser(string username)
        {
            return db.Tb_Users.Where(x => x.Username.Equals(username)).FirstOrDefault().IdUser;
        }

        public UserDTO getUserDetails(int Id)
        {
            try
            {
                var item = db.Tb_Users.Where(x => x.IdUser == Id).FirstOrDefault();
                if (item != null)
                {                    
                    UserDTO u = new UserDTO()
                    {
                        IdUser = item.IdUser,
                        UserName = item.Username,
                        Password = item._Password,
                        Address = item._Address,
                        FullName = item.FullName,
                        Avatar = new Firebase().GetImageDetails(item.IdUser).Avatar,
                        Permission = item.Permission
                    };
                    return u;
                }                    
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Lỗi truy vấn : " + e);
                return null;
            }
        }

        public bool EditUser(UserDTO userDto)
        {
            try
            {
                Tb_User user = db.Tb_Users.Single(x => x.IdUser == userDto.IdUser);
                user.Username = userDto.UserName;
                user.Permission = userDto.Permission;
                user.FullName = userDto.FullName;
                user._Address = userDto.Address;
                user._Password = userDto.Password;
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void DeleteUser(int Id)
        {
            var UserDelete = db.Tb_Users.Where(x => x.IdUser == Id).FirstOrDefault();
            if(UserDelete!=null)
            {
                db.Tb_Users.DeleteOnSubmit(UserDelete);
                db.SubmitChanges();
            }
        }

        public bool CreateNewUser(Tb_User tb_User)
        {
            try
            {
                
                db.Tb_Users.InsertOnSubmit(tb_User);
                db.SubmitChanges();
                int IdUser = getIdUser(tb_User.Username);
                bool result = new Firebase().AddImageUser(new ImageUser(IdUser, null));
                return true;
            }catch(Exception e)
            {
                return false;
            }


        }
    }
}