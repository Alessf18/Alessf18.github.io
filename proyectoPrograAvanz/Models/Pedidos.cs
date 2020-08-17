using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoPrograAvanz.Models
{
    public class Pedidos
    {
        private string _Sdescripcion, _Subicacion, _Scodigo,_SidCliente,_SidRerpart;
        private int _Iid;
        private DateTime _DtfechaEntr,_DthoraEntr;
        
        private char _Cestado;

        public string Sdescripcion { get => _Sdescripcion; set => _Sdescripcion = value; }
        public string Subicacion { get => _Subicacion; set => _Subicacion = value; }
        public DateTime DtfechaEntr { get => _DtfechaEntr; set => _DtfechaEntr = value; }
        public DateTime DthoraEntr { get => _DthoraEntr; set => _DthoraEntr = value; }
       
        public char Cestado { get => _Cestado; set => _Cestado = value; }
        public string Scodigo { get => _Scodigo; set => _Scodigo = value; }
        public int Iid { get => _Iid; set => _Iid = value; }
        public string SidCliente { get => _SidCliente; set => _SidCliente = value; }
        public string SidRerpart { get => _SidRerpart; set => _SidRerpart = value; }

        /* temgo que hacer una tabla intermedia entre productos y pedidos (productos por pedido)*/
    }
}