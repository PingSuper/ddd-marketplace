﻿using System;
namespace Marketplace.Framework
{
	public interface IApplicationService
	{
		Task Handle(object command);
	}
}

