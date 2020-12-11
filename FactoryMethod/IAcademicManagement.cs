using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicManagement.FactoryMethod
{
    interface IAcademicManagement<Model>
    {
        public void AddNew(Model model);
        public List<Model> ViewAll();
        public List<Model> SearchByName(string name);
        public Model SearchById(int id);
        public void EditModel(Model model);
        public void DeleteModel(int id);
    }
}
