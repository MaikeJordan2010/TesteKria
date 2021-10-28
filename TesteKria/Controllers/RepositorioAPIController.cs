using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TesteKria.Models;
using TesteKria.Util;

namespace TesteKria.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class RepositorioAPIController : ControllerBase
    {

        [Route("api/RepositorioAPI/AddFavoritos")]
        [HttpPost]
        public Repositorio AddFavoritos([FromForm] string Nome, [FromForm] string Descricao, [FromForm] int ID, [FromForm] string Dt_Criacao, [FromForm] string Dt_Atualizacao,
                               [FromForm] string NomeDono, [FromForm] string UrlDono, [FromForm] string Linguagem)
        {

            Repositorio rep = new Repositorio();                                                                        // Inicia um obj de Repositório
            DonoRepositorio d = new DonoRepositorio();                                                                  // Inicia um obj de Dono
            rep.Nome = Nome == null ? "" : Nome;                                                                        // Passando parametro recebido caso não seja nulo
            rep.Dt_Atualizacao = Dt_Atualizacao == null ? Convert.ToDateTime("") : Convert.ToDateTime(Dt_Atualizacao);  // Passando parametro recebido caso não seja nulo
            rep.Dt_Criacao = Dt_Criacao == null ? Convert.ToDateTime("") : Convert.ToDateTime(Dt_Criacao);              // Passando parametro recebido caso não seja nulo
            rep.Descricao = Descricao == null ? "" : Descricao;                                                         // Passando parametro recebido caso não seja nulo
            rep.Linguagem = Linguagem == null ? "" : Linguagem;                                                         // Passando parametro recebido caso não seja nulo
            d.Nome = NomeDono == null ? "" : NomeDono;                                                                  // Passando parametro recebido caso não seja nulo
            d.Url = UrlDono == null ? "" : UrlDono;                                                                     // Passando parametro recebido caso não seja nulo

            rep.Dono = d;                                                                                               // Adicionando o dono ao repositorio

            Repositorio repos = null;                                       // Inicia um obj de Repositório
            DonoRepositorio dono = null;                                    // Inicia um obj de Dono

            try
            {
                List<Repositorio> lista = new List<Repositorio>();              // Inicia uma lista de objs de Repositorio

                string dt = HttpContext.Session.GetString("Favoritos");         // Recuperando lista gravada na sessão

                if (dt == null)                                                  // Verificando se a lista é nulla
                {
                    dt = "[]";                                                  // se a lista do nula, add a ela uma lista vazia
                }


                dynamic r = JsonConvert.DeserializeObject(dt);                  // Desealizando um obj json para obj Dinanmico

                foreach (var f in r)                                           // Percorre todos os itens da lista do obj dinamico
                {
                    repos = new Repositorio();                                  // Cria obj de repositorio
                    dono = new DonoRepositorio();                               // Cria obj de repositorio

                    repos.Nome = f.Nome;                                        // Recupera Nome do repositorio
                    repos.Descricao = f.Descricao;                              // Recupera Descrição do repositorio
                    repos.Dt_Atualizacao = Convert.ToDateTime(f.Dt_Atualizacao);// Recupera Data de Atualização do repositorio
                    repos.Dt_Criacao = Convert.ToDateTime(f.Dt_Criacao);        // Recupera Data de Criação do 
                    repos.ID = Convert.ToInt32(f.ID);                           // Recupera ID do repositorio
                    repos.Linguagem = f.Linguagem;                              // Recupera Linguagem do repositorio

                    dono.Nome = f.Dono.Nome;                                    // Recupera Nome do Dono do repositorio
                    dono.Url = f.Dono.Url;                                      // Recupera Perfil do Dono do repositorio

                    repos.Dono = dono;                                          // Adciona o Dono do repositorio ao repositorio

                    lista.Add(repos);                                           // Add o obj do repositorio a lista de objs de repositorio
                }
                lista.Add(rep);                                                 // Add o ultimo obj favorito a lista

                string fav = JsonConvert.SerializeObject(lista);                // Serializando uma lista de obj repositorio em obj json

                HttpContext.Session.SetString("Favoritos", fav);
            }
            catch (Exception ex)
            {

            }
            // Gravando na sessão

            return rep;                                                   // redirecionando para  a tela de lista de favoritos
        }



        [Route("api/RepositorioAPI/ListarFavoritos")]
        [HttpGet]
        public List<Repositorio> ListarFavoritos()
        {
            Repositorio repos = null;                                       // Inicia um obj de Repositório
            DonoRepositorio dono = null;                                    // Inicia um obj de Dono
            List<Repositorio> lista = new List<Repositorio>();              // Inicia uma lista de objs de Repositorio


            string dt = HttpContext.Session.GetString("Favoritos");         // Recupera lista gravada na sessão

            if (dt == null)                                                 // Verifica se a lista é nula
            {
                dt = "[]";                                                  // se for nula add uma lista vazia
            }


            dynamic r = JsonConvert.DeserializeObject(dt);                  // Serializa a lista em um obj dinamico

            foreach (var f in r)                                           // Percorre todos os itens da lista do obj dinamico
            {
                repos = new Repositorio();                                  // Cria obj de repositorio
                dono = new DonoRepositorio();                               // Cria obj de repositorio

                repos.Nome = f.Nome;                                        // Recupera Nome do repositorio
                repos.Descricao = f.Descricao;                              // Recupera Descrição do repositorio
                repos.Dt_Atualizacao = Convert.ToDateTime(f.Dt_Atualizacao);// Recupera Data de Atualização do repositorio
                repos.Dt_Criacao = Convert.ToDateTime(f.Dt_Criacao);        // Recupera Data de Criação do 
                repos.ID = Convert.ToInt32(f.ID);                           // Recupera ID do repositorio
                repos.Linguagem = f.Linguagem;                              // Recupera Linguagem do repositorio

                dono.Nome = f.Dono.Nome;                                    // Recupera Nome do Dono do repositorio
                dono.Url = f.Dono.Url;                                      // Recupera Perfil do Dono do repositorio

                repos.Dono = dono;                                          // Adciona o Dono do repositorio ao repositorio

                lista.Add(repos);                                           // Add o obj do repositorio a lista de objs de repositorio
            }

            return lista;                                                // Returnando a View e passando o obj com os dados
        }


        [Route("api/RepositorioAPI/Add")]
        [HttpPost]
        public Repositorio Add([FromForm] string Nome, [FromForm] string Descricao, [FromForm] int ID, [FromForm] string Dt_Criacao, [FromForm] string Dt_Atualizacao,
                              [FromForm] string NomeDono, [FromForm] string UrlDono, [FromForm] string Linguagem)
        {
            List<Repositorio> lista = new List<Repositorio>();                                                                  // Instanciando lista de repositorios
            Repositorio rep = new Repositorio();                                                                                // Inicia um obj de Repositório
            Repositorio repos = null;                                                                                           // Inicia um obj de Repositório
            DonoRepositorio dono = null;                                                                                        // Inicia um obj Dono de repositorio
            DonoRepositorio d = new DonoRepositorio();                                                                          // Inicia um obj Dono de repositorio

            rep.ID = ID;                                                                                                        // passando parametro
            rep.Nome = Nome == null ? "" : Nome;                                                                                // passando parametro caso não nulo
            rep.Dt_Atualizacao = Dt_Atualizacao == null ? Convert.ToDateTime("01/01/2020") : Convert.ToDateTime(Dt_Atualizacao);// passando parametro caso não nulo
            rep.Dt_Criacao = Dt_Criacao == null ? Convert.ToDateTime("01/01/2020") : Convert.ToDateTime(Dt_Criacao);            // passando parametro caso não nulo
            rep.Descricao = Descricao == null ? "" : Descricao;                                                                 // passando parametro caso não nulo
            rep.Linguagem = Linguagem == null ? "" : Linguagem;                                                                 // passando parametro caso não nulo
            d.Nome = NomeDono == null ? "" : NomeDono;                                                                          // passando parametro caso não nulo
            d.Url = UrlDono == null ? "" : UrlDono;                                                                             // passando parametro caso não nulo
            rep.Dono = d;                                                                                                       // passando Obj dono para repositorio


            string t  = System.IO.File.ReadAllText(@"wwwroot\upload\WriteLines.txt");                                           // lendo arquivo texto

            if(t == null || t == "")                                                                                            // verificando se não é vazio ou nulo
            {
                t = "[]";                                                                                                       // atribui uma lista vazia caso nulo
            }

            dynamic din = JsonConvert.DeserializeObject(t);                                                                     // tranformando obj Json em obj dinamico

            foreach(var f in din)
            {
                repos = new Repositorio();                                                                                      // Cria obj de repositorio
                dono = new DonoRepositorio();                                                                                   // Cria obj de repositorio

                repos.Nome = f.Nome;                                                                                            // Recupera Nome do repositorio
                repos.Descricao = f.Descricao;                                                                                  // Recupera Descrição do repositorio
                repos.Dt_Atualizacao = Convert.ToDateTime(f.Dt_Atualizacao);                                                    // Recupera Data de Atualização do repositorio
                repos.Dt_Criacao = Convert.ToDateTime(f.Dt_Criacao);                                                            // Recupera Data de Criação do 
                repos.ID = Convert.ToInt32(f.ID);                                                                               // Recupera ID do repositorio
                repos.Linguagem = f.Linguagem;                                                                                  // Recupera Linguagem do repositorio

                dono.Nome = f.Dono.Nome;                                                                                        // Recupera Nome do Dono do repositorio
                dono.Url = f.Dono.Url;                                                                                          // Recupera Perfil do Dono do repositorio

                repos.Dono = dono;                                                                                              // Adciona o Dono do repositorio ao repositorio

                lista.Add(repos);                                                                                               // add obj a lista 
            }


            lista.Add(rep);                                                                                                     // add novo obj a lista dos antigos

            string txt = JsonConvert.SerializeObject(lista);                                                                    // tranforma a lista em Json

            System.IO.File.WriteAllText(@"wwwroot\upload\WriteLines.txt", txt);                                                 // Grava no arquivo de texto


            return rep;                                                                                                         // redirecionando para  a tela de lista de favoritos
        }


    }
}