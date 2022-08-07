using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT14.Models
{
    public enum MainMenuPositions
    {
        PrintEmployees = 1,//в методе выбора должности были использованы обобщения, которые не могут возвращать напрямую null. в подобном случае возвращают default, a у структур default = 0
        AddEmployee,
        EditEmployee,
        SetPost,
        FireEmployee
    }
}
