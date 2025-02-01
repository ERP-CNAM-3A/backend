namespace Domain.Exceptions
{
    public class ProjectAlreadyExistsException : Exception
    {
        public Guid ProjectId { get; }

        public ProjectAlreadyExistsException(Guid projectId)
            : base($"Project with ID {projectId} already exists.")
        {
            ProjectId = projectId;
        }
    }

    public class ProjectNotFoundException : Exception
    {
        public Guid ProjectId { get; }

        public ProjectNotFoundException(Guid projectId)
            : base($"Project with ID {projectId} was not found.")
        {
            ProjectId = projectId;
        }
    }

    public class ProjectSaveException : Exception
    {
        public ProjectSaveException()
            : base("An error occurred while saving the project.")
        {
        }

        public ProjectSaveException(string message)
            : base(message)
        {
        }

        public ProjectSaveException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class ProjectUpdateException : Exception
    {
        public Guid ProjectId { get; }

        public ProjectUpdateException(Guid projectId)
            : base($"An error occurred while updating the project with ID {projectId}.")
        {
            ProjectId = projectId;
        }

        public ProjectUpdateException(string message)
            : base(message)
        {
        }

        public ProjectUpdateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class ProjectDeleteException : Exception
    {
        public Guid ProjectId { get; }

        public ProjectDeleteException(Guid projectId)
            : base($"An error occurred while deleting the project with ID {projectId}.")
        {
            ProjectId = projectId;
        }

        public ProjectDeleteException(string message)
            : base(message)
        {
        }

        public ProjectDeleteException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class ProjectUpdateWorkDaysException : Exception
    {
        public double RequestedWorkDays { get; }
        public double TotalDaysWorking { get; }

        public ProjectUpdateWorkDaysException(double requestedWorkDays, double totalDaysWorking)
            : base($"Cannot update project workDaysNeeded to {requestedWorkDays} because the total days working from resources is {totalDaysWorking}. First deassign some ressources")
        {
            RequestedWorkDays = requestedWorkDays;
            TotalDaysWorking = totalDaysWorking;
        }

        public ProjectUpdateWorkDaysException(string message)
            : base(message)
        {
        }

        public ProjectUpdateWorkDaysException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
