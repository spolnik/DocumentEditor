using System.Collections.Generic;

namespace DocumentEditorLibrary
{
    public interface IFactory<T>
    {
        T CreateNew();
        void Save(T pattern);
        T Get(string id);
        List<T> GetAll();
        void ClearAll();
    }
}