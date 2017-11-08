import { Component, OnInit, Inject, ViewChild, ElementRef } from '@angular/core';
import { Book, Author } from '../../_models/index';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

class BookInfo implements Book {
    constructor(
        public id: number,
        public title: string,
        public pages: number,
        public publishingHouse: string,
        public authors: Author[],
        public publishDate: Date,
        public isbn: string,
        public imageUrl: string ) { }
}

@Component({
    selector: 'book',
    templateUrl: './book.component.html'
})

export class BookComponent implements OnInit {

    private _getBooksUrl = "api/book/getallbooks";
    private _deleteBookUrl = "api/book/deletebook?id=";

    public books: Book[];
    private baseUrl: string;
    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    @ViewChild("fileInput") fileInput: ElementRef;

    addFile(): void {
        console.log(this.fileInput);
        let fi = this.fileInput.nativeElement;
        if (fi.files && fi.files[0]) {
            let fileToUpload = fi.files[0];
            this.upload(fileToUpload)
                .subscribe(res => {
                    console.log(res);
                });
        }
    }

    upload(fileToUpload: any) {
        let input = new FormData();
        input.append("file", fileToUpload);

        return this.http
            .post("/api/book/uploadfile", input);
    }

    ngOnInit() {
        this.getContacts();
    }

    getContacts() {
        var headers = new Headers();
        var getContactsUrl = this._getBooksUrl;
        this.http.get(getContactsUrl, { headers: headers })
            .subscribe(res => {
                this.books = res.json();
                console.log(this.books);
            });
    }

    deleteBook(id: string) {
        console.log(id);
        var book = this.books.findIndex(b => b.id === +id);
        if (book !== undefined) {
            console.log(id);
            (b => {
                console.log(id);
                var headers = new Headers();
                headers.append('Content-Type', 'application/json');
                this.http.delete(this._deleteBookUrl + id)
                    .subscribe(result => {
                        if (result.status === 200) {
                            this.books.splice(book, 1);
                        }
                        else { alert("Failed"); }
                    },
                    error => console.error(error));
            })(book);
        }
    }

}