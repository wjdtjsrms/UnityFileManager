namespace JSGCode.Base
{
    using System;

    public interface IManager : IDisposable
    {
        public bool IsInit { get; }
        public void Init();
    }
}