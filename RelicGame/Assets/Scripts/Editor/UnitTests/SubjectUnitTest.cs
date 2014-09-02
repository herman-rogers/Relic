using UnityEngine;
using NUnit.Framework;

namespace UnityTest {
    internal class SubjectUnitTest {
		[Test]
		public void AddObserver_CorrectNumberOfInstancesAreAdded( ) {
			Observer testObject =  MockInstanceOfObserver( );
			Assert.True( ( Subject.ObserverCount( ) == 1 ) );
			Subject.RemoveAllObservers( );
			Assert.True( ( Subject.ObserverCount( ) == 0 ) );
		}

	    [Test]
		public void AddObserver_DuplicateObserversAreNotAddedToList( ) {
			Observer testObserver = MockInstanceOfObserver( );
			Subject.AddObserver( testObserver );
			Assert.True( Subject.ObserverCount( ) == 1 );
			Subject.RemoveAllObservers( );
			Assert.True( ( Subject.ObserverCount( ) == 0 ) );
		}

		Observer MockInstanceOfObserver( ) {
			Observer testObserver = new Observer( );
			return testObserver;
		}
	}
}
