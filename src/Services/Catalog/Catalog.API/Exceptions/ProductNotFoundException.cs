using BuildingBlocks.Exceptions;

namespace Catalog.API.Exceptions;

public class ProductNotFoundException : NotFoundException // kế thừa nhằm nhận đầy đủ thuôc tính của 1 Exception
{
    public ProductNotFoundException(Guid Id) : base("Product", Id) { } // truyền giá trị cho constructer của lớp cha
}
