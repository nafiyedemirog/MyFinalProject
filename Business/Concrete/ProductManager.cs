using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using Business.BusinessAspects.Autofac;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        //hata almazsa başarılıdır
        public IResult Add(Product product)
        {
            ValidationTool.Validate(new ProductValidator(), product);
            //if (product.ProductName.Length<2)
            //{
            //    return new ErrorResult("Ürün ismi minimum 2 karakter olmalıdır.");
            //}
            //_productDal.Add(product);

            //if (product.ProductName.Length == 0)
            //{
            //    return new ErrorResult("Ürün ismi boş geçilemez");
            //}
            _productDal.Add(product);
            return new SuccessResult("Ürün eklendi.");
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult("Ürün silindi.");
        }

        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList());
        }

        public IDataResult<List<Product>> GetProductsByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId));
        }

        [SecuredOperation("read,admin")]
        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        public IDataResult<List<ProductWithCategoryDto>> GetProductsByCategoryName(string categoryName)
        {
            return new SuccessDataResult<List<ProductWithCategoryDto>>(_productDal.GetProductsWithCategoryName(categoryName));
        }

        public IDataResult<List<Product>> GetProductsByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IResult Update(Product product)
        {
            ValidationTool.Validate(new ProductValidator(), product);

            _productDal.Update(product);
            return new SuccessResult("Ürün güncellendi.");
        }
    }
}
