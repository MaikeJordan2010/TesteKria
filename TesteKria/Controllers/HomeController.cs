using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TesteKria.Models;
using TesteKria.Util;
using TesteKria.ViewHelper;

namespace TesteKria.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index(string Repositorio)
        {

            BuscarRepositorios bResp = new BuscarRepositorios();                // Instancio obj classe BuscarRepositorio, que será utilizada para realizar a busca dos dados
            ViewHelper.ViewHelper lista = new ViewHelper.ViewHelper();          // Instanciado obj da classe ViewHelper
            
            lista.ListaRepositorio = bResp.ListarRepositorios(Repositorio);     // Recebendo retorno do metodo 

            return View(lista);                                                 // Returnando a View Index, passando a lista como parametros
        }

        /// <summary>
        /// ACTION DA TELA DE DETALHE
        /// </summary>
        /// <param name="Repositorio"></param>
        /// <param name="ID"></param>
        /// <returns>RETORNA A TELA DE DETALHE PASSANDO O REPOSITORIO </returns>
        [HttpGet]
        public ActionResult Detalhe(string Repositorio, int ID )
        {
            Repositorio repos = new Repositorio();                             // Inicia um obj de Repositório
            BuscarRepositorios bResp = new BuscarRepositorios();               // Instancio obj classe BuscarRepositorio, que será utilizada para realizar a busca dos dados

            repos = bResp.PesquisarUm(Repositorio, ID);                        // Recebendo a resposta do metodo PesquisarUm 
            return View(repos);                                                // retornando a view, passando o obj repositorio
        }

        
        /// <summary>
        /// ACTION LISTAR FAVORITOS
        /// </summary>
        /// <returns> OBJETO VIEW HELPER COM UMA LISTA DE REPOSITORIOS </returns>
        public  IActionResult ListarFavoritos()
        {

            List<Repositorio> lista = new List<Repositorio>();                                  // Instanciado obj da classe ViewHelper
            ViewHelper.ViewHelper vw = new ViewHelper.ViewHelper();
            Repositorio repos = null;
            DonoRepositorio dono = null;


            string t = System.IO.File.ReadAllText(@"wwwroot\upload\WriteLines.txt");                // lendo o arquivo  Texto

            if (t == null || t == "")                                                               // Verificando se não esta nulo
            {
                t = "[]";                                                                           // caso a lista seja nula, atribui uma lista vazia
            }

            dynamic din = JsonConvert.DeserializeObject(t);                                         // Transformando um obj Json em obj dinamico

            foreach (var f in din)
            {
                repos = new Repositorio();                                      // Cria obj de repositorio
                dono = new DonoRepositorio();                                   // Cria obj de repositorio

                repos.Nome = f.Nome;                                            // Recupera Nome do repositorio
                repos.Descricao = f.Descricao;                                  // Recupera Descrição do repositorio
                repos.Dt_Atualizacao = Convert.ToDateTime(f.Dt_Atualizacao);    // Recupera Data de Atualização do repositorio
                repos.Dt_Criacao = Convert.ToDateTime(f.Dt_Criacao);            // Recupera Data de Criação do 
                repos.ID = Convert.ToInt32(f.ID);                               // Recupera ID do repositorio
                repos.Linguagem = f.Linguagem;                                  // Recupera Linguagem do repositorio

                dono.Nome = f.Dono.Nome;                                        // Recupera Nome do Dono do repositorio
                dono.Url = f.Dono.Url;                                          // Recupera Perfil do Dono do repositorio

                repos.Dono = dono;                                              // Adciona o Dono do repositorio ao repositorio

                lista.Add(repos);
            }

            vw.ListaRepositorio = lista;                                        // adicionando a lista na ViewHelper


            return View(vw);                                                    // Returnando a View e passando o obj com os dados
        }

    }
}
