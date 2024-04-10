using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.RepositoryFolder;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace Backend.RepositoryFolder
{
    public class EFGenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _prop;

        public EFGenericRepository(AppDbContext _context)
        {
            this._context = _context;
            this._prop = this._context.Set<T>();
        }

        public async Task<List<T>> GetAll()
        {
            return await _prop.ToListAsync();
        }

        public async Task AddItem(T item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemId(int id)
        {
            var entity = await _prop.FindAsync(id);
            if (entity == null)
            {
                throw new ArgumentException("Entity not found");
            }

            DeleteItem(entity);
        }


        public async Task DeleteItem(T item)
        {
            _prop.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IfExist(T item)
        {
            T el = await _prop.FindAsync(item);
            if (el != null)
            {
                return true;
            }
            return false;
        }

        //так на перший погляд може здатись великою і затратною функцієї, але це зменшує повторення коду у майбутньому
        // якщо уявити, що в нас 10+ таблиць з багатьма колонками, а ми хочемо отримати елемент по одній з колонок, в кожній таблиці 
        // це буде велике повторення коду, тому в подільшому від цього тільки виграємо
        public async Task<T> GetItem<G>(G prop, string propName)
        {
            PropertyInfo propertyInfo = typeof(T).GetProperty(propName);

            if (propertyInfo == null)
            {
                return null;
            }
            //T res = await _prop.FirstOrDefaultAsync(e => propertyInfo.GetValue(e).Equals(prop));
            //return res;


            var parameter = Expression.Parameter(typeof(T), "e");
            var body = Expression.Equal(
                Expression.Property(parameter, propertyInfo),
                Expression.Constant(prop, typeof(G))
            );
            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
            var result = await _prop.Where(lambda).FirstOrDefaultAsync();

            return result;
        }
    }
}
