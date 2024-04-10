namespace Backend.RepositoryFolder
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<List<T>> GetAll();

        public Task AddItem(T item);
        public Task DeleteItem(T item);
        public Task DeleteItemId(int id);  
        public Task<bool> IfExist(T item);

        public Task<T> GetItem<G>(G item, string propName);
    }
}
