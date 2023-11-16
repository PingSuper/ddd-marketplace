using System;
using Marketplace.Framework;
namespace Marketplace.Domain
{
	public class ClassifiedAdId: Value<ClassifiedAdId>
	{
        public Guid Value { get; private set; }

        public ClassifiedAdId(Guid value)
		{
			if (value == default)
				throw new ArgumentNullException(nameof(value), "Classified Ad id cannot be empty");

			Value = value;
		}

		// Assign value of value object to primitive type directly
		public static implicit operator Guid(ClassifiedAdId self) => self.Value;
	}
}


