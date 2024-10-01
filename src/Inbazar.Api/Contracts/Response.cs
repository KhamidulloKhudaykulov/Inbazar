namespace Inbazar.Api.Contracts;

public class Response
{
    public string? Code { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }
}
