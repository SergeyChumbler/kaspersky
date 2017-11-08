export interface Book {
    id: number;
    title: string;
    pages: number;
    publishingHouse: string;
    authors: Author[];
    publishDate: Date;
    isbn: string;
    imageUrl: string;
}

export interface Author {
    id: number;
    name: string;
    surname: string;
}