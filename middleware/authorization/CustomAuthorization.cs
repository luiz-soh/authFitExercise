using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace middleware.authorization
{
    [AttributeUsage(AttributeTargets.Class)] 
    public class CustomAuthorization : Attribute, IAuthorizationFilter  
    {
        
    }
}