using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteKria.Models
{
    public class DonoRepositorio
    {
        private string _Nome;
        private string _Url;
        private int _Id;

        public string Nome { get => _Nome; set => _Nome = value; }
        public string Url { get => _Url; set => _Url = value; }
        public int Id { get => _Id; set => _Id = value; }
    }
}
