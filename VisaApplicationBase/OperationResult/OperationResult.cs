
using System.ComponentModel.DataAnnotations;

namespace VisaApplicationBase.OperationResult;
public class OperationResult<TEnumResult> where TEnumResult : struct, IConvertible
{
    #region Properties And Constructors

    public Exception Exception { get; protected set; }
    public TEnumResult EnumResult { get; set; }
    public ICollection<ValidationResult> ValidationResults { get; set; }
    public string ErrorMessages
    {
        get
        {
            if (ValidationResults != null)
            {
                return string.Join(", ", ValidationResults);
            }
            return string.Empty;
        }
    }
    public bool IsFailure
    {
        get
        {
            return ValidationResults.Any() || !string.IsNullOrEmpty(ErrorMessages) || !(Exception is null);
        }
    }
    public bool IsSuccess => !IsFailure;

    public OperationResult(TEnumResult enumResult = default, Exception exception = null)
    {
        EnumResult = enumResult;
        Exception = exception;

        if (Exception != null)
        {
            AddError(exception.Message);
        }
    }
    #endregion

    #region Methods

    public virtual OperationResult<TEnumResult> AddError(string errorMessage)
    {
        if (ValidationResults is null)
        {
            ValidationResults = new List<ValidationResult>();
        }
        ValidationResults.Add(new ValidationResult(errorMessage));
        return this;
    }
    #endregion
}

public class OperationResult<TEnumResult, TEntity> : OperationResult<TEnumResult>
    where TEnumResult : struct, IConvertible
{
    #region Properties And Constructors

    public TEntity Result { get; set; }
    public TEnumResult EnumResult { get; set; }
    public OperationResult(TEnumResult enumResult = default, TEntity entity = default, Exception exception = null)
        : base(enumResult, exception)
    {
        Result = entity;
        EnumResult = enumResult;
    }

    public new OperationResult<TEnumResult, TEntity> AddError(string errorMessage)
    {
        base.AddError(errorMessage);
        return this;
    }
    #endregion
}

public static class OperationResultExtensions
{
    public static OperationResult<TEnumResult> UpdateStatusResult<TEnumResult>
        (this OperationResult<TEnumResult> operation, TEnumResult enumResult) where TEnumResult : struct, IConvertible
    {
        operation.EnumResult = enumResult;
        return operation;
    }

    public static OperationResult<TEnumResult> WithNoErrors<TEnumResult>
            (this OperationResult<TEnumResult> operation) where TEnumResult : struct, IConvertible
    {
        if (!(operation.ValidationResults is null))
        {
            operation.ValidationResults.Clear();
        }
        return operation;
    }

    public static OperationResult<TEnumResult> WithException<TEnumResult>
            (this OperationResult<TEnumResult> operation, Exception exception) where TEnumResult : struct, IConvertible
    {
        if (exception != null)
        {
            operation.AddError(exception.Message);
        }

        return operation;
    }

    public static OperationResult<TEnumResult, TEntity> UpdateStatusResult<TEnumResult, TEntity>
            (this OperationResult<TEnumResult, TEntity> operation, TEnumResult enumResult) where TEnumResult : struct, IConvertible
    {
        operation.EnumResult = enumResult;
        return operation;
    }
    public static OperationResult<TEnumResult, TEntity> UpdateResultData<TEnumResult, TEntity>
            (this OperationResult<TEnumResult, TEntity> operation, TEntity result) where TEnumResult : struct, IConvertible
    {
        operation.Result = result;
        return operation;
    }

    public static OperationResult<TEnumResult, TEntity> WithNoErrors<TEnumResult, TEntity>
            (this OperationResult<TEnumResult, TEntity> operation) where TEnumResult : struct, IConvertible
    {
        if (!(operation.ValidationResults is null))
        {
            operation.ValidationResults.Clear();
        }
        return operation;
    }


    public static OperationResult<TEnumResult, TEntity> WithException<TEnumResult, TEntity>
            (this OperationResult<TEnumResult, TEntity> operation, Exception exception) where TEnumResult : struct, IConvertible
    {
        if (exception != null)
        {
            operation.AddError(exception.Message);
        }
        return operation;
    }

}
