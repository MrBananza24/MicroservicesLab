using Shared.Models;
using ProtoBuf.Grpc;
using System.ServiceModel;

namespace Shared.Interfaces;

[ServiceContract]
public interface IInventoryGrpcService
{
    [OperationContract]
    public Task<GetProductReply> GetProductAsync(GetProductRequest request, CallContext context = default);

    [OperationContract]
    public Task<UpdateProductReply> UpdateProductCountAsync(UpdateProductRequest request, CallContext context = default);
}