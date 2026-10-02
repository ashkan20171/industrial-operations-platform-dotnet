using System.Collections.ObjectModel;
using System.Windows.Threading;
using AshkanIndustrialOperations.Models;
namespace AshkanIndustrialOperations.Services;
public sealed class TelemetrySimulator {
 readonly DemoDataService _data; readonly DispatcherTimer _timer; readonly Random _random=new();
 public ObservableCollection<TelemetrySample> Samples {get;}=new(); public bool IsRunning=>_timer.IsEnabled;
 public TelemetrySimulator(DemoDataService data){_data=data;_timer=new DispatcherTimer{Interval=TimeSpan.FromSeconds(1)};_timer.Tick+=(_,_)=>Tick();}
 public void Start()=>_timer.Start(); public void Stop()=>_timer.Stop();
 void Tick(){foreach(var m in _data.Machines){m.Temperature=Math.Clamp(m.Temperature+(_random.NextDouble()-.48)*1.6,25,95);m.Vibration=Math.Clamp(m.Vibration+(_random.NextDouble()-.5)*.22,.2,8);Samples.Insert(0,new(){Asset=m.Name,Temperature=Math.Round(m.Temperature,1),Vibration=Math.Round(m.Vibration,2),Pressure=Math.Round(5.5+_random.NextDouble()*2.2,2),PowerKw=Math.Round(18+_random.NextDouble()*42,1)});}while(Samples.Count>100)Samples.RemoveAt(Samples.Count-1);}
}
