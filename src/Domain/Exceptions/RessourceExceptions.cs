using System;

namespace Domain.Exceptions
{
    public class RessourceAlreadyExistsException : Exception
    {
        public Guid RessourceId { get; }

        public RessourceAlreadyExistsException(Guid ressourceId)
            : base($"The ressource with ID {ressourceId} already exists.")
        {
            RessourceId = ressourceId;
        }
    }

    public class RessourceNotFoundException : Exception
    {
        public Guid RessourceId { get; }

        public RessourceNotFoundException(Guid ressourceId)
            : base($"The ressource with ID {ressourceId} was not found.")
        {
            RessourceId = ressourceId;
        }
    }

    public class RessourceSaveException : Exception
    {
        public RessourceSaveException()
            : base("An error occurred while saving or updating the ressource.")
        {
        }
    }
}
