using System;
using System.Collections.Generic;
using System.Text;

namespace A_P_SmartHub.Interfaces
{
    internal interface Senzor
    {
        public int Temerature {get; set;}
        public int Humidity {get; set;}
        public bool IsGasHarmfull { get; set;}
    }
}
