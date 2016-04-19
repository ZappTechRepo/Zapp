using Homemation.WebAPI.Models;
using Homemation.WebAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Homemation.WebAPI.Repository
{

    public class UserServices : IUserService  
    {

        private EntitiesConnectionA dataContext = new EntitiesConnectionA();

        public Guid Authenticate(string userName, string word) {

            //*Original code*
            //DeliveryUser user =  dataContext.DeliveryUsers.FirstOrDefault(u => u.UserName == userName && u.Password == word);
            // if (user != null && user.UserId > 0) {  
            //     return user.UserId;  
            // }  
            // return 0; 
           
          

           
            SalesRep user = dataContext.SalesReps.FirstOrDefault(u => u.Email == userName && u.Password == word);
            if (user != null && user.rowguid  !=  Guid.Empty)
            {
               
                return user.rowguid;
            }

            

            return Guid.Empty; 
 

        }


      


    }
}