namespace Kaspersky.BL.Services
{
    public interface IIsbnValidator
    {
        bool CanValidate(string isbn);
        bool IsValid(string isbn);
    }
}
