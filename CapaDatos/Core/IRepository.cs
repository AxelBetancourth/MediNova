using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Core
{
    public interface IRepository<T> where T : class
    {
        void Agregar(T entidad);
        IQueryable<T> Consulta();
        void Editar(T entidad);
        void Eliminar(T entidad);
    }
}
