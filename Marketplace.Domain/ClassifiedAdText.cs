using System;
using Marketplace.Framework;

namespace Marketplace.Domain
{
	public class ClassifiedAdText : Value<ClassifiedAdText>
	{
		public string Value { get; }

		internal ClassifiedAdText(string value)
		{
			Value = value;
		}

		public static ClassifiedAdText FromString(string value) => new ClassifiedAdText(value);

        public static implicit operator string(ClassifiedAdText text) => text.Value;

        // Satisfy the serialization requirements
        protected ClassifiedAdText() { }
    }
}

