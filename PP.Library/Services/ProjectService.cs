using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PP.Library.Models;

namespace PP.Library.Services
{
    public class ProjectService
    {
        private List<Project> projects;
        public List<Project> Projects
        {
            get
            {
                return projects;
            }
        }

        private static ProjectService? instance;
        public static ProjectService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProjectService();
                }

                return instance;
            }
        }

        private ProjectService()
        {
            projects = new List<Project>();
        }
        public IEnumerable<Project> Search(string query)
        {
            return projects
                .Where(c => c.ShortName.ToUpper()
                    .Contains(query.ToUpper()));
        }

        public IEnumerable<Project> SearchByClientID(int ClientID)
        {
            return projects.Where(c => c.ClientID == ClientID);
                  
        }

        public Project? GetProject(int id)
        {
            return Projects.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Project project)
        {
            if (project != null) 
            {
                if (project.Id == 0)
                {
                    project.Id = LastId + 1;
                }
                projects.Add(project);
            }
            
        }

        public void DeleteProject(int id)
        {
            var projectToRemove = projects.FirstOrDefault(p => p.Id == id);
            if (projectToRemove != null)
            {
                projects.Remove(projectToRemove);
            }

        }

        public void UpdateProject(Project project)
        {
            var projectToUpdate = projects.FirstOrDefault(p => p.Id == project.Id);
            if (projectToUpdate != null)
            {
                projectToUpdate.ShortName = project.ShortName;
                projectToUpdate.OpenDate = project.OpenDate;
                projectToUpdate.CloseDate = project.CloseDate;
                projectToUpdate.LongName = project.LongName;
                projectToUpdate.isActive = project.isActive;
                projectToUpdate.ClientID = project.ClientID;
            }
        }

        private int LastId
        {
            get
            {
                return Projects.Any() ? Projects.Select(p => p.Id).Max() : 0;
            }
        }
    }
}
