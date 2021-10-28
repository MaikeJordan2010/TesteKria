using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TesteKria.Models;

namespace TesteKria.Util
{
    public class BuscarRepositorios
    {
        private object client;

        public String BuscarApi(String url)
        {
            var requisicaoWeb = WebRequest.CreateHttp(url);                 // Declara variavel do tipo WebRequest passando a URL como parametro!
            requisicaoWeb.Method = "GET";                                   //chama o metodo da requisição, nesse caso é GET.
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";                  // Realiza a Requisição utilizando os parametros anteriores.
            try
            {
                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(streamDados);
                    object objResponse = reader.ReadToEnd(); //
                    return objResponse.ToString();                               // retorna a resposta como String

                }
            }
            catch(Exception ex)
            {

            }
            return "";
        }

        public dynamic ConverterURL(String url)
        {
            String json = BuscarApi(url);                                       // Chama função de busca do Json passando como parametro a url recebida

            //String dados = JsonConvert.ToString(json.ToString());
            //dynamic jsonTratado = JValue.Parse(dados);

            dynamic JsonDinamic = JsonConvert.DeserializeObject(json);          // converte a resposta recebida em dynamic

            return JsonDinamic;                                                 // retorna a resposta

        }


        /// <summary>
        /// METODO QUE RECEBE O NOME DO REPOSITORIO E DEVOLVE UMA LISTA
        /// </summary>
        /// <param name="Repositorio"></param>
        /// <returns></returns>
        public List<Repositorio> ListarRepositorios(string Repositorio)
        {

            List<Repositorio> listaRepositorio = new List<Repositorio>();                       // Instanciando lista de repositorio
            Repositorio resp = null;                                                            // instanciando obj Repositorio
            DonoRepositorio dono = null;                                                        // Instanciando obj de Dono do repositorio

            dynamic obj;                                                                        // declaranco obj dinamico
            dynamic item;                                                                       // declaranco obj dinamico
            String url = "";                                                                    // declaranco url

            if (Repositorio != null && Repositorio != "")                                       // verificando se o parametro recebido é vazio ou nulo
            {
                url = "https://api.github.com/search/repositories?q=" + Repositorio;            // Se não for vazio ou nulo, usa a url de pesquisa de repositorios
            }
            else
            {
                url = "https://api.github.com/users/" + "maikejordan2010" + "/repos";           // se for vazio a url receberá o link para os repositorios de maikejordan
            }


            obj = ConverterURL(url);                                                            // Chama função de pesquisa e recebe o resultado

            try
            {
                if (Repositorio != null && Repositorio != "")                                   // caso seja nulo ele recebe o objeto inteiro, senão ele recebe o obj.items
                {
                    item = obj.items;
                }
                else
                {
                    item = obj;
                }

                foreach (var ob in item)                                                        // percorre todo o obj e aciona cada repositorio a lista
                {
                    resp = new Repositorio();                                                   // completa a instancia do obj
                    dono = new DonoRepositorio();                                               // instancia o obj

                    resp.Nome = ob.name;                                                        // preenche o obj com o parametro recebido
                    resp.Descricao = ob.description;                                            // preenche o obj com o parametro recebido
                    resp.Dt_Atualizacao = Convert.ToDateTime(ob.updated_at);                    // preenche o obj com o parametro recebido
                    resp.Dt_Criacao = Convert.ToDateTime(ob.created_at);                        // preenche o obj com o parametro recebido
                    resp.ID = Convert.ToInt32(ob.id);                                           // preenche o obj com o parametro recebido
                    resp.Linguagem = ob.language;                                               // preenche o obj com o parametro recebido

                    dono.Nome = ob.owner.login;                                                 // preenche o obj com o parametro recebido
                    dono.Url = ob.owner.repos_url;                                              // preenche o obj com o parametro recebido

                    resp.Dono = dono;                                                           // adiciona o dono ao repositorio

                    listaRepositorio.Add(resp);                                                 // adiciona o repositorio na lista
                }
            }
            catch(Exception ex)
            {

            }

           
            return listaRepositorio;                                                        // retorna  a lista 
        }


        public Repositorio PesquisarUm(string Repositorio, int ID)
        {
            Repositorio repos = new Repositorio();                                          // instanciando obj de repositorio
            List<Repositorio> lista = new List<Repositorio>();                              // instanciando obj de lista de repositorio
            try
            {
                lista = ListarRepositorios(Repositorio);                                    // chama o metodo ListarRepositorio passando o nome do repositorio e recebendo a resposta

                foreach (var r in lista)                                                    // percorre toda lista recebida
                {
                    if (r.ID == ID)                                                         // comprar o ID
                    {
                        repos = r;                                                          // se o id for igual, o obj recebe  o valor de r
                    }
                }
            }
            catch (Exception ex)
            {

            }
           
            return repos;                                                                   // retorna o valor do repositorio
        }

      
    }
}
