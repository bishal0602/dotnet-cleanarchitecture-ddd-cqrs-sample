export type AuthResponse = {
  user: UserDto;
  token: string;
};

type UserDto = {
  id: string;
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
};

type Author = {
  id: string;
  firstName: string;
  lastName: string;
};

export type Book = {
  id: string;
  title: string;
  description: string;
  authors: Author[];
};

export type Pagination = {
  totalCount: number;
  pageSize: number;
  currentPage: number;
  totalPages: number;
  previousPageLink: string | null;
  nextPageLink: string | null;
};

export type BooksResponse = {
  books: Book[];
  pagination: Pagination;
};

export type Review = {
  id: string;
  username: string;
  comment: string;
};

export type BookDetails = {
  bookCovers: null;
  reviews: Review[];
  id: string;
  title: string;
  description: string;
  authors: Author[];
};


export type FileExportModel = {
  data: any;
  fileName: string;
  contentType: string;
}