using Homemation.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TriboschAdmin;

namespace Homemation.WebAPI.Repository
{
}
//    public class TokenServices : ITokenServices
//    {

//        private TriboschAppEntities dataContext = new TriboschAppEntities();

//        /// <summary>
//        ///TokenEntity  generates a token, encapsulates that token in a token entity with a Token expiry time and returns it to the caller.
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        //public TokenEntity GenerateToken(Guid userId)
//        //{
//        //    string token = Guid.NewGuid().ToString();
//        //    DateTime issuedOn = DateTime.Now;
//        //    DateTime expiredOn = DateTime.Now.AddSeconds(
//        //    Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));

//        //    TokenSaleRep token1 = dataContext.TokenSaleReps.FirstOrDefault(t => t.UserGuid == userId);
//        //    if (token1 == null)
//        //    {

//        //        var tokendomain = new TokenSaleRep
//        //        {
//        //            UserGuid = userId,
//        //            AuthToken = token,
//        //            IssuedOn = issuedOn,
//        //            ExpiresOn = expiredOn
//        //        };

//        //        dataContext.TokenSaleReps.Add(tokendomain);
//        //        dataContext.SaveChanges();
//        //    }
//        //    else
//        //    {

//        //        token1.IssuedOn = issuedOn;
//        //        token1.ExpiresOn = expiredOn;
//        //        token1.AuthToken = token;

//        //        dataContext.SaveChanges();

//        //    }

//        //    var tokenModel = new TokenEntity()
//        //    {
//        //        UserId = userId,
//        //        IssuedOn = issuedOn,
//        //        ExpiresOn = expiredOn,
//        //        AuthToken = token
//        //    };

//        //    return tokenModel;
//        //}

//        /// <summary>
//        /// Maintaining Session using Token
//        /// Method to validate token against expiry and existence in database.
//        /// Validates that the token associated with the request is valid or not, in other words it exists in the database within its expiry time limit
//        /// In the preceding code for token validation, first we check if the requested token exists in the database and is not expired. We check expiry by comparing it with the current date and time. If it is a valid token then we just update the token into the database with a new ExpiresOn time that is adding 900 seconds.
//        /// </summary>
//        /// <param name="tokenId"></param>
//        /// <returns></returns>
//        public bool ValidateToken(string tokenId)
//        {
//            var token = dataContext.TokenSaleReps.FirstOrDefault(t => t.AuthToken == tokenId && t.ExpiresOn > DateTime.Now);
//            if (token != null && !(DateTime.Now > token.ExpiresOn))
//            {
//                token.ExpiresOn = token.ExpiresOn.AddSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
//                //dataContext.Tokens.Attach(token);
//                dataContext.SaveChanges();
//                return true;
//            }
//            return false;
//        }  

//        /// <summary>
//        ///  removes the token from the database.
//        /// </summary>
//        /// <param name="tokenId"></param>
//        /// <returns></returns>
//        public bool Kill(string tokenId)
//        {
//            //dataContext.Tokens.delete  
//            var token = dataContext.Tokens.FirstOrDefault(x => x.AuthToken == tokenId);
//            dataContext.Tokens.Attach(token);
//            dataContext.Tokens.Remove(token);
//            dataContext.SaveChanges();

//            var isNotDeleted = dataContext.Tokens.Where(x => x.AuthToken == tokenId).Any();
//            if (isNotDeleted)
//            {
//                return false;
//            }
//            return true;
//        }

//        /// <summary>
//        /// deletes all the token entries from the database w.r.t specific userId associated with those tokens.
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public bool DeleteByUserId(Guid userId)
//        {
//            var token = dataContext.TokenSaleReps.FirstOrDefault(x =>  x.UserGuid == userId);

//            dataContext.TokenSaleReps.Attach(token);
//            dataContext.TokenSaleReps.Remove(token);
//            dataContext.SaveChanges();

//            var isNotDeleted = dataContext.TokenSaleReps.Where(x => x.UserGuid == userId).Any();
//            return !isNotDeleted;
//        }



//        /// <summary>
//        /// Get teh 
//        /// </summary>
//        /// <param name="UserId"></param>
//        /// <returns></returns>
//        public SalesRep ProfileDetail(Guid UserId)
//        {

//            SalesRep user = dataContext.SalesReps.FirstOrDefault(u => u.rowguid == UserId);
//            //var user = _unitOfWork.UserRepository.Get(u = > u.UserName == userName && u.word == word);  
//            if (user != null)
//            {
//                return user;
//            }
//            return null;
//        }

//    }
//}