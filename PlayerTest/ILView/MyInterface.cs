using System;

namespace PlayerTest.ILView
{
	public interface IMyInterface
	{
		void MyMethod();
	}
	class MyClass : IMyInterface
	{
		void IMyInterface.MyMethod()
		{

		}
	}

}