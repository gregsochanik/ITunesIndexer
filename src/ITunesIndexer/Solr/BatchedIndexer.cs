using System;
using System.Collections.Generic;
using System.Linq;
using log4net;

namespace ITunesIndexer.Solr
{
	public class BatchedIndexer<T> : IIndexer<T>
	{
		private readonly ISolrResolver<T> _solrResolver;
		private readonly IBatcher<T> _batcher;
		private readonly ILog _log;

		public int BatchBy
		{
			set{_batcher.BatchBy = value;}
		}

		public BatchedIndexer(ISolrResolver<T> solrResolver)
			: this(solrResolver, new Batcher<T>(),  LogManager.GetLogger(typeof(BatchedIndexer<T>)))
		{}

		public BatchedIndexer(ISolrResolver<T> solrResolver, IBatcher<T> batcher, ILog log)
		{
			_solrResolver = solrResolver;
			_batcher = batcher;
			_log = log;
		}

		public void GenerateIndex(IEnumerable<T> itemsToIndex)
		{
			int counter = 0;
			var solrInstance = _solrResolver.GetSolrOperationInstance();

			for (int position = 0; position <= _batcher.NumberOfBatches; position++)
			{
				int start = counter;

				IEnumerable<T> batch = _batcher.GetBatch(itemsToIndex, start, position);

				_log.Info(string.Format("Adding {0} through to {1}", start, start + batch.Count() - 1));
				solrInstance.Add(batch);

				_log.Info("Committing batch");
				solrInstance.Commit();
				counter += _batcher.BatchBy;
			}
			_log.Info("Optimizing index");
			solrInstance.Optimize();
		}
	}
}
