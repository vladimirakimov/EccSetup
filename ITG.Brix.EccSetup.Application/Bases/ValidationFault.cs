using ITG.Brix.EccSetup.Application.Enums;

namespace ITG.Brix.EccSetup.Application.Bases
{
    public class ValidationFault : Failure
    {
        public ErrorType Type
        {
            get
            {
                return ErrorType.ValidationError;
            }
        }
    }
}
