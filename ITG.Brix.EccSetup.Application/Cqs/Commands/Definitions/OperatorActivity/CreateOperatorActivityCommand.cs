using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using Newtonsoft.Json.Linq;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions.OperatorActivity
{
    public class CreateOperatorActivityCommand : IRequest<Result>
    {
        public JObject OperatorActivities { get; private set; }

        public CreateOperatorActivityCommand(JObject operatorActivities)
        {
            OperatorActivities = operatorActivities;
        }
    }
}
