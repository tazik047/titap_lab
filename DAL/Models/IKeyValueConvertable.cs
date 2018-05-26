using System.Collections.Generic;

namespace DAL.Models
{
	public interface IKeyValueConvertable
	{
		KeyValuePair<int, string> GetKeyValuePair();
	}
}