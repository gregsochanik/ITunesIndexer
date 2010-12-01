using Castle.Facilities.SolrNetIntegration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace ITunesIndexer.Solr
{
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
}