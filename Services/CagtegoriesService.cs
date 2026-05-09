using Microsoft.Data.SqlClient;
using System.Data;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;
using ALAoun_Pos.Data;


namespace ALAoun_Pos.Services
{
    
    public class CategoriesService : ICategoriesService
    {
        

        private readonly DbHelper? _dbHelper; 
       

        public CategoriesService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper; 
        }

           
            public List<ClsCategories> GetAllCategories(int companyId ,int branchId)
            {
                List<ClsCategories> categories = new List<ClsCategories>(); 

                string query = @"SELECT CategoryId,CategoryName
                                FROM Categories
                                WHERE CompanyId = @companyId
                                AND BranchId = @branchId; "; 

                 SqlParameter[] parameters =
                {
                    new SqlParameter("@companyId",companyId),
                    new SqlParameter("@branchId",branchId)
                };
                  
                  DataTable dt = _dbHelper.Select(query,parameters); 

                  foreach(DataRow row in dt.Rows)
                {
                            var category = new ClsCategories
                            {
                                CategoryId = Convert.ToInt32(row[0]),
                                CategoryName = row[1].ToString()
                            }; 

                            categories.Add(category);
                }

                return categories; 
            } 

          
            public ClsCategories GetCategoriesById(int companyId ,int branchId,int categoryId)
            {
                ClsCategories category = null;

                string query = @"SELECT CategoryId,CategoryName
                                    FROM Categories
                                    WHERE CompanyId = @companyId
                                    AND BranchId = @branchId
                                    AND CategoryId = @categoryId; ";

                 SqlParameter[] parameters =
                {
                    new SqlParameter("@companyId",companyId),
                    new SqlParameter("@branchId",branchId),
                    new SqlParameter("@categoryId",categoryId)
                };

                 DataTable dt = _dbHelper.Select(query, parameters);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        category = new ClsCategories
                        {
                            CategoryId = Convert.ToInt32(row[0]),
                            CategoryName = row[1].ToString()
                        };
                    }

                    return category;
            }
              
            

            public bool AddCategory(CategoryDto categoryDto)
            {
                string query = @"INSERT INTO Categories (CategoryName,CompanyId,BranchId)
                                VALUES (@categoryName,@companyId,@branchId);";

                 SqlParameter[] parameters =
                {
                    new SqlParameter("@categoryName",categoryDto.categoryName),
                    new SqlParameter("@companyId",categoryDto.companyId),
                    new SqlParameter("@branchId",categoryDto.branchId)
                };

                int rowsAffected = _dbHelper.Execute(query, parameters);

                return rowsAffected > 0;
            }

            public bool EditCategory(ClsCategories Category)
            {
                return false; 
            }

            public bool DeleteCategory(ClsCategories Category)
            {
                return false; 
            }

    }
    
 }