using Moq;
using Simplic.Cache.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Simplic.Cache.Test
{
    public class DataCacheTest
    {
        [Fact]
        public void Call_Get_Without_Cache_Entry()
        {
            var repository = new Mock<IDataCacheRepository>();

            bool getCalled = false;
            bool setCalled = false;

            string repositoryGet(string type, string key, string value)
            {
                getCalled = true;
                return null;
            }

            void repositorySet()
            {
                setCalled = true;
            }

            repository.Setup(x => x.Get<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                      .Returns<string, string, string>(repositoryGet);

            repository.Setup(x => x.Set<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                      .Callback(repositorySet);

            var service = new DataCacheService(repository.Object);

            var result = service.Get<string>("sample_type", "sample_key", "sample_value", () =>
            {
                return "Sample Value";
            });

            Assert.Equal("Sample Value", result);
            Assert.True(getCalled);
            Assert.True(setCalled);
        }

        [Fact]
        public void Call_Get_With_Cache_Entry()
        {
            var repository = new Mock<IDataCacheRepository>();

            bool getCalled = false;
            bool setCalled = false;

            string repositoryGet(string type, string key, string value)
            {
                return "Cache-Value";
            }

            void repositorySet()
            {
                setCalled = true;
            }

            repository.Setup(x => x.Get<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                      .Returns<string, string, string>(repositoryGet);

            repository.Setup(x => x.Set<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                      .Callback(repositorySet);

            var service = new DataCacheService(repository.Object);

            var result = service.Get<string>("sample_type", "sample_key", "sample_value", () =>
            {
                getCalled = true;
                return "Sample Value";
            });

            Assert.Equal("Cache-Value", result);
            Assert.False(getCalled);
            Assert.False(setCalled);
        }
    }
}
