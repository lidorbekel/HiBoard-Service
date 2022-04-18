namespace HiBoard.Domain;

[Serializable]
public class HiBoardResponse<T>
{
    public T? Data { get; set; }

    public HiBoardResponse(T? data)
    {
        Data = data;
    }
}