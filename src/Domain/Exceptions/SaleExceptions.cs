using System;

namespace Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a sale with a given ID already exists.
    /// </summary>
    public class SaleAlreadyExistsException : Exception
    {
        public SaleAlreadyExistsException(Guid saleId)
            : base($"A sale with ID {saleId} already exists.")
        {
        }
    }

    /// <summary>
    /// Exception thrown when no sale is found for a given ID.
    /// </summary>
    public class SaleNotFoundException : Exception
    {
        public SaleNotFoundException(Guid saleId)
            : base($"No sale found with ID {saleId}.")
        {
        }
    }

    /// <summary>
    /// Exception thrown when there is an error saving the sale.
    /// </summary>
    public class SaleSaveException : Exception
    {
        public SaleSaveException()
            : base("An error occurred while saving the sale.")
        {
        }
    }
}
