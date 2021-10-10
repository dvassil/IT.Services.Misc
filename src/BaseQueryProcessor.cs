using System;
using System.Text;

namespace IT.Services.Misc
{
    internal class BaseQueryProcessor : IQueryProcessor, IDisposable
    {
        public void Dispose() { }

        public bool RemoveBetween(ref StringBuilder sb, string from, string to)
        {
            string tmp = sb.ToString();
            int p1 = tmp.IndexOf(from);
            if (p1 < 0)
                return false;
            int p2 = tmp.IndexOf(to, p1) + to.Length;
            tmp = tmp.Substring(p1 + from.Length, p2 - p1 - from.Length - to.Length);
            sb.Remove(p1, p2 - p1);
            return true;
        }

        public bool UseBetween(ref StringBuilder sb, string from, string to)
        {
            string tmp = sb.ToString();
            int p1 = tmp.IndexOf(from);
            if (p1 < 0)
                return false;
            int p2 = tmp.IndexOf(to, p1) + to.Length;
            tmp = tmp.Substring(p1 + from.Length, p2 - p1 - from.Length - to.Length);
            sb.Remove(p1, p2 - p1);
            sb.Insert(p1, tmp);
            return true;
        }

        public virtual string Process(string query)
        {
            return query;
        }

        public virtual string BuildSPCall(string procedureName, params string[] arguments)
        {
            throw new NotImplementedException();
        }
    }
}
