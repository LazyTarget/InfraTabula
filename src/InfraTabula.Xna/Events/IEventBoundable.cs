namespace InfraTabula.Xna
{
    public interface IEventBoundable
    {
        void BindEvent(IEvent evt);
        void UnbindEvent(IEvent evt);
    }
}
