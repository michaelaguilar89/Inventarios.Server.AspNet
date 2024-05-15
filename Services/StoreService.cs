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

        private async Task<List<Store>> getStores()
        {
            List<Store> list=new List<Store>();
           
               
            list = await _context.stores.ToListAsync();
                 
            return list;
        }
        private async Task<List<Store>> getStoresByDate(DateTime startDate, DateTime endDate)
        {
            List<Store> list = new List<Store>();

            list = await _context.stores.Where
                (s => s.CreationDate >= startDate && s.CreationDate <= endDate).ToListAsync();
            return list;
        }
        private async Task<List<Store>> getStoresByBatch(string bacth)
        {
            List<Store> list = new List<Store>();

            list = await _context.stores.Where
                (s => s.Batch == bacth).ToListAsync();
            return list;
        }

        private async Task<List<Store>> getStoresByProductName(string name)
        {
            List<Store> list = new List<Store>();

            list = await _context.stores.Where
                (s => s.ProductName==name).ToListAsync();
            return list;
        }

        private async Task<string> CreateStore(Store store)
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
        private async Task<string> UpdateStore(Store store)
        {
            try
            {
                var data = await _context.stores.FirstOrDefaultAsync(x => x.Id == store.Id); ;
                if (data!=null)//if exists record
                {
                    _context.stores.Update(store);
                    await _context.SaveChangesAsync();
                    return "1";
                }
                return "Concurrency Exception, record no found!";
                
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }
        private async Task<Store> GetStoreById(int id)
        {
            return await _context.stores.FindAsync(id);
        }
    }
}
