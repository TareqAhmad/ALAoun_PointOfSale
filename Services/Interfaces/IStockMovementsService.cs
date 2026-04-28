using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces 
    
{
    public interface IStockMovementsService
    {

        public List<StockMovementsViewModel> GetAllStockMovements(int companyId, int branchId);

        public StockMovementsViewModel GetStockMovementsById(int companyId, int branchId, int id);

        public bool AddStockMovements(ClsStockMovements stockMovement);

        public bool EditStockMovements(ClsStockMovements stockMovement);

        public bool DeleteStockMovements(ClsStockMovements stockMovement);


    }
}