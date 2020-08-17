using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoPrograAvanz.Models
{
    public class Clientes
    {
        private string _Snombre, _Sapellidos, _Scorreo, _Scedula,_StelCel,_StelCasa;

        public string Snombre { get => _Snombre; set => _Snombre = value; }
        public string Sapellidos { get => _Sapellidos; set => _Sapellidos = value; }
       
        public string Scedula { get => _Scedula; set => _Scedula = value; }
        public string StelCel { get => _StelCel; set => _StelCel = value; }
        public string StelCasa { get => _StelCasa; set => _StelCasa = value; }
    }
}