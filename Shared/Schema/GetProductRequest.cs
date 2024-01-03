using System.Runtime.Serialization;

namespace Shared.Models;

[DataContract]
public class GetProductRequest
{
    [DataMember(Order = 1)] 
    public Guid Id { get; set; }
}

[DataContract]
public class GetProductReply
{
    [DataMember(Order = 1)] 
    public Guid Id { get; set; }
    [DataMember(Order = 2)] 
    public int Count { get; set; }
    [DataMember(Order = 3)] 
    public decimal Price { get; set; }
}