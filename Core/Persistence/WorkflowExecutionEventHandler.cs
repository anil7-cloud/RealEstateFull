namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionEventHandler
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionEventId { get; set; }

    public string HandlerName { get; set; } = string.Empty;

    public string HandlerType { get; set; } = string.Empty;

    public bool IsEnabled { get; set; } = true;

    public int ExecutionOrder { get; set; }

    public bool IsSuccessful { get; set; }

    public DateTime? ExecutedAt { get; set; }

    public string? ResultMessage { get; set; }

    public WorkflowExecutionEvent? WorkflowExecutionEvent { get; set; }
}
