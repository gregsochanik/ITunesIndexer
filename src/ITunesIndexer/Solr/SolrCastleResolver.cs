using Castle.Windsor;
using SolrNet;

namespace ITunesIndexer.Solr
{
	public class SolrCastleResolver<T> : ISolrResolver<T>
	{
		private readonly IContainerBuilder _containerbuilder;

		public SolrCastleResolver()
			: this(new SolrContainerBuilder())
		{}

		public SolrCastleResolver(IContainerBuilder containerbuilder)
		{
			_containerbuilder = containerbuilder;
		}

		public ISolrOperations<T> GetSolrOperationInstance()
		{
			IWindsorContainer container = _containerbuilder.GetContainer("solr");
			return container.Resolve<ISolrOperations<T>>();
		}
	}
}
