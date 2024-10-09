using FluentValidation.Results;

namespace Cliente.Web.Api.Transversal.Modelos;

public class ResponseGeneric<T>
{

    public T? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public IEnumerable<ValidationFailure>? Errors { get; set; }  //ValidationFailure es un objeto propio de fluent validation
}
