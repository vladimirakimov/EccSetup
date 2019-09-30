using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.EccSetup.Domain.Bases
{
    public abstract class ValueObject
    {
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetAtomicValues();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            ValueObject other = (ValueObject)obj;
            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }
                if (thisValues.Current != null && !(thisValues.Current is IEnumerable<Enumeration>) && !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }

                if (thisValues.Current != null && (thisValues.Current is IEnumerable<Enumeration>) && !((IEnumerable<Enumeration>)thisValues.Current).SequenceEqual((IEnumerable<Enumeration>)otherValues.Current))
                {
                    return false;
                }
            }
            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public int GetHashCode<T>(IEnumerable<T> coll)
        {
            var result = 0;

            foreach (var obj in coll)
            {
                unchecked
                {
                    if (!ReferenceEquals(obj, null))
                        foreach (var item in (IEnumerable<T>)obj)
                        {
                            if (!ReferenceEquals(item, null))
                                result += item.GetHashCode();
                            result++;
                        }
                }
            }

            return result;
        }

        public override int GetHashCode()
        {
            var atomicValues = GetAtomicValues();
            var nonEnumerableAggregateResult = atomicValues.Where(x => !(x is IEnumerable<object>)).Select(x => x != null ? x.GetHashCode() : 0).Aggregate((x, y) => x ^ y);
            var enumerableAggregateResult = GetHashCode(atomicValues.Where(x => x.GetType() != typeof(string) && x is IEnumerable<object>));
            return nonEnumerableAggregateResult ^ enumerableAggregateResult;
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            return object.ReferenceEquals(a, b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }

        public ValueObject GetCopy()
        {
            return this.MemberwiseClone() as ValueObject;
        }
    }
}
