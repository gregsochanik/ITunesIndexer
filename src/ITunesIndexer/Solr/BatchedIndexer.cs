using System;
using System.Collections.Generic;
using System.Linq;
using log4net;

namespace ITunesIndexer.Solr
{
	public class BatchedIndexer<T>
	{
		private readonly ISolrResolver<T> _solrResolver;
		private readonly ILog _log;

		private int _batchBy = 100;
		public int BatchBy
		{
			get { return _batchBy; }
			set { _batchBy = value; }
		}

		public int NumberOfItems { get; private set; }
		public int NumberOfBatches { get; private set; }

		public BatchedIndexer(ISolrResolver<T> solrResolver)
			: this(solrResolver, LogManager.GetLogger(typeof(BatchedIndexer<T>)))
		{}

		public BatchedIndexer(ISolrResolver<T> solrResolver, ILog log)
		{
			_solrResolver = solrResolver;
			_log = log;
		}

		public void Index(IEnumerable<T> itemsToIndex)
		{
			int counter = 0;
			NumberOfItems = itemsToIndex.Count();
			NumberOfBatches = NumberOfItems / _batchBy;

			var solrInstance = _solrResolver.GetSolrOperationInstance();

			// for each batch
			for (int i = 0; i < NumberOfBatches; i++)
			{
				int start = counter;
				IEnumerable<T> songBatch = itemsToIndex.Skip(start).Take(_batchBy);
				_log.Info(string.Format("Adding {0} through to {1}", start, start + songBatch.Count() - 1));
				solrInstance.Add(songBatch);

				_log.Info("Committing batch");
				solrInstance.Commit();
				counter += _batchBy;
			}
			_log.Info("Optimizing index");
			solrInstance.Optimize();
		}
	}
}
