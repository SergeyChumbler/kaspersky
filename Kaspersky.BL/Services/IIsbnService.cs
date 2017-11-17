namespace Kaspersky.BL.Services
{
    public interface IIsbnService
    {
        bool Validate(string isbn);
    }
}
