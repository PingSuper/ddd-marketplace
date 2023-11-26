using Marketplace.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.UserProfile
{
    public class FullName: Value<FullName>
    {
        public string Value { get; private set; }

        internal FullName(string value) => Value = value;

        public static FullName FromString(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentNullException(nameof(fullName));

            return new FullName(fullName);
        }

        public static implicit operator string(FullName fullName) => fullName.Value;

        // Satisfy the serialization requirements
        protected FullName() { }
    }
}
