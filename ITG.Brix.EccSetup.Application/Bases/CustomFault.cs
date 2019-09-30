using ITG.Brix.EccSetup.Application.Enums;

namespace ITG.Brix.EccSetup.Application.Bases
{
    public class CustomFault : Failure
    {
        public ErrorType Type
        {
            get
            {
                return ErrorType.CustomError;
            }
        }
    }
}
