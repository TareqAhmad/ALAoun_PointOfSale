using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces
{
      public interface ICategoriesService
      {
              
            public List<ClsCategories> GetAllCategories(int companyId ,int branchId); 

            public ClsCategories GetCategoriesById(int companyId ,int branchId,int categoryId);

            public bool AddCategory(); 

            public bool EditCategory(ClsCategories Category); 

            public bool DeleteCategory(ClsCategories Category); 


      }
}