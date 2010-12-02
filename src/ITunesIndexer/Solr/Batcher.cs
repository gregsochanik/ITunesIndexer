using System.Collections.Generic;
using System.Linq;

namespace ITunesIndexer.Solr
{
	public class Batcher<T> : IBatcher<T>
	{
		private int _batchBy = 10;
		public int BatchBy
		{
			get { return _batchBy; }
			set { _batchBy = value; }
		}

		public int NumberOfBatches { get; private set; }

		public int Remainder { get; private set; }

		public Batcher(){}

		public Batcher(int batchBy)
		{
			_batchBy = batchBy;
		}

		public IEnumerable<T> GetBatch(IEnumerable<T> itemsToIndex, int start, int position)
		{
			PrepareBatch(itemsToIndex.Count());
			int numToTake = position == NumberOfBatches ? Remainder : _batchBy;
			return itemsToIndex.Skip(start).Take(numToTake);
		}

		private void PrepareBatch(int numberOfItems)
		{
			NumberOfBatches = numberOfItems / _batchBy;
			Remainder = numberOfItems % _batchBy;
		}
	}
}