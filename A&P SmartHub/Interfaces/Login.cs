using System;
using System.Collections.Generic;
using System.Text;

namespace A_P_SmartHub.Interfaces
{
    interface ILogin
    {
        string Username { get; set; }
        string Password { get; set; }
        bool IsLoggedIn { get; set; }




    }
}
