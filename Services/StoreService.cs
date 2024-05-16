using Inventarios.Server.AspNet.Data;
using Inventarios.Server.AspNet.Dto_s;
using Microsoft.EntityFrameworkCore;
using SolutionPal.RazorPages.Models;

namespace Inventarios.Server.AspNet.Services
{
    public class StoreService
    {

        private readonly ApplicationDbContext _context;
        

        public StoreService(ApplicationDbContext context)
        {
            _context = context;
        
        }
        public async Task<List<Store>> getStore()
        {
            List<Store> list = new List<Store>();


            list = await _context.stores.ToListAsync();

            return list;
        }
        public async Task<List<Store>> getStores(int pageNumber, int pageSize)
        {
            List<Store> list=new List<Store>();


               list = await _context.stores
                   .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                             .ToListAsync();

            return list;
        }
      /*  private async Task<List<Store>> getStoresByDate(DateTime startDate, DateTime endDate)
        {
            List<Store> list = new List<Store>();

            list = await _context.stores.Where
                (s => s.CreationDate >= startDate && s.CreationDate <= endDate).ToListAsync();
            return list;
        }*/
        public async Task<List<Store>> getStoresByBatchOrProductName(string bacth, int pageNumber,int pageSize)
        {
            List<Store> list = new List<Store>();

            list = await _context.stores.Where
                (s => s.Batch.Contains(bacth) || s.ProductName.Contains( bacth))
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return list;
        }
        /*
        private async Task<List<Store>> getStoresByProductName(string name)
        {
            List<Store> list = new List<Store>();

            list = await _context.stores.Where
                (s => s.ProductName==name).ToListAsync();
            return list;
        }*/

        public async Task<string> CreateStore(Store store)
        {
            try
            {
                await _context.stores.AddAsync(store);
                await _context.SaveChangesAsync();
                return "1";
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }
        public async Task<string> UpdateStore(StoreUpdateDto store)
        {
            try
            {
                string message;
                
               
                switch (store.opt)
                {
                    case "none":
                        message =await Operation1(store);
                        break;
                    case "add":
                        message =await OperationAdd(store);
                        break;
                    case "substract":
                        message = await OperationSubstract(store);        
                        break;
                    default:
                        message = "Operation not found";
                        break;
                }
                return message;
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }

        private async Task<string> Operation1(StoreUpdateDto store)
        {
            string message;
            var data =await _context.stores.FirstOrDefaultAsync(x => x.Id == store.Id); ;
            if (data != null)//if exists record
            {
                data.Id=store.Id;
                data.ProductName = store.ProductName;
                data.Quantity = data.Quantity;
                data.userIdCreation = data.userIdCreation;
                data.CreationDate = data.CreationDate;
                data.userIdModification = store.userIdModification;
                data.ModificationDate = DateTime.Now;
                data.Comments = "Update Product : " + store.Comments;
                
                _context.stores.Update(data);
                _context.SaveChanges();
                 message = "1";
            }
            else
            {
                message = "Concurrency Exception, record no found!";
            }
            return message;
        }
        private async Task<string> OperationAdd(StoreUpdateDto store)
        {
            string message;
            var data = await _context.stores.FirstOrDefaultAsync(x => x.Id == store.Id); ;
            if (data != null)//if exists record
            {
                data.Id = store.Id;
                data.ProductName = store.ProductName;
                data.Quantity = data.Quantity+store.newQuantity;
                data.userIdCreation = data.userIdCreation;
                data.CreationDate = data.CreationDate;
                data.userIdModification = store.userIdModification;
                data.ModificationDate = DateTime.Now;
                data.Comments = "Update product : +"+store.newQuantity+store.Comments;

                _context.stores.Update(data);
                _context.SaveChanges();
                message = "1";
            }
            else
            {
                message = "Concurrency Exception, record no found!";
            }

            return message;

        }
        private async Task<string> OperationSubstract(StoreUpdateDto store)
        {
            string message; 
            var data =await _context.stores.FirstOrDefaultAsync(x => x.Id == store.Id); ;
            if (data != null)//if exists record
            {
                data.Id = store.Id;
                data.ProductName = store.ProductName;
                data.Quantity = data.Quantity - store.newQuantity;
                data.userIdCreation = data.userIdCreation;
                data.CreationDate = data.CreationDate;
                data.userIdModification = store.userIdModification;
                data.ModificationDate = DateTime.Now;
                data.Comments = "Update product : " + store.newQuantity + store.Comments;

               
              
                if (data.Quantity < 0)
                {
                    message = "Invalid Operation";
                }
                else
                {

                    _context.stores.Update(data);
                    _context.SaveChanges();
                    message = "1";
                }
            }
            else
            {
                message = "Concurrency Exception, record no found!";
            }
            return message;
        }
        public async Task<Store> GetStoreById(int id)
        {
            return await _context.stores.FindAsync(id);
        }
    }
}
