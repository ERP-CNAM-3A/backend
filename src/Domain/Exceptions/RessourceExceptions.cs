namespace Domain.Exceptions
{
    public class RessourceNotFoundException : Exception
    {
        public int RessourceId { get; }

        public RessourceNotFoundException(int ressourceId)
            : base($"The ressource with ID {ressourceId} was not found for this project.")
        {
            RessourceId = ressourceId;
        }
    }

    public class RessourceNotAssignedException : Exception
    {
        public int RessourceId { get; }
        public RessourceNotAssignedException(int ressourceId)
            : base($"The ressource with ID {ressourceId} is not assigned during this period.")
        {
            RessourceId = ressourceId;
        }
    }

    public class RessourceNotInProjectDateRangeException : Exception
    {
        public int RessourceId { get; }
        public DateTime ProjectStartDate { get; }
        public DateTime ProjectEndDate { get; }

        public RessourceNotInProjectDateRangeException(int ressourceId, DateTime projectStartDate, DateTime projectEndDate)
            : base($"The ressource with ID {ressourceId} is not in the project date range ({projectStartDate} to {projectEndDate}).")
        {
            RessourceId = ressourceId;
            ProjectStartDate = projectStartDate;
            ProjectEndDate = projectEndDate;
        }
    }

    public class RessourceExternalAPIException : Exception
    {
        public int RessourceId { get; }
        public RessourceExternalAPIException(int ressourceId)
            : base($"Extenal API issue with {ressourceId}")
        {
            RessourceId = ressourceId;
        }
    }

    public class RessourceAlreadyAssignedException : Exception
    {
        public int RessourceId { get; }
        public RessourceAlreadyAssignedException(int ressourceId)
            : base($"The ressource with ID {ressourceId} is already assigned during this period.")
        {
            RessourceId = ressourceId;
        }
    }

    public class RessourceAssignedPriorToSaleDateException : Exception
    {
        public int RessourceId { get; }
        public RessourceAssignedPriorToSaleDateException(int ressourceId)
            : base($"The ressource with ID {ressourceId} is assigned prior to the sale date.")
        {
            RessourceId = ressourceId;
        }
    }
}
