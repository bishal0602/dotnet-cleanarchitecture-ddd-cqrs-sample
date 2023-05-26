import axios from "axios";
import {
  Book,
  BookDetails,
  BooksResponse,
  Pagination,
} from "../types/ApiTypes";

const API_SERVER = "http://localhost:5263";

export const getPaginatedBooks = async (
  pageNumber: number,
  pageSize: number
): Promise<BooksResponse> => {
  const response = await axios.get(
    `${API_SERVER}/api/books?pageSize=${pageSize}&pageNumber=${pageNumber}`
  );
  if (response.status === 200) {
    const books = response.data as Book[];
    const pagination = response.headers["x-pagination"];
    const paginationObject = JSON.parse(pagination) as Pagination;
    return { books: books, pagination: paginationObject };
  }
  throw new Error("Error getting books");
};

export const getBookDetails = async (id: string): Promise<BookDetails> => {
  const response = await axios.get(`${API_SERVER}/api/books/${id}`);
  if (response.status === 200) {
    const book = response.data as BookDetails;
    return book;
  }
  throw new Error("Error getting book");
};
