using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Kaspersky.Data.Domain;
using Kaspersky.Data.Repository;
using Kaspersky.Data.Specification.Impl;
using Kaspersky.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kaspersky.Web.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IRepository<Book> repository, IMapper mapper)
        {
            _bookRepository = repository;
            _mapper = mapper;
        }

        [HttpGet("getallbooks")]
        public async Task<IEnumerable<BookModel>> GetAllAsync()
        {
            var books = await _bookRepository.GetManyAsync(include: source => source.Include(a => a.Autors));

            return _mapper.Map<ICollection<Book>, IEnumerable<BookModel>>(books);
        }

        [HttpGet("get")]
        public async Task<BookModel> GetAsync(int id)
            => _mapper.Map<Book, BookModel>(await GetBookById(id));


        [HttpDelete("deletebook")]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            var book = await GetBookById(id);
            if (book == null)
                return NotFound();

            await _bookRepository.DeleteAsync(book);
            return Ok();
        }

        [HttpPost("updatebook")]
        public async Task<IActionResult> UpdateBookAsync([FromBody]BookModel bookModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (bookModel.Id == 0)
            {
                var newBook = await _bookRepository.CreateAsync(_mapper.Map<BookModel, Book>(bookModel));
                return CreatedAtAction(nameof(GetAsync), new { id = newBook.Id }, newBook);
            }

            var book = await GetBookById(bookModel.Id);

            if (book == null)
                return NotFound();

            return Ok(await _bookRepository.UpdateAsync(_mapper.Map<BookModel, Book>(bookModel)));
        }

        private Func<int, Task<Book>> GetBookById => async id
            => await _bookRepository.GetSingleAsync(new AdHocSpecification<Book>(b => b.Id == id), source => source.Include(b => b.Autors));


        [HttpPost("uploadfile")]
        public async Task<IActionResult> UploadFileAsync(IFormFile file, [FromServices] IHostingEnvironment env)
        {
            var filePath = Path.Combine(env.WebRootPath, "img", file.FileName);

            if (file.Length <= 0)
                return Ok(new { file.Length, filePath });

            using (var stream = new FileStream(filePath, FileMode.Create))
                await file.CopyToAsync(stream);

            return Ok(new { file.Length, filePath });
        }
    }
}
