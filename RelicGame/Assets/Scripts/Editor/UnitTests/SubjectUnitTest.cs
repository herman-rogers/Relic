using UnityEngine;
using NUnit.Framework;

namespace UnityTest {
    internal class SubjectUnitTest {
		[Test]
		public void AddObserver_CorrectNumberOfInstancesAreAdded( ) {
			Observer testObject =  MockInstanceOfObserver( );
			Assert.True( ( Subject.NumberOfObserversAdded( ) == 1 ) );
			Subject.ClearAllObservers( );
			Assert.True( ( Subject.NumberOfObserversAdded( ) == 0 ) );
		}

	    [Test]
		public void AddObserver_DuplicateObserversAreNotAddedToList( ) {
			Observer testObserver = MockInstanceOfObserver( );
			Subject.AddObserver( testObserver );
			Assert.True( Subject.NumberOfObserversAdded( ) == 1 );
			Subject.ClearAllObservers( );
			Assert.True( ( Subject.NumberOfObserversAdded( ) == 0 ) );
		}

//		[Test]
//		public void GarbageCollectObservers_NullObserversRemoved( ) {
//			MockInstanceOfObserver( );
//		}

		Observer MockInstanceOfObserver( ) {
			Observer testObserver = new Observer( );
			return testObserver;
		}
	}
}
