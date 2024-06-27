using ufo_api.Interfaces;

class LongTimeService : ITimeService
{
    public string GetTime() => DateTime.Now.ToLongTimeString();
}