using Store.Common.Entities;
using Store.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Helpers
{
    public interface IConverterHelper
    {

        Task<ProductoEntity> ToProductoAsync(ProductoViewModel model, bool isNew);

        ProductoViewModel ToProductViewModel(ProductoEntity product);

    }
}
