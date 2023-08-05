using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Project_Terror_Light.Logging
{
    public class ExecuteLogging : ConcurrentSmartThreadQueue<object>
    {

        public static string Path;
        public static ExecuteLogging LoginQueue = new ExecuteLogging(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
        public object SynRoot = new object();
        public ExecuteLogging(string path)
            : base(1)
        {
            Path = path + "\\Logging";
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            Start(10);
        }
        public void TryEnqueue(object obj)
        {
            base.Enqueue(obj);
        }
        protected unsafe override void OnDequeue(object obj, int time)
        {
            try
            {
                if (obj is string)
                {
                    string[] array = (obj as string).Split('#');
                    string Key = array[0] + "#" + Program.ServerConfig.ServerName;
                    string Message = array[1];
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
    }
}