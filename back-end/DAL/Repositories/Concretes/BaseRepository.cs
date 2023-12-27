using DAL.Context;
using DAL.Repositories.Abstracts;
using ENTITIES.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Concretes
{

    /*
    IRepository Arayüzünü uygulama ve CRUD işlemlerini gerçekleştirme
    Veritabanı bağlantısı yönetimi için DbContext (_db) örneği.

    Repository Pattern
    Veritabanı işlemlerini soyutlamak ve genelleştirmek için kullanım.


    Arayüz(IRepository) ve somut sınıf(BaseRepository) kullanılarak soyutlama ve
    bağımlılıkları yönetme sağlandı. Bu yapı Dependency Injection gibi prensiplere uygunluk sağlar.


    IRepository veritabanı işlemlerini soyutlar ve kontrat sağlar. BaseRepository
    bu kontratı uygular ve somut veritabanı işlemlerini gerçekleştirir.
    Bu sayede proje IRepository arayüzüne bağımlı hale getirilir. İşlemler ise BaseRepositoryden yapılır.
    Yönetilebilirlik sağlanır.


    CategoryRepository ise BaseRepository<Category> sınıfını genişletip ICategoryRepository arayüzünü uyguluyor.
     */
    public class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {

        protected MyContext _db;
        public BaseRepository(MyContext db)
        {
            _db = db;
        }

        public IQueryable<T> GetAll()
        {
            return _db.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetActives()
        {
            return Where(x => x.Status == ENTITIES.Enums.DataStatus.Active);
        }
        public void Add(T item)
        {
            _db.Set<T>().Add(item);
            _db.SaveChanges();
        }

        public void Delete(T item)
        {
            item.Status = ENTITIES.Enums.DataStatus.Deleted;
            _db.SaveChanges();
        }

        public void Destroy(T item)
        {

            _db.Set<T>().Remove(item);
            _db.SaveChanges();
        }

        public async Task<T> FindAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }



        public async Task Update(T item)
        {
            T original = await FindAsync(item.ID);
            _db.Entry(original).CurrentValues.SetValues(item);
            await _db.SaveChangesAsync();
        }


        public IQueryable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _db.Set<T>().Where(exp);
        }
    }
}