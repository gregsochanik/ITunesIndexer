using System.Collections.Generic;
using ITunesIndexer.Solr;
using log4net;
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
			batchedIndexer.GenerateIndex(_itemsToIndex);

			_solrResolver.AssertWasCalled(x=>x.GetSolrOperationInstance());
		}
		
		[Test]
		public void Should_work_out_correct_number_of_batches()
		{
			const int batchBy = 10;
			const int numberOfItems = 28;
			const int expectedBatches = numberOfItems / batchBy;
			const int expectedRemainder = numberOfItems % batchBy;

			Given_a_solr_resolver_that_returns_a_stubbed_solr_instance();
			Given_a_set_of_items_of_length(numberOfItems);

			var batchedIndexer = new Batcher<TestClass>(batchBy);
			batchedIndexer.PrepareBatch(_itemsToIndex.Count);

			Assert.That(batchedIndexer.NumberOfBatches, Is.EqualTo(expectedBatches));
			Assert.That(batchedIndexer.Remainder, Is.EqualTo(expectedRemainder));
		}

		[Test]
		public void Should_fire_GetBatch_correct_number_of_times()
		{
			const int batchBy = 10;
			const int numberOfItems = 30;
			const int expectedBatches = numberOfItems / batchBy;
			int expectedRemainder = numberOfItems % batchBy;
			int expectedNumberOfTimes = (expectedRemainder < 1) ? expectedBatches : expectedBatches + 1;

			Given_a_solr_resolver_that_returns_a_stubbed_solr_instance();
			Given_a_set_of_items_of_length(numberOfItems);

			var batcher = MockRepository.GenerateStub<IBatcher<TestClass>>();
			batcher.Stub(x => x.GetBatch(null, 0, 0)).IgnoreArguments().Return(new List<TestClass>());
			batcher.Stub(x => x.NumberOfBatches).Return(expectedBatches);
			batcher.Stub(x => x.Remainder).Return(expectedRemainder);

			var log = MockRepository.GenerateStub<ILog>();
			var batchedIndexer = new BatchedIndexer<TestClass>(_solrResolver, batcher, log);
			batchedIndexer.GenerateIndex(_itemsToIndex);
			batcher.AssertWasCalled(x => x.GetBatch(Arg<IEnumerable<TestClass>>.Is.Anything, Arg<int>.Is.Anything, Arg<int>.Is.Anything),
				o => o.Repeat.Times(expectedNumberOfTimes));

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
