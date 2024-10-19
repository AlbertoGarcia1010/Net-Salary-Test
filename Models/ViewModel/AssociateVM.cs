using Salary.Models.Entities;

namespace Salary.Models.ViewModel
{
    public class AssociateVM: Associate
    {
        public virtual string CompleteName { 
            get
            {
                return $"{AssociateName} {AssociateFirstLastName} {AssociateSecondLastName}";
            }
        }

        public virtual string StatusName
        {
            get
            {
                var txtStatus = ""; 
                switch (StatusID)
                {
                    case 1: txtStatus = "Activo"; break;
                    case 2: txtStatus = "Inactivo"; break;
                    case 3: txtStatus = "Eliminado"; break;
                    default: txtStatus = "Sin Status"; break;
                }
                return txtStatus;
            }
        }
    }
}
