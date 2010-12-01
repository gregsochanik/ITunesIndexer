using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITunesIndexer.Solr;
using NUnit.Framework;
using Rhino.Mocks;
using SolrNet;

namespace ITunesIndexer.UnitTests
{
	public class BatchedIndexerTests
	{
		private ISolrResolver<TestClass> _solrResolver;
		private List<TestClass> _itemsToIndex;

		[Test]
		public void Should_fire_getsolroperationinstance()
		{
			Given_a_solr_resolver_that_returns_a_stubbed_solr_instance();
			Given_a_set_of_items_of_length(3);
			var batchedIndexer = new BatchedIndexer<TestClass>(_solrResolver);
			batchedIndexer.Index(_itemsToIndex);

			_solrResolver.AssertWasCalled(x=>x.GetSolrOperationInstance());
		}

		[Test]
		public void Should_work_out_correct_numberofitems()
		{
			const int numberOfItems = 20;

			Given_a_solr_resolver_that_returns_a_stubbed_solr_instance();
			Given_a_set_of_items_of_length(numberOfItems);

			var batchedIndexer = new BatchedIndexer<TestClass>(_solrResolver);
			batchedIndexer.Index(_itemsToIndex);

			Assert.That(batchedIndexer.NumberOfItems, Is.EqualTo(numberOfItems));
		}

		private void Given_a_solr_resolver_that_returns_a_stubbed_solr_instance()
		{
			var solrInstance = MockRepository.GenerateStub<ISolrOperations<TestClass>>();

			_solrResolver = MockRepository.GenerateStub<ISolrResolver<TestClass>>();
			_solrResolver.Stub(x => x.GetSolrOperationInstance()).Return(solrInstance);
		}

		private void Given_a_set_of_items_of_length(int length)
		{
			_itemsToIndex = new List<TestClass>();

			for(int i = 0; i<length; i++)
			{
				_itemsToIndex.Add(new TestClass(i));
			}
		}
	}

	public class TestClass
	{
		public int Id { get; set; }

		public TestClass(int id)
		{
			Id = id;
		}
	}
}
