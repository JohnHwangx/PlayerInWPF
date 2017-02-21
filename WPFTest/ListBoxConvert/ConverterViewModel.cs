using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTest.ListBoxConvert
{
	public class ConverterViewModel:NotificationObject
	{
		private List<string> _songList;

		public List<string> SongLIst
		{
			get { return _songList; }
			set
			{
				_songList = value; 
				OnPropertyChanged();
			}
		}

	}
}
