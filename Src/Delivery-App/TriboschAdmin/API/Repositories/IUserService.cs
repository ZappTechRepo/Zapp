using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriboschAdmin.WebAPI.Repository
{
    interface IUserService
    {
        /// <summary>
        /// returns the specific userId if the user is authenticated successfully.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        int Authenticate(string userName, string word, string token);


    }
}
