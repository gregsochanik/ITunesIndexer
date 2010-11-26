using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Facilities.SolrNetIntegration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using SolrNet;

namespace ITunesIndexer.Solr
{
	public class SolrNetFacade<T> : ISolrFacade
	{
		private ISolrOperations<T> _solrOperations;

		public SolrNetFacade(IContainerBuilder containerbuilder)
		{
			var container = containerbuilder.GetContainer();
			_solrOperations = container.Resolve<ISolrOperations<T>>();
		}

		//Add and commit
		//Add
		//Query
		//Delete
		//ClearAll
		//Optimise
	}


	public interface ISolrFacade
	{
		//Add and commit
		//Add
		//Query
		//Delete
		//ClearAll
	}

	public class SolrContainerBuilder : IContainerBuilder
	{
		public WindsorContainer GetContainer()
		{
			var solrFacility = new SolrNetFacility();
			var container = new WindsorContainer(new XmlInterpreter());
			container.AddFacility("solr", solrFacility);

			return container;
		}
	}

	public interface IContainerBuilder
	{
		WindsorContainer GetContainer();
	}
}
