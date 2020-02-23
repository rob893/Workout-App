using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class EquipmentRepository : Repository<Equipment>
    {
        public EquipmentRepository(DataContext context) : base(context) { }

        public async Task<Equipment> GetSingleEquipmentAsync(int id)
        {
            return await context.Equipment.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Equipment> GetSingleEquipmentDetailedAsync(int id)
        {
            return await context.Equipment
                .Include(e => e.Exercises).ThenInclude(e => e.Exercise)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<PagedList<Equipment>> GetEquipmentAsync(EquipmentSearchParams searchParams)
        {
            IQueryable<Equipment> query = context.Equipment;

            return await PagedList<Equipment>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        public async Task<PagedList<Equipment>> GetEquipmentDetailedAsync(EquipmentSearchParams searchParams)
        {
            IQueryable<Equipment> query = context.Equipment
                .Include(e => e.Exercises).ThenInclude(e => e.Exercise);

            return await PagedList<Equipment>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }
    }
}