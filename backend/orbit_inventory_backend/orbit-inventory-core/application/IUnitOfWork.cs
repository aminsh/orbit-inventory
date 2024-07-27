namespace orbit_inventory_core.application;

public interface IUnitOfWork
{
    Task Commit();
}