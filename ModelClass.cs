using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Search;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace WpfApplication3
{
	public class ModelClass :INotifyPropertyChanged
	{
		public ModelClass()
		{
			SearchCommand = new SearchCommand(this);
		}

		public SearchCommand SearchCommand { get; private set; }

		string _serverName = "http://spark.furore.com/fhir/";

		public string ServerName
		{
			get { return _serverName; }
			set { _serverName = value; NotifyChange("ServerName");  }
		}
		string _searchFor;

		public string SearchFor
		{
			get { return _searchFor; }
			set { _searchFor = value; NotifyChange("SearchFor");  }
		}
		int _recordCount;

		public int RecordCount
		{
			get { return _recordCount; }
			set { _recordCount = value; NotifyChange("RecordCount"); }
		}

		protected void NotifyChange(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private ObservableCollection<Hl7.Fhir.Model.Patient> _patients;

		public ObservableCollection<Hl7.Fhir.Model.Patient> Patients
		{
			get { return _patients; }
			set { _patients = value; NotifyChange("Patients"); }
		}

		public void Search()
		{
			FhirClient server = new FhirClient(_serverName);
			Query q = new Query().For<Patient>().LimitTo(20).OrderBy("family");
			if (!string.IsNullOrEmpty(_searchFor))
				q.AddParameter("name", _searchFor);
			var results = server.Search(q);

			RecordCount = results.TotalResults.Value;
			Patients = new ObservableCollection<Patient>();
			foreach (ResourceEntry<Patient> item in results.Entries)
			{
				Patients.Add(item.Resource);
			}
		}
	}

	public class SearchCommand :ICommand
	{
		private ModelClass _model;

		public SearchCommand(ModelClass modelClass)
		{
			this._model = modelClass;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			_model.Search();
		}
	}
}
