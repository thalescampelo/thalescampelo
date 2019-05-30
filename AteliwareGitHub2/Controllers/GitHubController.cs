using AteliwareGitHub2.Models;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AteliwareGitHub2.Controllers
{
    public class GitHubController : Controller
    {
        private Context db = new Context();
        private List<GitHub> gitHubs = new List<GitHub>();

        public ActionResult Index()
        {
            GitHubClient gitHubClient = new GitHubClient(new ProductHeaderValue("MyGitHubAPI"));

            Task task = new Task(() => Consulta(gitHubClient));
            task.Start();

            while(gitHubs.Count == 0)
            {
                task.Wait();
            }
            
            return View(db.GitHub.OrderBy(ob => ob.StargazersCount).ToList());
        }

        public ActionResult Detalhes(int identificador)
        {
            try
            {
                GitHub github = db.GitHub.Find(identificador);

                return View(github);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        private async void Consulta(GitHubClient gitHubClient)
        {
            int identificador = 1;
            List<GitHub> gitHubList = new List<GitHub>();
            try
            {
                db.GitHub.RemoveRange(db.GitHub.ToList());
                db.SaveChanges();
                gitHubs = db.GitHub.ToList();

                List<SearchRepositoriesRequest> requisicoes = new List<SearchRepositoriesRequest>();

                #region CSharp
                requisicoes.Add(new SearchRepositoriesRequest()
                {
                    Language = Language.CSharp,
                    SortField = RepoSearchSort.Stars,
                    Order = SortDirection.Descending,
                    PerPage = 1
                });
                #endregion

                // PYTHON
                requisicoes.Add(new SearchRepositoriesRequest()
                {
                    Language = Language.Python,
                    SortField = RepoSearchSort.Stars,
                    Order = SortDirection.Descending,
                    PerPage = 1
                });

                // JavaScript
                requisicoes.Add(new SearchRepositoriesRequest()
                {
                    Language = Language.JavaScript,
                    SortField = RepoSearchSort.Stars,
                    Order = SortDirection.Descending,
                    PerPage = 1
                });

                // Java
                requisicoes.Add(new SearchRepositoriesRequest()
                {
                    Language = Language.Java,
                    SortField = RepoSearchSort.Stars,
                    Order = SortDirection.Descending,
                    PerPage = 1
                });

                // Ruby
                requisicoes.Add(new SearchRepositoriesRequest()
                {
                    Language = Language.Ruby,
                    SortField = RepoSearchSort.Stars,
                    Order = SortDirection.Descending,
                    PerPage = 1
                });

                List<SearchRepositoryResult> resultados = new List<SearchRepositoryResult>();
                foreach(SearchRepositoriesRequest requsicao in requisicoes)
                {
                    resultados.Add( await gitHubClient.Search.SearchRepo(requsicao));
                }
                
                foreach(SearchRepositoryResult resultado in resultados)
                {
                    foreach (var item in resultado.Items)
                    {
                        gitHubList.Add(new GitHub()
                        {
                            Identificador = identificador++,
                            CreatedAt = item.CreatedAt.DateTime,
                            Description = item.Description,
                            HasDownloads = item.HasDownloads ? "Sim" : "Não",
                            HasIssues = item.HasIssues ? "Sim" : "Não",
                            HtmlUrl = item.HtmlUrl,
                            Language = item.Language,
                            Name = item.Name,
                            StargazersCount = item.StargazersCount,
                            UpdatedAt = item.UpdatedAt.DateTime,
                            Owner = item.Owner.Name
                        });
                    }
                }
                db.GitHub.AddRange(gitHubList);
                db.SaveChanges();


                gitHubs = db.GitHub.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}