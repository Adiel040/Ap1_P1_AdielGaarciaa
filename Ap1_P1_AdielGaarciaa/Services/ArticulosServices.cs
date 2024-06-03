using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ap1_P1_AdielGaarciaa.DAL;
using Ap1_P1_AdielGaarciaa.Models;
using System.Linq;

namespace Ap1_P1_AdielGaarciaa.Services
{
    public class ArticulosServices
    {
        private readonly Contexto _context;

        public ArticulosServices(Contexto contexto)
        {
            _context = contexto;
        }

        public async Task<bool> Existe(int articuloId)
        {
            return await _context.Articulos
                .AnyAsync(a => a.ArticuloId == articuloId);
        }

        private async Task<bool> Insertar(Articulos articulo)
        {
            _context.Articulos.Add(articulo);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Articulos articulo)
        {
            var articuloOriginal = await _context.Articulos.AsNoTracking().FirstOrDefaultAsync(a => a.ArticuloId == articulo.ArticuloId);
            if (articuloOriginal != null)
            {
                _context.Update(articulo);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> Guardar(Articulos articulo)
        {
            if (await ExisteDescripcion(articulo.Descripcion, articulo.ArticuloId))
            {
                return false;
            }

            if (!await Existe(articulo.ArticuloId))
            {
                return await Insertar(articulo);
            }
            else
            {
                return await Modificar(articulo);
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.ArticuloId == id);
            if (articulo == null)
            {
                return false;
            }

            _context.Articulos.Remove(articulo);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Articulos?> Buscar(int id)
        {
            return await _context.Articulos
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.ArticuloId == id);
        }

        public List<Articulos> Listar(Expression<Func<Articulos, bool>> criterio)
        {
            return _context.Articulos
                .AsNoTracking()
                .Where(criterio)
                .ToList();
        }

        public async Task<bool> ExisteDescripcion(string descripcion, int? idArticulo = null)
        {
            if (idArticulo.HasValue)
            {
                return await _context.Articulos.AnyAsync(a => a.Descripcion == descripcion && a.ArticuloId != idArticulo);
            }
            else
            {
                return await _context.Articulos.AnyAsync(a => a.Descripcion == descripcion);
            }
        }
    }
}
