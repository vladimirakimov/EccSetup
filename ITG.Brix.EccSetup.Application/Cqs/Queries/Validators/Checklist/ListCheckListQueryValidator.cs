using FluentValidation;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Constants;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Validators
{
    public class ListChecklistQueryValidator : AbstractValidator<ListChecklistQuery>
    {
        public ListChecklistQueryValidator()
        {
            RuleFor(x => x.Top).Custom((elem, context) =>
            {
                if (elem != null)
                {
                    var topMaxValue = Consts.CqsValidation.TopMaxValue;
                    string topRangeMessage = string.Format(CustomFailures.TopRange, topMaxValue);
                    try
                    {
                        var top = Convert.ToInt32(elem);
                        if (top <= 0 || top > topMaxValue)
                        {
                            context.AddCustomFault("$top", CustomFaultCode.InvalidQueryTop, topRangeMessage);
                        }
                    }
                    catch (FormatException)
                    {
                        context.AddCustomFault("$top", CustomFaultCode.InvalidQueryTop, CustomFailures.TopInvalid);
                    }
                    catch (OverflowException)
                    {
                        context.AddCustomFault("$top", CustomFaultCode.InvalidQueryTop, topRangeMessage);
                    }
                }
            });

            RuleFor(x => x.Skip).Custom((elem, context) =>
            {
                if (elem != null)
                {
                    var skipMaxValue = Consts.CqsValidation.SkipMaxValue;
                    string skipRangeMessage = string.Format(CustomFailures.SkipRange, skipMaxValue);
                    try
                    {
                        var skip = Convert.ToInt32(elem);
                        if (skip < 0 || skip > skipMaxValue)
                        {
                            context.AddCustomFault("$skip", CustomFaultCode.InvalidQuerySkip, skipRangeMessage);
                        }
                    }
                    catch (FormatException)
                    {
                        context.AddCustomFault("$skip", CustomFaultCode.InvalidQuerySkip, CustomFailures.SkipInvalid);
                    }
                    catch (OverflowException)
                    {
                        context.AddCustomFault("$skip", CustomFaultCode.InvalidQuerySkip, skipRangeMessage);
                    }
                }
            });
        }
    }
}
