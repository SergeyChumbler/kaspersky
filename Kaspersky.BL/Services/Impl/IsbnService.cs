using System.Collections.Generic;
using System.Linq;

namespace Kaspersky.BL.Services.Impl
{
    public class IsbnService : IIsbnService
    {
	    private readonly IEnumerable<IIsbnValidator> _isbnValidators;

		public IsbnService(IEnumerable<IIsbnValidator> isbnValidators)
			=> _isbnValidators = isbnValidators;
	    
	    public bool Validate(string isbn)
	    {
		    if (string.IsNullOrEmpty(isbn))
			    return false;

			return _isbnValidators.FirstOrDefault(v => v.CanValidate(isbn))?.IsValid(isbn) ?? false; 
	    }
    }
}
