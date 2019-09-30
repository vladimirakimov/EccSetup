using FluentValidation;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Enums;

namespace ITG.Brix.EccSetup.Application.Extensions
{
    public static class RuleBuilderOptionsExtensions
    {
        // TODO : get rid of

        public static IRuleBuilderOptions<T, TProperty> ValidationError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, string errorMessage)
        {
            return rule.WithMessage(errorMessage);
        }

        //

        public static IRuleBuilderOptions<T, TProperty> CustomFault<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, CustomFaultCode errorCode, string errorMessage)
        {
            return rule.WithMessage(errorMessage).WithErrorCode(ErrorType.CustomError.ToString() + "###" + errorCode.Name);
        }

        public static IRuleBuilderOptions<T, TProperty> ValidationFault<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, string errorMessage)
        {
            return rule.WithMessage(errorMessage);
        }
    }
}
