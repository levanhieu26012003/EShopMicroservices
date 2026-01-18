namespace Catalog.API.Exceptions;

public class ProductNotFoundException : Exception // kế thừa nhằm nhận đầy đủ thuôc tính của 1 Exception
{
    public ProductNotFoundException() : base("Product not found!") { } // truyền giá trị cho constructer của lớp cha
}
