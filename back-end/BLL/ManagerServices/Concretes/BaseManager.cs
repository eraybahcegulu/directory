using BLL.ManagerServices.Abstracts;
using DAL.Repositories.Abstracts;
using ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ManagerServices.Concretes
{
    public class BaseManager<T> : IManager<T> where T : class, IEntity
    {
        protected IRepository<T> _irp;

        public BaseManager(IRepository<T> irp)
        {
            _irp = irp;
        }

        public IQueryable<T> GetAll()
        {
            return _irp.GetAll();
        }

        public IQueryable<T> GetActives()
        {
            return _irp.GetActives();
        }

        public string Add(T item)
        {

            if (item.CreatedDate != null)
            {
                _irp.Add(item);
                return "Ekleme başarılı";
            }
            return "Ekleme başarısız";
        }




        public void Delete(T item)
        {
            _irp.Delete(item);
        }



        public void Destroy(T item)
        {
            _irp.Destroy(item);
        }



        public async Task<T> FindAsync(int id)
        {
            return await _irp.FindAsync(id);
        }



        public async Task Update(T item)
        {
            await _irp.Update(item);
        }




        public IQueryable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _irp.Where(exp);
        }

    }
}