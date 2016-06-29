using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TriboschAdmin;


namespace TriboschAdmin.WebAPI.Repository
{

    public class UserServices : IUserService  
    {

        private TriboschAppEntities dataContext = new TriboschAppEntities();

        public Guid Authenticate(string userName, string word) {

            //*Original code*
            //DeliveryUser user =  dataContext.DeliveryUsers.FirstOrDefault(u => u.UserName == userName && u.Password == word);
            // if (user != null && user.UserId > 0) {  
            //     return user.UserId;  
            // }  
            // return 0; 
           
          

           
            User user = dataContext.Users.FirstOrDefault(u => u.Username == userName && u.Password == word);
            if (user != null)
            {
                Guid gui = new Guid();
                return gui;
            }

            

            return Guid.Empty; 
 

        }

        int IUserService.Authenticate(string userName, string word)
        {
            User user = dataContext.Users.FirstOrDefault(u => u.Username == userName && u.Password == word);
            if (user != null)
            {
                Random r = new Random();
                int gui = r.Next(0, 100);
                return gui;
            }



            return 0;
        }
    }
}