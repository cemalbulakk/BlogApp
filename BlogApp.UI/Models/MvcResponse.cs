namespace BlogApp.UI.Models;


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Data<T>
{
    public int from { get; set; }
    public int index { get; set; }
    public int size { get; set; }
    public int count { get; set; }
    public int pages { get; set; }
    public List<T> items { get; set; }
    public bool hasPrevious { get; set; }
    public bool hasNext { get; set; }
}

public class MvcResponse<T>
{
    public Data<T>? data { get; set; }
    public bool isSuccessful { get; set; }
    public object errors { get; set; }
}

public class TokenResponse<T>
{
    public T? data { get; set; }
    public bool isSuccessful { get; set; }
    public object errors { get; set; }
}
