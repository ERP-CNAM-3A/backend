using Domain.Entities.Sales;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Helpers;
using System.IO;

namespace Infrastructure.Repositories
{
    internal sealed class SaleRepository : ISaleRepository
    {
        private const string FileName = "sales.json";

        public void Add(Sale sale)
        {
            try
            {
                var sales = JsonFileHandler.ReadFromFile<Sale>(FileName);

                if (sales.Any(s => s.Id == sale.Id))
                {
                    throw new SaleAlreadyExistsException(sale.Id);
                }

                sales.Add(sale);
                JsonFileHandler.WriteToFile(FileName, sales);
            }
            catch (Exception)
            {
                throw new SaleSaveException();
            }
        }

        public void DeleteById(Guid id)
        {
            try
            {
                var sales = JsonFileHandler.ReadFromFile<Sale>(FileName);
                var saleToRemove = sales.FirstOrDefault(s => s.Id == id);

                if (saleToRemove == null)
                {
                    throw new SaleNotFoundException(id);
                }

                sales.Remove(saleToRemove);
                JsonFileHandler.WriteToFile(FileName, sales);
            }
            catch (SaleNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new SaleSaveException();
            }
        }

        public List<Sale> GetAll()
        {
            try
            {
                return JsonFileHandler.ReadFromFile<Sale>(FileName);
            }
            catch (Exception)
            {
                throw new SaleSaveException();
            }
        }

        public Sale GetById(Guid id)
        {
            try
            {
                var sales = JsonFileHandler.ReadFromFile<Sale>(FileName);
                var sale = sales.FirstOrDefault(s => s.Id == id);

                if (sale == null)
                {
                    throw new SaleNotFoundException(id);
                }

                return sale;
            }
            catch (SaleNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new SaleSaveException();
            }
        }

        public void Update(Sale sale)
        {
            try
            {
                var sales = JsonFileHandler.ReadFromFile<Sale>(FileName);
                var index = sales.FindIndex(s => s.Id == sale.Id);

                if (index == -1)
                {
                    throw new SaleNotFoundException(sale.Id);
                }

                sales[index] = sale;
                JsonFileHandler.WriteToFile(FileName, sales);
            }
            catch (SaleNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new SaleSaveException();
            }
        }
    }
}
