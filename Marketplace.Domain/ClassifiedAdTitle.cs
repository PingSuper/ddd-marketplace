﻿using System;
using System.Text.RegularExpressions;
using Marketplace.Framework;

namespace Marketplace.Domain
{
	public class ClassifiedAdTitle : Value<ClassifiedAdTitle>
	{
		public static ClassifiedAdTitle FromString(string title) => new ClassifiedAdTitle(title);

		public static ClassifiedAdTitle FromHtml(string htmlTitle)
		{
			var supportedTagsReplaced = htmlTitle
				.Replace("<i>", "*")
				.Replace("</i>", "*")
				.Replace("<b>", "**")
				.Replace("</b>", "**");

			return new ClassifiedAdTitle(Regex.Replace(supportedTagsReplaced, "<.*?>", string.Empty));
		}


		private readonly string _value;

		internal ClassifiedAdTitle(string value)
		{
			_value = value;
		}

        public static implicit operator string(ClassifiedAdTitle self) => self._value;
    }


}
