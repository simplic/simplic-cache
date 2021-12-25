using Simplic.Cache.Service;
using System;
using Unity;
using Xunit;

namespace Simplic.Cache.Test
{
    public class CacheTest
    {
        private readonly UnityContainer container;

        public CacheTest()
        {
            container = new UnityContainer();
            container.RegisterType<ICacheService, CacheService>();
            container.RegisterType<IWeakReferenceCacheService, WeakReferenceCacheService>();
        }

        [Fact]
        public void SetGetTest()
        {
            var service = container.Resolve<ICacheService>();

            var key = "Sample_Cache_Key";
            var value = Guid.NewGuid();

            service.Set(key, value);

            Assert.Equal(value, service.Get<Guid>(key));

            Assert.Equal(default(Guid), service.Get<Guid>(Guid.NewGuid().ToString()));

            service.Remove<Guid>(key);
            Assert.Equal(default(Guid), service.Get<Guid>(key));
        }

        [Fact]
        public void SetGetWeakReferenceTest()
        {
            var service = container.Resolve<IWeakReferenceCacheService>();

            var key = "Sample_Cache_Key_WK";
            var value = Guid.NewGuid();

            service.Set(key, value);

            Assert.Equal(value, service.Get<Guid>(key));

            Assert.Equal(default(Guid), service.Get<Guid>(Guid.NewGuid().ToString()));

            service.Remove<Guid>(key);
            Assert.Equal(default(Guid), service.Get<Guid>(key));
        }

        [Fact]
        public void SetGetObjectTest()
        {
            var service = container.Resolve<ICacheService>();

            var obj = new CacheTestObject()
            {
                CacheKey = "Test_Key"
            };

            service.Set(obj);

            Assert.Equal(obj, service.Get<CacheTestObject>(obj.CacheKey));

            Assert.Null(service.Get<CacheTestObject>(Guid.NewGuid().ToString()));

            service.Remove<CacheTestObject>(obj.CacheKey);
            Assert.Null(service.Get<CacheTestObject>(obj.CacheKey));
        }

        [Fact]
        public void SetGetObjectWeakReferenceTest()
        {
            var service = container.Resolve<IWeakReferenceCacheService>();

            var obj = new CacheTestObject()
            {
                CacheKey = "Test_Key_WR"
            };

            service.Set(obj);

            Assert.Equal(obj, service.Get<CacheTestObject>(obj.CacheKey));

            Assert.Null(service.Get<CacheTestObject>(Guid.NewGuid().ToString()));

            service.Remove<CacheTestObject>(obj.CacheKey);
            Assert.Null(service.Get<CacheTestObject>(obj.CacheKey));
        }

        [Fact]
        public void LambdaGet()
        {
            var value = Guid.NewGuid();
            var key = "SAMPLE_KEY";

            var service = container.Resolve<ICacheService>();
            var newValue = service.Get<Guid>(key, () => 
            {
                return value;
            });

            Assert.Equal(newValue, value);
        }

        [Fact]
        public void LambdaGetWeakReference()
        {
            var value = Guid.NewGuid();
            var key = "SAMPLE_KEY";

            var service = container.Resolve<IWeakReferenceCacheService>();
            var newValue = service.Get<Guid>(key, () =>
            {
                return value;
            });

            Assert.Equal(newValue, value);
        }
    }
}
