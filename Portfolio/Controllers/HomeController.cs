using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portfolio.Data;
using Portfolio.Domain;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _appContext;
        private readonly IHostingEnvironment hostingEnvironment;
        public HomeController(ApplicationDbContext appContext,IHostingEnvironment environment)
        {
            _appContext = appContext;
            hostingEnvironment = environment;
        }
        public IActionResult Index()
        {
            List<HomeListViewModel> projects = new List<HomeListViewModel>();
            IEnumerable<Project> projectsFromDb = _appContext.Projects.ToArray();

            foreach (Project project in projectsFromDb)
            {
                projects.Add(new HomeListViewModel()
                {
                    Id = project.Id,
                    Titel = project.Titel,
                    Beschrijving = project.Beschrijving,
                    Foto = project.Foto,
                    Status = project.Status,
                    TagProjects = project.TagProjects
                });
            }
            return View(projects);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Create()
        {
            Tag[] tagsFromDb = _appContext.Tags.ToArray();
            List<HomeTagViewModel> tags = new List<HomeTagViewModel>();
            
            Status[] statusesFromDb = _appContext.Statuses.ToArray();
            List<SelectListItem> statuses = new List<SelectListItem>();

            foreach (var item in tagsFromDb)
            {
                tags.Add(new HomeTagViewModel()
                {
                    Id = item.Id,
                    Name = item.Naam
                });
            }
            foreach (var item in statusesFromDb)
            {
                statuses.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Naam
                });
            }

            HomeCreateViewModel model = new HomeCreateViewModel()
            {
                Tags = tags,
                Statuses = statuses
            };

            return View(model);
        }
       

        [HttpPost]
        public async Task<IActionResult> Create(HomeCreateViewModel model)
        {
            if (!TryValidateModel(model))
            {
                return View(model);
            }
            

            var project = new Project()
            {
                Titel = model.Titel,
                Beschrijving = model.Beschrijving,
                StatusId = int.Parse(model.SelectedStatus),
                
            };
            using (var memoryStream = new MemoryStream())
            {
                await model.Foto.CopyToAsync(memoryStream);
                project.Foto = memoryStream.ToArray();
            }


                _appContext.Projects.Add(project);
            _appContext.SaveChanges();
            foreach (var item in model.Tags)
            {
                if (item.Checked)
                {
                    var tagProject = new TagProject()
                    {
                        ProjectId = project.Id,
                        TagId = item.Id
                    };
                    _appContext.TagProjects.Add(tagProject);
            _appContext.SaveChanges();
                }
            }
           
           

            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            Project projectFromDb = _appContext.Projects.FirstOrDefault(x => x.Id == id);
            Status status = _appContext.Statuses.FirstOrDefault(x => x.Id == projectFromDb.StatusId);
            
            List<HomeTagViewModel> tags= new List<HomeTagViewModel>();
            var selectedTags = _appContext.TagProjects.ToArray();
            foreach (var item in selectedTags)
            {
                if (item.ProjectId == id)
                {
                    Tag tag = _appContext.Tags.FirstOrDefault(x => x.Id == item.TagId);
                    HomeTagViewModel tagViewModel = new HomeTagViewModel()
                    {
                        Name = tag.Naam
                        
                    };
                    tags.Add(tagViewModel);
                }
            }
            

            HomeDetailViewModel model = new HomeDetailViewModel()
            {

                Titel = projectFromDb.Titel,
                Beschrijving = projectFromDb.Beschrijving,
                Foto = projectFromDb.Foto,
                Status = status.Naam,
                Tags = tags,
                
            };
            return View(model);
            
        }
        public IActionResult Edit(int id)
        {
            Tag[] tagsFromDb = _appContext.Tags.ToArray();
            List<HomeTagViewModel> tags = new List<HomeTagViewModel>();
            Status[] statusesFromDb = _appContext.Statuses.ToArray();
            List<SelectListItem> statuses = new List<SelectListItem>();
            foreach (var item in statusesFromDb)
            {   
                statuses.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Naam
                });
            }
            foreach (var item in tagsFromDb)
            {
                tags.Add(new HomeTagViewModel()
                {
                    Id = item.Id,
                    Name = item.Naam,
                    
                    
                });
            }

          
            Project projectFromDb = _appContext.Projects.FirstOrDefault(x => x.Id == id);
            HomeEditViewModel vm = new HomeEditViewModel()
            {
                Titel = projectFromDb.Titel,
                Beschrijving = projectFromDb.Beschrijving,
                Foto = projectFromDb.Foto,
                Tags = tags,
                Statuses = statuses
               
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult edit(int id, HomeEditViewModel model)
        {
            if (!TryValidateModel(model))
            {
                return View(model);
            }
            Tag[] tagsFromDb = _appContext.Tags.ToArray();
            List<HomeTagViewModel> tags = new List<HomeTagViewModel>();
            Project projectToEdit = new Project()
            {
                Id = id,
                Titel = model.Titel,
                Beschrijving = model.Beschrijving,
                Foto = model.Foto,
                StatusId = int.Parse(model.SelectedStatus),
               
                
            };
            _appContext.Projects.Update(projectToEdit);
            _appContext.SaveChanges();

            foreach (var item in model.Tags)
            {
                if (item.Checked)
                {

                var tagProject = new TagProject()
                {
                   
                    ProjectId = projectToEdit.Id,
                    TagId = item.Id
                };
                _appContext.TagProjects.Update(tagProject);
                _appContext.SaveChanges();
                }
            }

            return RedirectToAction("Details",new { Id = id });
        }
        
        public IActionResult Delete(int id)
        {
            Project projectToDel = _appContext.Projects.FirstOrDefault(x => x.Id == id);
            HomeDeleteViewModel model = new HomeDeleteViewModel()
            {
                Id = projectToDel.Id,
                Titel = projectToDel.Titel
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            Project projectToDel = _appContext.Projects.FirstOrDefault(x => x.Id == id);
            _appContext.Projects.Remove(projectToDel);
            _appContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
