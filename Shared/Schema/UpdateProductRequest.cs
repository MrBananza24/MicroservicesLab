using System.Runtime.Serialization;
using ProtoBuf.Grpc;

namespace Shared.Models;

[DataContract]
public class UpdateProductRequest
{
    [DataMember(Order = 1)] 
    public Guid Id { get; set; }
    [DataMember(Order = 2)] 
    public int NewCount { get; set; }
}

[DataContract]
public class UpdateProductReply
{
    [DataMember(Order = 1)] 
    public Guid Id { get; set; }
    [DataMember(Order = 2)]
    public int NewCount { get; set; }
}