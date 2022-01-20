using System.Collections.Generic;

namespace DIO.Series.Interfaces
{
    public interface IRepositorio<T>
    {
        List<T> Listar();
        T RetornaPorId(int id);
        T Insere(T entidade);
        void Exclui(int id);
        void Atualizar(T entidade);
    }
}