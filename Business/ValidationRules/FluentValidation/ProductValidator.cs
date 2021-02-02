using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).MinimumLength(2).WithMessage("Ürün ismi minimum 2 karakter olmalıdır.");
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(0).WithMessage("Ürün fiyatı 0'a eşit veya büyük olmalıdır.");
            RuleFor(p => p.UnitPrice).GreaterThan(0).When(p => p.CategoryId == 1).WithMessage("İçecek kategorisinde ücretsiz ürün tanımlanamaz.");
        }
    }
}