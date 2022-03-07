using FirebaseAdmin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Utilities
{
    public class FireabaseUtility
    {
        public async Task<UserRecord> getFireBaseUser(string Uid)
        {
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(Uid);
            return userRecord;
        }

        public async Task<UserRecord> getFireBaseUserByToken(string token)
        {
            
            FirebaseToken decode = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);

            string uid = decode.Uid;

            return await this.getFireBaseUser(uid);
        }

    }
}
