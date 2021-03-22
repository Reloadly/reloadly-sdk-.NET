using Microsoft.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reloadly.Core.Testing;
using System;
using System.Collections.Generic;

namespace Reloadly.Authentication.Tests.Unit
{
    public abstract class BaseOperationsTests
    {
        protected static readonly string AccessToken = Guid.NewGuid().ToString();
        protected static readonly Uri BaseUri = new("https://topups-sandbox.reloadly.com");

        protected HttpTest CreateHttpTest() =>
            new HttpTest(BaseUri)
                .ExpectHeader(HeaderNames.Authorization, $"Bearer {AccessToken}");

        protected static void VerifyList<TExpected, TActual>(IList<TExpected> expected, IList<TActual> actual)
            where TExpected : class
            where TActual : class
        {
            Assert.IsTrue(ObjectComparer.AreListsEqual(expected, actual));

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.IsTrue(ObjectComparer.ArePropertiesEqual(expected[i], actual[i]));
            }
        }

        protected static void Verify<TExpected, TActual>(TExpected expected, TActual actual)
            where TExpected : class
            where TActual : class
        {
            Assert.IsTrue(ObjectComparer.ArePropertiesEqual(expected, actual));
        }
    }
}
