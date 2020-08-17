using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoPrograAvanz.Models
{
    public class Repartidor
    {
        private string _Scedula, _Snombre, _Sapellidos, _StelCelular, _Scorreo;
        public string Scedula { get => _Scedula; set => _Scedula = value; }
        public string Snombre { get => _Snombre; set => _Snombre = value; }
        public string Sapellidos { get => _Sapellidos; set => _Sapellidos = value; }
        public string StelCelular { get => _StelCelular; set => _StelCelular = value; }
        public string Scorreo { get => _Scorreo; set => _Scorreo = value; }
       
    }
}