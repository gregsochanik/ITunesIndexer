using Castle.Windsor;

namespace ITunesIndexer.Solr
{
	public interface IContainerBuilder
	{
		IWindsorContainer GetContainer(string facilityName);
	}
}