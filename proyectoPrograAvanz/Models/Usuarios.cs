using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoPrograAvanz.Models
{
    public class Usuarios
    {
        private string _SnombreUsu, _Scontra,_ScedUsu;/*La cedula es el id del cliente*/
        private int _IidUsu;

        public string SnombreUsu { get => _SnombreUsu; set => _SnombreUsu = value; }
        public string Scontra { get => _Scontra; set => _Scontra = value; }
        public string ScedUsu { get => _ScedUsu; set => _ScedUsu = value; }
        public int IidUsu { get => _IidUsu; set => _IidUsu = value; }
    }
}