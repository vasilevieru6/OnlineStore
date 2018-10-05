using IdentityServer4.Quickstart.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.IdentityServer.Quickstart.Account
{
    public class RegistrationViewModel : RegistrationInputModel
    {
        public IEnumerable<ExternalProvider> ExternalProviders { get; set; }
    }
}
