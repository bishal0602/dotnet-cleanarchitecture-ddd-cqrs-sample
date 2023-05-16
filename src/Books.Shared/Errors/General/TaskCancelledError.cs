namespace Books.Shared.Errors.General
{
    public class TaskCancelledError : BadRequestError
    {
        public TaskCancelledError(
            string errorType = nameof(TaskCancelledError),
            string? errorMessage = null) : base(nameof(TaskCancelledError), errorMessage: errorMessage)
        {

        }
    }
}
