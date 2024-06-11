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

        private async Task<bool> Insertar(Articulos articulos)
        {
            _context.Articulos.Add(articulos);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Articulos articulos)
        {
            var articuloOriginal = await _context.Articulos.AsNoTracking().FirstOrDefaultAsync(a => a.ArticuloId == articulos.ArticuloId);
            if (articuloOriginal != null)
            {
                _context.Update(articulos);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> Guardar(Articulos articulos)
        {
            if (await ExisteDescripcion(articulos.Descripcion, articulos.ArticuloId))
            {
                return false;
            }

            if (!await Existe(articulos.ArticuloId))
            {
                return await Insertar(articulos);
            }
            else
            {
                return await Modificar(articulos);
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            var articulos = await _context.Articulos.FirstOrDefaultAsync(a => a.ArticuloId == id);
            if (articulos == null)
            {
                return false;
            }

            _context.Articulos.Remove(articulos);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Articulos?> Buscar(int id)
        {
            return await _context.Articulos
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.ArticuloId == id);
        }

        public async Task<List<Articulos>> Listar(Expression<Func<Articulos, bool>> criterio)
        {
            return await _context.Articulos.AsNoTracking()
                .Where(criterio)
                .ToListAsync();
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
