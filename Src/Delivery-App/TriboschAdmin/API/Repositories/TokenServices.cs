using Homemation.WebAPI.Models;
using Homemation.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TriboschAdmin;
using TriboschAdmin.Models;

namespace Homemation.WebAPI.Repository
{
}
public class TokenServices : ITokenServices
{

    private TriboschAppEntities dataContext = new TriboschAppEntities();

    /// <summary>
    ///TokenEntity generates a token, encapsulates that token in a token entity with a Token expiry time and returns it to the caller.
    /// </summary>
    /// <param name = "userId" ></ param >
    public TokenEntity GenerateToken(int userId)
    {
        string token = Guid.NewGuid().ToString();
        DateTime issuedOn = DateTime.Now;
        DateTime expiredOn = DateTime.Now.AddSeconds(
        Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));

        TriboschAdmin.TokenSaleRep token1 = dataContext.TokenSaleReps.FirstOrDefault(t => t.UserId == userId);
        if (token1 == null)
        {

            var tokendomain = new TriboschAdmin.TokenSaleRep
            {
                UserId = userId,
                AuthToken = token,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn
            };

            dataContext.TokenSaleReps.Add(tokendomain);
            dataContext.SaveChanges();
        }
        else
        {

            token1.IssuedOn = issuedOn;
            token1.ExpiresOn = expiredOn;
            token1.AuthToken = token;

            dataContext.SaveChanges();

        }

        var tokenModel = new TokenEntity()
        {
            UserId = userId,
            IssuedOn = issuedOn,
            ExpiresOn = expiredOn,
            AuthToken = token
        };

        return tokenModel;
    }

    /// <summary>
    /// Maintaining Session using Token
    /// Method to validate token against expiry and existence in database.
    /// Validates that the token associated with the request is valid or not, in other words it exists in the database within its expiry time limit
    /// In the preceding code for token validation, first we check if the requested token exists in the database and is not expired. We check expiry by comparing it with the current date and time. If it is a valid token then we just update the token into the database with a new ExpiresOn time that is adding 900 seconds.
    /// </summary>
    /// <param name="tokenId"></param>
    /// <returns></returns>
    public int ValidateToken(string tokenId)
    {
        var token = dataContext.TokenSaleReps.FirstOrDefault(t => t.AuthToken == tokenId);
        if (token != null)
        {
            token.ExpiresOn = DateTime.Now.AddYears(20);
            //dataContext.Tokens.Attach(token);
            dataContext.SaveChanges();

            return Int32.Parse(token.UserId.ToString());
        }

        return -1;
    }

    /// <summary>
    /// Get teh 
    /// </summary>
    /// <param name="UserId"></param>
    /// <returns></returns>
    public TriboschAdmin.User ProfileDetail(int UserId)
    {

        TriboschAdmin.User user = dataContext.Users.FirstOrDefault(u => u.UserID == UserId);
        //var user = _unitOfWork.UserRepository.Get(u = > u.UserName == userName && u.word == word);  
        if (user != null)
        {
            return user;
        }
        return null;
    }


    public bool Kill(string tokenId)
    {
        throw new NotImplementedException();
    }

    public bool DeleteByUserId(Guid userId)
    {
        throw new NotImplementedException();
    }

    TriboschAdmin.User ITokenServices.ProfileDetail(string username)
    {
        TriboschAdmin.User user = dataContext.Users.FirstOrDefault(u => u.Username == username);
        //var user = _unitOfWork.UserRepository.Get(u = > u.UserName == userName && u.word == word);  
        if (user != null)
        {
            return user;
        }
        return null;
    }
}