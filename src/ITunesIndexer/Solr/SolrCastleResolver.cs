using Castle.Facilities.SolrNetIntegration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
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

	public interface ISolrResolver<T>
	{
		ISolrOperations<T> GetSolrOperationInstance();
	}



	public class SolrContainerBuilder : IContainerBuilder
	{
		private readonly IWindsorContainer _container;

		public SolrContainerBuilder() 
			: this( new WindsorContainer(new XmlInterpreter()))
		{}

		public SolrContainerBuilder(IWindsorContainer container)
		{
			_container = container;
		}

		public IWindsorContainer GetContainer(string facilityName)
		{
			var solrFacility = new SolrNetFacility();
			_container.AddFacility(facilityName, solrFacility);

			return _container;
		}
	}

	public interface IContainerBuilder
	{
		IWindsorContainer GetContainer(string facilityName);
	}
}
