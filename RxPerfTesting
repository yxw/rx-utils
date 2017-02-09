/* Run the snippets as C# statements in LINQPad
*/
	ReactiveExtensionsPerformanceComparisons();
}  //For LINQPad: Add extra open end curl 

public void ReactiveExtensionsPerformanceComparisons()
{
   int counter = 0;
   int iterations = 1000000;

   Action<int> a = (i) => { counter++; };

   DelegateSmokeTest(iterations, a);
   ObservableRangeTest(iterations, a);
   SubjectSubscribeTest(iterations, a, NewThreadScheduler.Default, "NewThread");
   SubjectSubscribeTest(iterations, a, CurrentThreadScheduler.Instance, "CurrentThread");
   SubjectSubscribeTest(iterations, a, ImmediateScheduler.Instance, "Immediate");
   SubjectSubscribeTest(iterations, a, ThreadPoolScheduler.Instance, "ThreadPool");
   SubjectSubscribeTest(iterations, a, DefaultScheduler.Instance, "Default");                
}

public void ObservableRangeTest(int iterations, Action<int> action)
{
  int counter = 0;

  long start = DateTime.Now.Ticks;

  Observable.Range(0, iterations).Subscribe(action);

  OutputTestDuration("Observable.Range()", start, counter);
}


public void SubjectSubscribeTest(int iterations, Action<int> action, IScheduler scheduler, string mode)
{
  int counter = 0;

  var eventSubject = new Subject<int>();
  var events = eventSubject.SubscribeOn(scheduler);
  events.Subscribe(action);

  long start = DateTime.Now.Ticks;

  Enumerable.Range(0, iterations).ToList().ForEach
      (
          a => eventSubject.OnNext(1)
      );

  OutputTestDuration("Subject.Subscribe() - " + mode, start, counter);
}

public void DelegateSmokeTest(int iterations, Action<int> action)
{
  int counter = 0;
  long start = DateTime.Now.Ticks;

  Enumerable.Range(0, iterations).ToList().ForEach
      (
          a => action(1)
      );

  OutputTestDuration("Delegate", start, counter);
}

public void OutputTestDuration(string test, long duration, int counter)
{
  //int counter = 0;
  Debug.WriteLine(string.Format("{0, -40} - ({1}) - {2}", test, counter, ElapsedDuration(duration)));
}

public string ElapsedDuration(long elapsedTicks)
{
  return new TimeSpan(DateTime.Now.Ticks - elapsedTicks).ToString();

//} // For LINQPad: Remove the last closing tailing curl
