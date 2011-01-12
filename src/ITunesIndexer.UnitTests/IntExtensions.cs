using NUnit.Framework;

namespace ITunesIndexer.UnitTests
{
	public static class IntExtensions
	{
		public static void Should_be(this int actual, int expected)
		{
			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}