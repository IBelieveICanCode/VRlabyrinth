namespace ObjectPool
{
    public interface IFactory<T>
    {
        T Create();
    }
}