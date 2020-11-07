using Store.Common.Entities;
using Store.Web.Data;
using Store.Web.Models;
using System.Threading.Tasks;

namespace Store.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        public async Task<ProductoEntity> ToProductoAsync(ProductoViewModel model, bool isNew)
        {
            return new ProductoEntity
            {
                Categoria = await _context.CategoriaEntities.FindAsync(model.CategoriaId),
                Nombre = model.Nombre,
                Id = model.Id,
                IsActive = model.IsActive,
                Precio = model.Precio,
            };
        }

        public ProductoViewModel ToProductViewModel(ProductoEntity product)
        {
            return new ProductoViewModel
            {
                Categorias = _combosHelper.GetComboCategorias(),
                Categoria = product.Categoria,
                CategoriaId = product.Categoria.Id,
                Id = product.Id,
                IsActive = product.IsActive,
                Nombre = product.Nombre,
                Precio = product.Precio,
            };
        }
    }
}