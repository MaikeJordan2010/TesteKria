using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteKria.Models
{
    public class Repositorio
    {
        private string _Nome;
        private string _Descricao;
        private string _Linguagem;
        private DateTime _Dt_Atualizacao;
        private DateTime _Dt_Criacao;
        private DonoRepositorio _Dono;
        private int _ID;

        public string Nome { get => _Nome; set => _Nome = value; }
        public string Descricao { get => _Descricao; set => _Descricao = value; }
        public string Linguagem { get => _Linguagem; set => _Linguagem = value; }
        public DateTime Dt_Atualizacao { get => _Dt_Atualizacao; set => _Dt_Atualizacao = value; }
        public DonoRepositorio Dono { get => _Dono; set => _Dono = value; }
        public DateTime Dt_Criacao { get => _Dt_Criacao; set => _Dt_Criacao = value; }
        public int ID { get => _ID; set => _ID = value; }
    }
}
