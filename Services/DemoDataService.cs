using System.Collections.ObjectModel;
using AshkanIndustrialOperations.Models;
namespace AshkanIndustrialOperations.Services;
public sealed class DemoDataService {
 public ObservableCollection<Machine> Machines {get;} = new() {
  new(){Name="CNC-01",Area="Machining",Status="Running",Oee=91.8,Temperature=54,Vibration=1.2,LastService="2026-09-18"},
  new(){Name="CNC-04",Area="Machining",Status="Alarm",Oee=68.2,Temperature=71,Vibration=5.9,LastService="2026-08-22"},
  new(){Name="PRESS-02",Area="Forming",Status="Running",Oee=88.5,Temperature=49,Vibration=1.0,LastService="2026-09-27"},
  new(){Name="COMP-02",Area="Utilities",Status="Warning",Oee=82.1,Temperature=78,Vibration=2.4,LastService="2026-09-05"},
  new(){Name="PACK-01",Area="Packaging",Status="Running",Oee=94.3,Temperature=43,Vibration=.8,LastService="2026-09-29"}};
 public ObservableCollection<Alarm> Alarms {get;} = new(){
  new(){Code="ALM-1042",Asset="CNC-04",Severity="Critical",Message="Spindle vibration above threshold",Time="12 min ago",State="Open"},
  new(){Code="ALM-1038",Asset="COMP-02",Severity="Warning",Message="Temperature trending upward",Time="28 min ago",State="Assigned"},
  new(){Code="ALM-1027",Asset="PRESS-02",Severity="Info",Message="Inspection window approaching",Time="1 h ago",State="Open"}};
 public ObservableCollection<WorkOrder> WorkOrders {get;} = new(){
  new(){Number="WO-26091",Asset="CNC-04",Type="Corrective",Priority="Critical",Assignee="M. Schneider",DueDate="Today 14:00",Status="In Progress"},
  new(){Number="WO-26092",Asset="COMP-02",Type="Inspection",Priority="High",Assignee="L. Weber",DueDate="Today 17:00",Status="Open"},
  new(){Number="WO-26084",Asset="PACK-01",Type="Preventive",Priority="Medium",Assignee="S. Keller",DueDate="2026-10-03",Status="Open"}};
 public ObservableCollection<AppUser> Users {get;} = new(){
  new(){Username="admin",DisplayName="System Administrator",Role="Administrator",Department="IT / OT",Status="Active",LastLogin="Now"},
  new(){Username="operator01",DisplayName="Anna Fischer",Role="Operator",Department="Production",Status="Active",LastLogin="Today 07:58"},
  new(){Username="maint01",DisplayName="Markus Schneider",Role="Maintenance Engineer",Department="Maintenance",Status="Active",LastLogin="Today 08:11"},
  new(){Username="viewer01",DisplayName="Lena Weber",Role="Viewer",Department="Quality",Status="Active",LastLogin="Yesterday"}};
 public ObservableCollection<AppNotification> Notifications {get;} = new(){
  new(){Severity="Critical",Title="CNC-04 requires attention",Message="Vibration exceeded the critical threshold.",Time="12 min ago"},
  new(){Severity="Warning",Title="Preventive maintenance due",Message="PACK-01 service window is approaching.",Time="1 h ago"}};
 public ObservableCollection<AuditEntry> Audit {get;}=new(){ new(){Time="Now",Action="Session started",Detail="Administrator signed in"}, new(){Time="08:42",Action="Alarm acknowledged",Detail="ALM-1031 / LINE-02"}, new(){Time="07:55",User="operator01",Action="Work order updated",Detail="WO-26084 assigned"}};
 public void Log(string action,string detail)=>Audit.Insert(0,new(){Time=DateTime.Now.ToString("HH:mm:ss"),Action=action,Detail=detail});
}
