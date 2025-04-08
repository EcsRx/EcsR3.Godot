using System;
using System.Timers;
using Godot;
using R3;
using SystemsR3.Scheduling;
using Timer = System.Timers.Timer;

namespace EcsR3.Godot.Plugins.EcsR3.Godot.Scheduler;

public class GodotUpdateScheduler : IUpdateScheduler
{
	private readonly Timer _timer;
	private DateTime _previousDateTime;
	private readonly Subject<ElapsedTime> _onPreUpdate = new Subject<ElapsedTime>();
	private readonly Subject<ElapsedTime> _onUpdate = new Subject<ElapsedTime>();
	private readonly Subject<ElapsedTime> _onPostUpdate = new Subject<ElapsedTime>();

	public ElapsedTime ElapsedTime { get; private set; }
	public Observable<ElapsedTime> OnUpdate => _onUpdate.ObserveOn(GodotSynchronizationContext.Current);
	public Observable<ElapsedTime> OnPreUpdate => _onPreUpdate.ObserveOn(GodotSynchronizationContext.Current);
	public Observable<ElapsedTime> OnPostUpdate => _onPostUpdate.ObserveOn(GodotSynchronizationContext.Current);
		
	public GodotUpdateScheduler(int updateFrequencyPerSecond = 60)
	{
		_timer = new Timer { Interval = 1000f / updateFrequencyPerSecond };
		_timer.Elapsed += UpdateTick;

		_previousDateTime = DateTime.Now;
		_timer.Start();
	}

	private void UpdateTick(object sender, ElapsedEventArgs e)
	{
		var deltaTime = e.SignalTime - _previousDateTime;
		var totalTime = ElapsedTime.TotalTime + deltaTime;
		ElapsedTime = new ElapsedTime(deltaTime, totalTime);
		_onPreUpdate.OnNext(ElapsedTime);
		_onUpdate.OnNext(ElapsedTime);
		_onPostUpdate.OnNext(ElapsedTime);
		_previousDateTime = e.SignalTime;
	}

	public void Dispose()
	{
		_timer.Stop();
		_timer.Dispose();
			
		_onUpdate.Dispose();
		_onPreUpdate.Dispose();
		_onPostUpdate.Dispose();
	}
}
