using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiscuitLandAngular.Models
{
    public partial class Login
    {
        #region Public Properties

        public string UserName { get; set; }

        public string Password { get; set; }

        #endregion

        #region Private Properties

        private string EncryptionSeed { get; set; }

        private DBHelper _dBHelper;

        #endregion
    }
}
