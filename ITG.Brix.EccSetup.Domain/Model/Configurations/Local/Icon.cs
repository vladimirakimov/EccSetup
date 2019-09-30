using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System;

namespace ITG.Brix.EccSetup.Domain
{
    public class Icon : Entity
    {
        public string Name { get; private set; }

        public string DataPath { get; private set; }

        public Icon(Guid id, string name, string dataPath)
        {
            if (id == default(Guid))
            {
                throw Error.Argument(string.Format("{0} can't be dafault guid.", nameof(id)));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(name)));
            }

            if (string.IsNullOrEmpty(dataPath))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(dataPath)));
            }

            Id = id;
            Name = name;
            DataPath = dataPath;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangeDataPath(string dataPath)
        {
            DataPath = dataPath;
        }
    }
}
