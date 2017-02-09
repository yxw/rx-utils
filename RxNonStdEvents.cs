// Run as C# program in LINQPad

void Main()
{
	TestNonStdEvent test = new TestNonStdEvent();
	test.Test();
}

}

// Define other methods and classes here

public class TestNonStdEvent {

	public TestNonStdEvent() {}

   public void Test()
   {
       var api = new Api();
       api.FieldsUpdated += OnFieldsUpdated;
       var source = Observable.FromEvent<Api.FieldsUpdatedHandler, Api.FieldsUpdatedArgs>(
           action => 
           {
               Api.FieldsUpdatedHandler handler = delegate(object sender, Api.FieldsUpdatedArgs e) { action(e); };
               return handler;
           },
           h => api.FieldsUpdated += h,
           h => api.FieldsUpdated -= h);
		source.Subscribe(
				x => Console.WriteLine("TestNonStdEvent --> {1}: {0}", x.ToString(), DateTime.Now.ToString("s.ffffff")),
				ex => Console.WriteLine("TestNonStdEvent failed --> {1}: {0}", ex.Message, DateTime.Now.ToString("s.ffffff")),
				() => Console.WriteLine("TestNonStdEvent complted"));
       api.RaiseField("A");
	   api.RaiseField("B");
       //source.Dispose();
   }

   public class Api 
   {
       public delegate void FieldsUpdatedHandler(object sender, NotStdEventArgs e);
       public event FieldsUpdatedHandler FieldsUpdated;

       public void RaiseField(string id) 
       {
           var handler = FieldsUpdated;
           if (handler != null) handler(this, new NotStdEventArgs(id));
       }

       public class NotStdEventArgs
       {
           public NotStdEventArgs(string id)
           {
               Id = id;
           }
           public string Id { get; private set;}
       }

  }
   private void OnFieldsUpdated(object sender, Api.NotStdEventArgs ev)
   {
       Console.WriteLine("On Fields updates: {1}: {0}", ev.Id, DateTime.Now.ToString("s.ffffff"));
   }
//}
