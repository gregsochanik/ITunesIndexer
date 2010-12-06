using System.Collections.Generic;

namespace ITunesIndexer.ItunesXml
{
	public interface ILibraryConvertor<in TFrom, out TTo>
	{
		IEnumerable<TTo> ConvertLibrary(IEnumerable<TFrom> toConvert);
	}
}