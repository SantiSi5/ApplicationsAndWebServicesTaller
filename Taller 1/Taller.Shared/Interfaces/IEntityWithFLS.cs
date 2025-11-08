namespace Taller.Shared.Interfaces;

public interface IEntityWithFLS
{
    string FirstName { get; set; }
    string LastName { get; set; }
    public decimal Salary { get; set; }
}