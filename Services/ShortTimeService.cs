using ufo_api.Interfaces;

class ShortTimeService : ITimeService
{
    public string GetTime() => DateTime.Now.ToLongTimeString();
}