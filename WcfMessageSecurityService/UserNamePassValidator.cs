using System;
using System.ServiceModel;

namespace WcfPhuDinhEntitiesService
{
    public class UserNamePassValidator :
          System.IdentityModel.Selectors.UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName == null || password == null)
            {
                throw new ArgumentNullException();
            }

            if (!(userName == "fayaz" && password == "soomro"))
            {
                throw new FaultException("Incorrect Username or Password");
            }
        }
    }
}