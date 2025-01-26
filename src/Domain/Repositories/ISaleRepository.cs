using Domain.Entities.Sales;

namespace Domain.Repositories
{
    public interface ISaleRepository
    {
        public void Add(Sale sale);
        public void Update(Sale sale);
        public void DeleteById(Guid id);
        public Sale GetById(Guid id);
        public List<Sale> GetAll();
    }
}
