using ITG.Brix.EccSetup.Domain.Bases;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class DefaultRemark : ValueObject
    {
        public string Remark { get; private set; }

        public DefaultRemark(string remark)
        {
            Remark = remark;
        }


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Remark;
        }
    }
}
