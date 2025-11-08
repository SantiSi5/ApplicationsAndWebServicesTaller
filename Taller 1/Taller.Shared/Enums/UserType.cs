using System.ComponentModel;

namespace Taller.Shared.Enums;

public enum UserType
{
    [Description("Administrador")]
    Admin,

    [Description("Usuario")]
    User
}