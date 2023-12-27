using ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ManagerServices.Abstracts
{
    public interface IManager<T> where T : class, IEntity
    {
        //List Commands
        IQueryable<T> GetAll();
        IQueryable<T> GetActives();

        //Modify Commands

        string Add(T item);





        Task Update(T item);

        void Delete(T item);

        void Destroy(T item);


        //Find
        Task<T> FindAsync(int id);


        IQueryable<T> Where(Expression<Func<T, bool>> exp);
    }
}