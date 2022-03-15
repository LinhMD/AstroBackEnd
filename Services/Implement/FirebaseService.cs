using AstroBackEnd.Services.Core;
using Firebase.Auth;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AstroBackEnd.Services.Implement
{
    public class FirebaseService : IFirebaseService
    {

        private static string ApiKey = "AIzaSyBk4Z_g5vowQemjfjtfzlZIRbG2JIDiCB0";
        private static string Bucket = "astrology-a5858.appspot.com";
        private static string AuthEmail = "backend@gmail.com";
        private static string AuthPassword = "spirit1233";

        public FirebaseService()
        {

        }
        public async Task<string> UploadImage(Stream image, string name)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
            CancellationTokenSource cancel = new CancellationTokenSource();
            var task = new FirebaseStorage(Bucket, new FirebaseStorageOptions()
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                ThrowOnCancel = true,
            });
            string link = string.Empty;
            try
            {
                link = await task.Child("userchart").Child(name).PutAsync(image);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw new ArgumentException(e.Message);
            }
            
            return link;
        }
    }
}
