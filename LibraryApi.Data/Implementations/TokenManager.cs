//using LibraryApi.Data.Interfaces;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Caching.Distributed;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LibraryApi.Data.Implementations
//{
//    public class TokenManager : ITokenManager
//    {
//        public TokenManager(IDistributedCache cache,
//            IHttpContextAccessor httpContextAccessor,
//            IOptions<AuthenticationManager> options)
//        {

//        }
//        public async Task<bool> IsCurrentActiveToken()
//        {
//            throw new NotImplementedException();
//        }

//        public async Task DeactivateCurrentAsync()
//        {
//            throw new NotImplementedException();
//        }     
        
//        public async Task<bool> IsActiveAsync(string token)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task DeactivateAsync(string token)
//        {
//            throw new NotImplementedException();
//        }

//    }
//}
