using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.EccSetup.Domain
{
    public class QuestionType : IComparable
    {
        public static QuestionType SingleChoice = new QuestionType(0, "Single choice");
        public static QuestionType MultipleChoice = new QuestionType(1, "Multiple choice");
        public static QuestionType Open = new QuestionType(2, "Open");

        public string Name { get; private set; }
        public int Id { get; private set; }

        protected QuestionType() { }
        public QuestionType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static IEnumerable<QuestionType> List()
        {
            return new[] { SingleChoice, MultipleChoice, Open };
        }

        public static QuestionType Parse(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ArgumentException($"Possible values for QuestionType: {String.Join(", ", List().Select(s => s.Name))}");
            }

            return state;
        }
        public int CompareTo(object obj) => Id.CompareTo(((QuestionType)obj).Id);

        public override bool Equals(object obj)
        {
            var otherValue = obj as QuestionType;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
