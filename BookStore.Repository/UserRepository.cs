using BookStore.Models.DataModels;
using BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class UserRepository : BaseRepository
    {
        public User Login(Models.Models.LoginModel model)
        {
            return _context.Users.FirstOrDefault(c => c.Email.Equals(model.Email.ToLower()) && c.Password.Equals(model.Password));
        }

        public User Register(RegisterModel model)
        {
            User user = new User()
            {
                Email = model.Email,
                Password = model.Password,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Roleid = model.Roleid,
            };
            

            var entry = _context.Users.Add(user);
            _context.SaveChanges();
            return entry.Entity;
        }

        public List<User> GetUsers(int pageIndex, int pageSize, string keyword)
        {
            // var users = _context.Users.ToList().Where();
            // in above condition 1st it take whole datas and then search which take too much time 
            // but in AsQueryable 1st we mopped tables and then took data which take less time compare to where condition
            var users = _context.Users.AsQueryable();

            if (pageIndex > 0)
            {
                if (String.IsNullOrEmpty(keyword) == false)
                {
                    users = users.Where(w => w.Firstname.ToLower().Contains(keyword) || w.Lastname.ToLower().Contains(keyword));
                }
                // skip 1st record and take 2nd racord as 1st record of that list
                // var usersLIst = users.Skip(1).Take(1).ToList();
                var userList = users.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return userList;
            }
            return null;
        }

        public User GetUser(int id)
        {
            // var users = _context.Users.ToList().Where();
            // in above condition 1st it take whole datas and then search which take too much time 
            // but in AsQueryable 1st we mopped tables and then took data which take less time compare to where condition
            var users = _context.Users.AsQueryable();

            if (id > 0)
            {
                return _context.Users.Where(w => w.Id == id).FirstOrDefault();
            }
            return null;
        }

        public bool UpdateUser(User model)
        {
            if(model.Id > 0)
            {
                _context.Update(model);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public bool DeleteUser(User model)
        {
            if (model.Id > 0)
            {
                _context.Remove(model);
                _context.SaveChanges();

                return true;
            }
            return false;
        }


    }
}
