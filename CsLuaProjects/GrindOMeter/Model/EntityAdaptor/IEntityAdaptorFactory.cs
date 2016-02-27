namespace GrindOMeter.Model.EntityAdaptor
{
    using Entity;

    public interface IEntityAdaptorFactory
    {
        IEntityAdaptor CreateAdoptor(EntityType type);
    }
}
