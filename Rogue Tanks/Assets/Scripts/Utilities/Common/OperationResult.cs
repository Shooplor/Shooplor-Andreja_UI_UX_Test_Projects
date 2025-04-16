using System;

public class OperationResult<T>
{
    private T result = default(T);
    private bool isSuccess = false;
    private string errorMessage = string.Empty;

    private OperationResult(T result, bool isSuccess, string errorMessage)
    {
        this.result = result;
        this.isSuccess = isSuccess;
        this.errorMessage = errorMessage;
    }

    public static OperationResult<T> CreateSuccess(T result) => new OperationResult<T>(result, true, string.Empty);
    public static OperationResult<T> CreateError(string errorMessage) => new OperationResult<T>(default(T), false, errorMessage);

    public OperationResult<T> OnSuccess(Action<T> onSuccess)
    {
        if (isSuccess)
            onSuccess(result);
        return this;
    }

    public OperationResult<T> OnError(Action<string> onError)
    {
        if (!isSuccess)
            onError(errorMessage);
        return this;
    }
}
