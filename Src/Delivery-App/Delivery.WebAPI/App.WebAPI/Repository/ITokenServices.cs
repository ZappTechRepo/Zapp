using Homemation.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemation.WebAPI.Repository
{
   public interface ITokenServices {
       
       #region Interface member methods.  
        /// <summary>  
        /// Function to generate unique token with expiry against the provided userId.  
        /// Also add a record in database for generated token.  
        /// </summary>  
        /// <param name="userId"></param>  
        /// <returns></returns>  
        TokenEntity GenerateToken(Guid userId);  
  
        /// <summary>  
        /// Function to validate token againt expiry and existance in database.  
        /// </summary>  
        /// <param name="tokenId"></param>  
        /// <returns></returns>  
        bool ValidateToken(string tokenId);  
  
        /// <summary>  
        /// Method to kill the provided token id.  
        /// </summary>  
        /// <param name="tokenId"></param>  
        bool Kill(string tokenId);  
  
        /// <summary>  
        /// Delete tokens for the specific deleted user  
        /// </summary>  
        /// <param name="userId"></param>  
        /// <returns></returns>  
        bool DeleteByUserId(Guid userId);


        /// <summary>
        /// returns the specific userId if the user is authenticated successfully.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        SalesRep ProfileDetail(Guid UserId);

       #endregion  
    }  

}
