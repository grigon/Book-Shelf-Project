using bookshelf.FakeData;

namespace bookshelf.Context
{
    public class FakeDataContext : IBaseContext
    {
        private DataFake _dataFake;
        public FakeDataContext(DataFake dataFake)
        {
            _dataFake = dataFake;
        }
        
        public void Commit()
        {
                
        }
    }
}