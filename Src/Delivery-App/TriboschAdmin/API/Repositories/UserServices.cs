using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TriboschAdmin;


namespace Homemation.WebAPI.Repository
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


      


    }
}