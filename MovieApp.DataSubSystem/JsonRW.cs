using MovieApp.DataSubSystem.Contract;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieApp.DataSubSystem
{
    public class JsonRW : IJsonRW
    {
        private string fileName;

        public JsonRW()
        {
            this.fileName = "./Resource/moviedata.json";
        }

        public JsonRW(string fileName)
        {
            this.fileName = fileName;
        }

        public async Task<IEnumerable<T>> Read<T>()
        {
            using FileStream openStream = File.OpenRead(fileName);
            return await JsonSerializer.DeserializeAsync<IEnumerable<T>>(openStream);
        }

        public async Task<T> Write<T>(T data)
        {
            throw new System.NotImplementedException();
        }
    }
}
