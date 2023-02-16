using System;

namespace Demo.Core.Data
{
	public interface IDatabaseTransaction : IDisposable
	{
		void Commit();
		void Rollback();
	}
}
