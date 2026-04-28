
using ALAoun_Pos.Data;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;

namespace ALAoun_Pos.Services
{
    public class StockMovementsService : IStockMovementsService
    {
        private readonly DbHelper _dbHelper;

        public StockMovementsService(DbHelper dbHelper)
            {
                _dbHelper = dbHelper;
        }

        public List<StockMovementsViewModel> GetAllStockMovements(int companyId, int branchId)
        {
            return new  List<StockMovementsViewModel>();
        }


         public StockMovementsViewModel GetStockMovementsById(int companyId, int branchId, int id)
        {
            return new StockMovementsViewModel();
        }

        public bool AddStockMovements(ClsStockMovements stockMovement)
        {
            throw new NotImplementedException();
        }

        public bool EditStockMovements(ClsStockMovements stockMovement)
        {
            throw new NotImplementedException();
        }

        public bool DeleteStockMovements(ClsStockMovements stockMovement)
        {
            throw new NotImplementedException();
        }
    }
}