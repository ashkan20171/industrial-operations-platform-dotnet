using System.Collections.Specialized;
using System.ComponentModel;
using AshkanIndustrialOperations.Infrastructure;
using AshkanIndustrialOperations.Models;
namespace AshkanIndustrialOperations.Services;
public sealed class PlantPersistenceService
{
    private readonly IPlantDataStore _store;
    private bool _loading;
    public string DatabasePath => _store.DatabasePath;
    public PlantPersistenceService(IPlantDataStore store) => _store = store;
    public void Initialize(DemoDataService data, Stage6DataService stage6)
    {
        var saved = _store.Load();
        if (saved.Machines.Count > 0 || saved.Alarms.Count > 0 || saved.WorkOrders.Count > 0 || saved.Parts.Count > 0 || saved.Plans.Count > 0)
        {
            _loading = true;
            Replace(data.Machines, saved.Machines); Replace(data.Alarms, saved.Alarms); Replace(data.WorkOrders, saved.WorkOrders);
            Replace(stage6.Parts, saved.Parts); Replace(stage6.Plans, saved.Plans);
            _loading = false;
        }
        Watch(data.Machines, () => Save(data, stage6)); Watch(data.Alarms, () => Save(data, stage6)); Watch(data.WorkOrders, () => Save(data, stage6));
        Watch(stage6.Parts, () => Save(data, stage6)); Watch(stage6.Plans, () => Save(data, stage6));
        Save(data, stage6);
    }
    public void Save(DemoDataService data, Stage6DataService stage6)
    {
        if (_loading) return;
        _store.Save(new PlantDataSnapshot { Machines=[..data.Machines], Alarms=[..data.Alarms], WorkOrders=[..data.WorkOrders], Parts=[..stage6.Parts], Plans=[..stage6.Plans] });
    }
    private static void Replace<T>(System.Collections.ObjectModel.ObservableCollection<T> target, IEnumerable<T> source) { target.Clear(); foreach(var item in source) target.Add(item); }
    private static void Watch<T>(System.Collections.ObjectModel.ObservableCollection<T> source, Action save)
    {
        source.CollectionChanged += (_, e) => { if(e.NewItems is not null) foreach(var x in e.NewItems) if(x is INotifyPropertyChanged n) n.PropertyChanged += (_,_)=>save(); save(); };
        foreach(var x in source) if(x is INotifyPropertyChanged n) n.PropertyChanged += (_,_)=>save();
    }
}
