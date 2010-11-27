using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using ITunesIndexer.Solr;
using NUnit.Framework;

namespace ITunesIndexer.IntegrationTests
{
	public class SolrContainerBuilderTests
	{
		[Test]
		public void Should_resolve_a_container_with_the_correct_facility()
		{
			var solrContainerBuilder = new SolrContainerBuilder();

			IWindsorContainer windsorContainer = solrContainerBuilder.GetContainer("solr");

			Assert.That(windsorContainer, Is.Not.Null);

		}
	}
}
