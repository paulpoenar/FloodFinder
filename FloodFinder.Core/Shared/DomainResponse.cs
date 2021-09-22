namespace FloodFinder.Core.Shared
{
  public class DomainResponse
  {
    public bool Success { get; private set; }
    public string Error { get; private set; }

    public static DomainResponse Succeeded()
    {
      return new DomainResponse() { Success = true };
    }

    public static DomainResponse<T> Succeeded<T>(T data)
    {
      return new DomainResponse<T>() { Success = true, Data = data };
    }

    public static DomainResponse Failed(string error)
    {
      return new DomainResponse() { Success = false, Error = error };
    }

    public static DomainResponse<T> Failed<T>(string error, T data)
    {
      return new DomainResponse<T>() { Success = false, Error = error, Data = data };
    }

  }

  public sealed class DomainResponse<T> : DomainResponse
  {
    public T Data { get; set; }
  }
}