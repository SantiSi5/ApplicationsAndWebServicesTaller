using System.ComponentModel.DataAnnotations;
using Taller.Shared.Interfaces;

namespace Taller.Shared.Entities;

public class Employee : IEntityWithFLS
{
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    [MaxLength(30, ErrorMessage = "El Campo {0} no puede tener mas de {1} carácteres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Apellido")]
    [MaxLength(30, ErrorMessage = "El Campo {0} no puede tener mas de {1} carácteres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string LastName { get; set; } = null!;

    public bool IsActive { get; set; }

    [Display(Name = "Fecha de contratación")]
    [DataType(DataType.Date)]
    public DateTime HireDate { get; set; }

    [Display(Name = "Salario")]
    [Range(1000000, double.MaxValue, ErrorMessage = "El campo {0} debe ser mínimo {1}.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public decimal Salary { get; set; }
}