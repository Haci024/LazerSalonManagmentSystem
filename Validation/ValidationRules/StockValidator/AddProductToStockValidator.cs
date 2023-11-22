using DTO.DTOS.StockDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.StockValidator
{
    public class AddProductToStockValidator:AbstractValidator<AddProductToStockDTO>
    {
        public AddProductToStockValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Məhsul adı boş keçilə bilməz!");
            RuleFor(x => x.ProductCount).NotEmpty().WithMessage("Məhsul sayı 0 ola bilməz");
            RuleFor(x=>x.SellingPrice).NotEmpty().WithMessage("Satış qiymətini qeyd edin!");
            RuleFor(x=>x.BuyingPrice).NotEmpty().WithMessage("Maya dəyərini daxil edin!");
        }
    }
}
