using System;

using NUnit.Framework;

namespace ThreeSeventy.Vector.Client.Tests
{
    [TestFixture]
    public class RetryStrategyTests
    {
        const int INTERVAL_MS = 500;
        const int ATTEMPTS = 3;

        private class AllAreTransient : IRetryDetectionStrategy
        {
            public bool IsTransient(Exception ex)
            {
                return true;
            }
        }

        private static void RunBasicTests(RetryStrategy strategy, int expectedTimeMs)
        {
            DateTime start = DateTime.UtcNow;

            var retry = new RetryPoliciy<AllAreTransient>(strategy);

            int tries = 0;

            Assert.Throws<MockException>(
                () => retry.Execute(
                    attempt =>
                    {
                        ++tries;
                        throw new MockException("Deliberately fail");
                    })
                );

            DateTime finish = DateTime.UtcNow;
            TimeSpan time = finish.Subtract(start);

            Assert.AreEqual(ATTEMPTS, tries);

            // We expect that it could take longer than expected, but never LESS.
            Assert.GreaterOrEqual(expectedTimeMs, time.TotalMilliseconds, "Delay was too short.");
        }

        [Test]
        public void TestFixedStrategy()
        {
            var strategy = new FixedRetryStrategy
            {
                MaxTries = ATTEMPTS,
                Interval = TimeSpan.FromMilliseconds(INTERVAL_MS),
                MaxInterval = TimeSpan.FromSeconds(2),
                MinInterval = TimeSpan.FromMilliseconds(500)
            };

            const int EXPECTED_TIME_MS = ATTEMPTS * INTERVAL_MS;

            RunBasicTests(strategy, EXPECTED_TIME_MS);
        }

        [Test]
        public void TestIncrementalStrategy()
        {
            var strategy = new IncrementalRetryStrategy
            {
                MaxTries = ATTEMPTS,
                Interval = TimeSpan.FromMilliseconds(INTERVAL_MS),
                MaxInterval = TimeSpan.FromSeconds(2),
                MinInterval = TimeSpan.FromMilliseconds(500)
            };

            int expectedTimeMs = 0;

            for (int i = 0; i < ATTEMPTS; ++i)
                expectedTimeMs += INTERVAL_MS * (i + 1);

            RunBasicTests(strategy, expectedTimeMs);
        }

        [Test]
        public void TestExponentialStrategy()
        {
            var strategy = new ExponentialRetryStrategy
            {
                MaxTries = ATTEMPTS,
                Interval = TimeSpan.FromMilliseconds(INTERVAL_MS),
                MaxInterval = TimeSpan.FromSeconds(2),
                MinInterval = TimeSpan.FromMilliseconds(500)
            };

            int expectedTimeMs = 0;

            for (int i = 0; i < ATTEMPTS; ++i)
            {
                int scale = 1 << i;
                expectedTimeMs += INTERVAL_MS * scale;
            }

            RunBasicTests(strategy, expectedTimeMs);
        }
    }
}
