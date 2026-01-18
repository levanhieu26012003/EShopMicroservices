using MediatR;
namespace BuildingBlocks.CQRS;
public interface IQueryHandler<in TQuery, TResponse> // nhánh query dùng để đọc
    : IRequestHandler<TQuery, TResponse> // kế thừa để implemneted Handle
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
}
