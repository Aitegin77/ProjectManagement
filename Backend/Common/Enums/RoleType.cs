using System.ComponentModel;

namespace Common.Enums
{
    public enum RoleType
    {
        [Description("Руководитель")]
        Admin = 1,
        [Description("Менеджер проекта")]
        ProjectManager = 2,
        [Description("Сотрудник")]
        Employee = 3,
    }
}
